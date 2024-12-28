using Amazon.S3;
using Amazon.S3.Model;
using Application.Contracts.Infrastructure;
using Application.DTOs;
using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infrastructure.Repositories;



public class FileUploadService : IFileUploadRepository
{
    private readonly ILogger<FileUploadService> _logger;
    private readonly IAmazonS3 _s3Client;
    

    public FileUploadService(ILogger<FileUploadService> logger, IAmazonS3 s3Client)
    {
        _logger = logger;
        _s3Client = s3Client;
    }

    public async Task<string> InsertOrUpdateAsync(string bucketName, IFormFile file)
    {
        string key = Guid.NewGuid().ToString();
        var request = new PutObjectRequest()
        {
            InputStream = file.OpenReadStream(),
            BucketName = bucketName,
            Key = key,
            ContentType = file.ContentType
        };

        try
        {
            var response = await _s3Client.PutObjectAsync(request);

            _logger.LogInformation($"\n Successfully saved {key} into bucket {bucketName} \n");

            return string.Format($"https://{bucketName}.s3.amazonaws.com/{key}");
        }
        catch (AmazonS3Exception exception)
        {
            if (exception.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation($"\n item: could not be uploaded. | {DateTime.UtcNow} \n");
                throw new EcommerceBadRequestException($"S3 error uploading file: {exception.Message}", HttpStatusCode.BadRequest.ToString());
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

    public async Task<object> UploadMultipartFileAsync(string bucketName, FormFile file)
    {
        string key = Guid.NewGuid().ToString();
        var request = new InitiateMultipartUploadRequest()
        {
            BucketName = bucketName,
            Key = $"images/{key}",
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
                throw new EcommerceNotFoundException($"\n {exception.Message} - item:{key} not found | {DateTime.UtcNow} \n");
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
            Expires = DateTime.UtcNow.AddMinutes(30)
        };

        try
        {
            var presignedUrl = await _s3Client.GetPreSignedURLAsync(request);
            return new { key, presignedUrl };
        }
        catch(AmazonS3Exception exception)
        {
            if(exception.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogInformation($"\n {exception.Message} - could not get presigned url | {DateTime.UtcNow} \n");
                throw new EcommerceBadRequestException($"{exception.Message}", exception.StatusCode.ToString());
            }
            throw;
        }
    }

    public async Task<object> CompleteMultipartUploadAsync(string bucketName, string key, CompleteMultipartUploadVm complete)
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
}