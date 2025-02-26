using Newtonsoft.Json;

namespace Application.AppSettingsConfig;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AmazonS3Settings
{
    [JsonProperty("BaseUrl")]
    public string BaseUrl { get; set; }

    [JsonProperty("Region")]
    public string Region { get; set; }

    [JsonProperty("BucketName")]
    public string BucketName { get; set; }

    [JsonProperty("AccessKey")]
    public string AccessKey { get; set; }

    [JsonProperty("SecretKey")]
    public string SecretKey { get; set; }

    [JsonProperty("PresignedUrlDuration")]
    public int PresignedUrlDuration { get; set; }
}

public class ConnectionStrings
{
    [JsonProperty("EcommerceDbConnectionString")]
    public string EcommerceDbConnectionString { get; set; }
}

public class CronExpressions
{
    [JsonProperty("OrderUpdate")]
    public string OrderUpdate { get; set; }
}

public class EmailSettings
{
    [JsonProperty("ApiKey")]
    public string ApiKey { get; set; }

    [JsonProperty("FromEmail")]
    public string FromEmail { get; set; }

    [JsonProperty("FromName")]
    public string FromName { get; set; }
}

public class EncryptionSettings
{
    [JsonProperty("IvKey")]
    public string IvKey { get; set; }

    [JsonProperty("SecretKey")]
    public string SecretKey { get; set; }
}

public class JwtSettings
{
    [JsonProperty("Issuer")]
    public string Issuer { get; set; }

    [JsonProperty("Audience")]
    public string Audience { get; set; }

    [JsonProperty("Secret")]
    public string Secret { get; set; }

    [JsonProperty("ExpiryInMinutes")]
    public int ExpiryInMinutes { get; set; }
}

public class Logging
{
    [JsonProperty("LogLevel")]
    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    [JsonProperty("Default")]
    public string Default { get; set; }

    [JsonProperty("Microsoft.AspNetCore")]
    public string MicrosoftAspNetCore { get; set; }
}

public class MailingSettings
{
    [JsonProperty("BaseUrl")]
    public string BaseUrl { get; set; }

    [JsonProperty("EmailEndpoint")]
    public string EmailEndpoint { get; set; }

    [JsonProperty("SmsEndpoint")]
    public string SmsEndpoint { get; set; }

    [JsonProperty("FromName")]
    public string FromName { get; set; }
}

public class OtpSettings
{
    [JsonProperty("OtpLength")]
    public int OtpLength { get; set; }

    [JsonProperty("Secret")]
    public string Secret { get; set; }

    [JsonProperty("ExpiryInMinutes")]
    public int ExpiryInMinutes { get; set; }
}

public class AwsSecretManagerResponse
{
    [JsonProperty("Logging")]
    public Logging Logging { get; set; }

    [JsonProperty("ConnectionStrings")]
    public ConnectionStrings ConnectionStrings { get; set; }

    [JsonProperty("JwtSettings")]
    public JwtSettings JwtSettings { get; set; }

    [JsonProperty("OtpSettings")]
    public OtpSettings OtpSettings { get; set; }

    [JsonProperty("EmailSettings")]
    public EmailSettings EmailSettings { get; set; }

    [JsonProperty("MailingSettings")]
    public MailingSettings MailingSettings { get; set; }

    [JsonProperty("EncryptionSettings")]
    public EncryptionSettings EncryptionSettings { get; set; }

    [JsonProperty("AmazonS3Settings")]
    public AmazonS3Settings AmazonS3Settings { get; set; }

    [JsonProperty("MaxFailedAccessAttempts")]
    public int MaxFailedAccessAttempts { get; set; }

    [JsonProperty("CronExpressions")]
    public CronExpressions CronExpressions { get; set; }

    [JsonProperty("AllowedHosts")]
    public string AllowedHosts { get; set; }
}


