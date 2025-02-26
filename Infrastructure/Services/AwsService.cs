using Amazon.S3;
using Amazon.S3.Model;
using Application.Contracts.Infrastructure;
using Application.DTOs;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;
using Application.AppSettingsConfig;

namespace Infrastructure.Repositories;



public class AwsService : IAwsRepository
{
    private readonly ILogger<AwsService> _logger;
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonSecretsManager _secretManagerClient;

    public AwsService(ILogger<AwsService> logger, IAmazonS3 s3Client, IAmazonSecretsManager secretManagerClient)
    {
        _logger = logger;
        _s3Client = s3Client;
        _secretManagerClient = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName("us-east-1"));
    }

    public async Task<bool> BucketExistAsync(string bucketName)
    {
        try
        {
            await _s3Client.EnsureBucketExistsAsync(bucketName);
            return true;
        }
        catch (AmazonS3Exception exception)
        {
            return false;
            throw new EcommerceNotFoundException($"bucket - {bucketName} not found.", exception.StatusCode.ToString());
        }
        catch(Exception exception)
        {
            _logger.LogInformation($"\n {exception.Message} \n");
            throw new Exception(exception.Message);
        }
    }

    public async Task<string> InsertOrUpdateAsync(string bucketName, string directory, string subDirectory, IFormFile file)
    {
        string key = file.Name.ToString();
        var request = new PutObjectRequest()
        {
            InputStream = file.OpenReadStream(),
            BucketName = bucketName,
            Key = $"{directory}/{subDirectory}/{key}",
            ContentType = file.ContentType
        };

        try
        {
            var response = await _s3Client.PutObjectAsync(request);

            _logger.LogInformation($"\n Successfully saved {key} into bucket {bucketName} \n");

            return string.Format($"https://{bucketName}.s3.amazonaws.com/{directory}/{subDirectory}/{key}");
        }
        catch (AmazonS3Exception exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation($"\n could not upload {file.Name.ToString()} - {exception.Message} | {DateTime.UtcNow} \n");
                throw new EcommerceBadRequestException($"could not upload {file.Name.ToString()}: {exception.Message}", HttpStatusCode.BadRequest.ToString());
            }
            throw;
        }
    }

    public async Task<object> GetPresignedUrlAsync(string bucketName)
    {
        string key = Guid.NewGuid().ToString();
        var request = new GetPreSignedUrlRequest()
        {
            BucketName = bucketName,
            Key = key,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(60)
        };

        try
        {
            string presignedUrl = await _s3Client.GetPreSignedURLAsync(request);
            return new { key, url = presignedUrl};
        }
        catch (AmazonS3Exception exception)
        {
            _logger.LogInformation($"\n item: could not be uploaded. | {DateTime.UtcNow} \n");
            throw new EcommerceBadRequestException($"S3 error uploading file: {exception.Message}", HttpStatusCode.BadRequest.ToString());
        }
    }

    public async Task<object> UploadMultipartFileAsync(string bucketName, string directory, string subDirectory, IFormFile file)
    {
        string key = file.Name.ToString();
        var request = new InitiateMultipartUploadRequest()
        {
            BucketName = bucketName,
            Key = $"{directory}/{subDirectory}/{key}",
            ContentType = file.ContentType,
            Metadata =
            {
                ["file-name"] = file.Name
            }
        };

        try
        {
            var response = await _s3Client.InitiateMultipartUploadAsync(request);
            return new { key, uploadId = response.UploadId };
        }
        catch(AmazonS3Exception exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation($"\n {exception.Message} - error starting multipart upload | {DateTime.UtcNow} \n");
                throw new EcommerceBadRequestException($"{exception.Message} - error starting multipart upload", HttpStatusCode.BadGateway.ToString());
            }
            throw;
        }
    }

    public async Task<object> GetMultiPartPresignedUrlAsync(string bucketName, string key, string uploadId, int partNumber)
    {
        var request = new GetPreSignedUrlRequest()
        {
            BucketName = bucketName,
            Key = key,
            UploadId = uploadId,
            PartNumber = partNumber,
            Expires = DateTime.UtcNow.AddMinutes(60)
        };

        try
        {
            var presignedUrl = await _s3Client.GetPreSignedURLAsync(request);
            return new { key, presignedUrl };
        }
        catch (AmazonS3Exception exception)
        {
            switch (exception.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    _logger.LogInformation($"\n {exception.Message} - Could not get presigned URL | {DateTime.UtcNow} \n");
                    throw new EcommerceBadRequestException(exception.Message, exception.StatusCode.ToString());

                case HttpStatusCode.NotFound:
                    _logger.LogInformation($"\n {exception.Message} - Could not get presigned URL | {DateTime.UtcNow} \n");
                    throw new EcommerceNotFoundException($"{key} not found", exception.StatusCode.ToString());

                default:
                    _logger.LogError($"\n Unexpected error: {exception.Message} | {DateTime.UtcNow} \n");
                    throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"\n Unexpected error: {ex.Message} | {DateTime.UtcNow} \n");
            throw new Exception("An unexpected error occurred while generating the presigned URL.", ex);
        }
    }

    public async Task<object> CompleteMultipartUploadAsync(string bucketName, string directory, string subDirectory, string key, CompleteMultipartUploadVm complete)
    {
        var request = new CompleteMultipartUploadRequest()
        {
            BucketName = bucketName,
            Key = $"images/{key}",
            UploadId = complete.UploadId,
            PartETags = complete.Parts.Select(x => new PartETag(x.PartNumber, x.ETag)).ToList(),
        };

        try
        {
            var response = await _s3Client.CompleteMultipartUploadAsync(request);
            return new { key, location = response.Location };
        }
        catch(AmazonS3Exception exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation($"\n {exception.Message} - could not complete uploads | {DateTime.UtcNow} \n");
                throw new EcommerceBadRequestException($"\n {exception.Message} - could not complete uploads", HttpStatusCode.BadRequest.ToString());
            }
            throw;
        }
    }

    public async Task<bool> DeleteIfExistsAsync(string bucketName, string key)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };

        try
        {
            var response = await _s3Client.DeleteObjectAsync(request);

            _logger.LogInformation($"\n deleted item: {key} | {DateTime.UtcNow} \n");

            return response.HttpStatusCode == HttpStatusCode.NoContent;
        }
        catch (AmazonS3Exception exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"\n {exception.Message} - item:{key} not found | {DateTime.UtcNow} \n");
                throw new EcommerceNotFoundException($"\n item: {key} not found | {DateTime.UtcNow} \n");
            }
            throw;
        }
    }

    public async Task<object> GetAppSettingsConfigAsync()
    {
        /*
        *	Use this code snippet in your app.
        *	If you need more information about configurations or implementing the sample code, visit the AWS docs:
        *	https://aws.amazon.com/developer/language/net/getting-started
        */

        string secretName = "prod/AppSettings";

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
        };

        GetSecretValueResponse response;

        try
        {
            response = await _secretManagerClient.GetSecretValueAsync(request);
            var data = JsonConvert.DeserializeObject<AwsSecretManagerResponse>(response.SecretString);
            return data;
        }
        catch (Exception exception)
        {
            // For a list of the exceptions thrown, see
            // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            throw exception;
        }
    }
}