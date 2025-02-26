using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneNumbers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Common;

public static class CommonHelper
{
    public static string CreateUniqueId(int length = 64)
    {
        var bytes = Array.Empty<byte>();
        return ByteArrayToString(bytes);
    }

    //var orderId = $"SID_ORD_{DateTime.Now:HHmmss}_{Guid.NewGuid():N}".Substring(0, 20);

    private static string ByteArrayToString(byte[] array)
    {
        var hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        email = email.Trim();
        var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
        return result;
    }

    //public static bool IsValidEmail(this string email)
    //{
    //    return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
    //}

    public static string GenerateRandomDigitCode(int length)
    {

        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than zero.");
        }

        var randomNumberBuffer = new byte[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumberBuffer);
        }

        StringBuilder code = new StringBuilder(length);
        foreach (byte num in randomNumberBuffer)
        {
            int digit = num % 10; // Ensure the digit is between 0 and 9
            code.Append(digit);
        }

        return code.ToString();
    }

    public static bool IsValidPhoneNumber(string phoneNumber, string countryCode)
    {
        bool valid = false;

        try
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var number = phoneNumberUtil.Parse(phoneNumber, countryCode);
            valid = phoneNumberUtil.IsValidNumber(number);
        }
        catch (NumberParseException)
        {

        }

        return valid;

    }

    public static string EncryptStringToAES(string plainText, string key, string iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            // Create an encryptor to perform the stream transform
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // Write all data to the stream
                        swEncrypt.Write(plainText);
                    }
                    return ByteArrayToHexString(msEncrypt.ToArray());
                }
            }
        }
    }

    static string ByteArrayToHexString(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    public static string DecryptStringFromAES(string cipherText, string key, string iv)
    {
        // Convert the ciphertext from hexadecimal to byte array
        byte[] cipherBytes = HexStringToByteArray(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key)
;
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            // Create a decryptor to perform the stream transform
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption
            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream and place them in a string
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    static byte[] HexStringToByteArray(string hex)
    {
        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }

    public static string Base64Decodestring(string base64EncodedString)
    {
        if (string.IsNullOrEmpty(base64EncodedString))
        {
            return default;
        }
        else
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedString);
            var decodedData = Encoding.UTF8.GetString(base64EncodedBytes);
            return decodedData;
        }
    }

    public static string Encrypt(this string data, string key)
    {
        byte[][] keys = GetHashKeys(key);

        string encryptedData = EncryptStringToBytes_Aes(data, keys[0], keys[1]);

        return encryptedData;
    }

    public static string Decrypt(this string data, string key)
    {
        byte[][] keys = GetHashKeys(key);

        string decryptedData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);

        return decryptedData;
    }

    private static byte[][] GetHashKeys(string key)
    {
        byte[][] result = new byte[2][];
        Encoding encoding = Encoding.UTF8;

        SHA256 sha2 = new SHA256CryptoServiceProvider();

        byte[] rawKey = encoding.GetBytes(key);
        byte[] rawIV = encoding.GetBytes(key);

        byte[] hashKey = sha2.ComputeHash(rawKey);
        byte[] hashIV = sha2.ComputeHash(rawIV);

        Array.Resize(ref hashIV, 16);

        result[0] = hashKey;
        result[1] = hashIV;

        return result;
    }

    private static string EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        byte[] encrypted;

        using (var aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.Key = Key;
            aesAlgorithm.IV = IV;

            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                        new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encrypted);
    }

    private static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
    {
        byte[] cipherText = Convert.FromBase64String(cipherTextString);

        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherTextString));
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt =
                        new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }

    public static string encryptResponse(DateTime datetime, string applicationId, [FromBody] dynamic pliantext)
    {
        string formattedOmtid = datetime.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
        string key = Convert.ToBase64String(Encoding.UTF8.GetBytes(formattedOmtid)).Substring(0, 16);
        var iv = Guid.Parse(applicationId).ToString("N").Substring(0, 16);
        var encryptedData = CommonHelper.EncryptStringToAES(Convert.ToString(pliantext), key, iv);
        return encryptedData;
    }

    public static string decryptPayload(DateTime datetime, string applicationId, [FromBody] string encryptedpayload)
    {
        string formattedOmtid = datetime.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
        string key = Convert.ToBase64String(Encoding.UTF8.GetBytes(formattedOmtid)).Substring(0, 16);
        var iv = Guid.Parse(applicationId).ToString("N").Substring(0, 16);
        var decryptedData = DecryptStringFromAES(encryptedpayload, key, iv);
        return decryptedData;
    }

    public static bool IsValidBase64String(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return false;
        try
        {
            Convert.FromBase64String(base64String);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static string GetUserIpAddress(HttpContext context)
    {
        var ipAddress = "";

        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            ipAddress = context.Request.Headers["X-Forwarded-For"].ToString().Split(',')[0];
        }

        ipAddress = context.Connection.RemoteIpAddress.ToString();

        return ipAddress;
    }

    private static string GetUserPlatform(HttpContext context)
    {
        var userAgent = context.Request.Headers["User-Agent"].ToString();

        if (userAgent.Contains("Mobile", StringComparison.OrdinalIgnoreCase))
        {
            return "Mobile";
        }
        else
        {
            return "Web";
        }
    }
}
