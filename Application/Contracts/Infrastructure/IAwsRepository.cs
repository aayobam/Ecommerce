using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Infrastructure;

public interface IAwsRepository
{
    Task<bool> BucketExistAsync(string bucketName);
    Task<string> InsertOrUpdateAsync(string bucketName, string directory, string subDirectory, IFormFile file);
    Task<object> GetPresignedUrlAsync(string bucketName);
    Task<bool> DeleteIfExistsAsync(string buckName, string key);
    Task<object> UploadMultipartFileAsync(string bucketName, string directory, string subDirectory, IFormFile file);
    Task<object> GetMultiPartPresignedUrlAsync(string bucketName, string key, string uploadId, int partNumber);
    Task<object> CompleteMultipartUploadAsync(string bucketName, string directory, string subDirectory, string key, CompleteMultipartUploadVm complete);
    Task<object> GetAppSettingsConfigAsync();
}