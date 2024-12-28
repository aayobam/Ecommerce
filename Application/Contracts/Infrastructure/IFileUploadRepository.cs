using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Infrastructure;

public interface IFileUploadRepository
{
    Task<string> InsertOrUpdateAsync(string bucketName, IFormFile file);
    Task<object> GetPresignedUrlAsync(string bucketName);
    public Task<bool> DeleteIfExistsAsync(string buckName, string key);
    public Task<object> UploadMultipartFileAsync(string bucketName, FormFile file);
    public Task<object> GetMultiPartPresignedUrlAsync(string bucketName, string key, string uploadId, int partNumber);
    public Task<object> CompleteMultipartUploadAsync(string bucketName, string key, CompleteMultipartUploadVm complete);
}