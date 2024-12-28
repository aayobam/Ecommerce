namespace Application.AppSettingsConfig;

public sealed class AmazonS3Settings
{
    public string BaseUrl { get; set; }
    public string Region { get; set; }
    public string BucketName { get; set; }
    public string AWSAccessKey { get; set; }
    public string AWSSecretKey { get; set; }
    public string PresignedUrl { get; set; }
    public int PresignedUrlDuration { get; set; }
}
