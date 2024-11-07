using PhoneNumbers;
using System.Security.Cryptography;
using System.Text;

namespace Application.Contracts.Persistence;

public static class CommonHelper
{
    public static string CreateUniqueId(int length = 64)
    {
        var bytes = Array.Empty<byte>();
        return ByteArrayToString(bytes);
    }

    public static bool IsValidPhoneNumber(string phoneNumber, string countryCode)
    {
        // Phone number util validation 
        //return true;
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
    private static string ByteArrayToString(byte[] array)
    {
        var hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

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
    public static string Base64Decodestring(string base64EncodedString)
    {
        if (string.IsNullOrEmpty(base64EncodedString))
        {
            return default;
        }
        else
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedString);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }


    public static string DecryptStringFromAES(string cipherText, string key, string iv)
    {
        // Convert the ciphertext from hexadecimal to byte array
        byte[] cipherBytes = HexStringToByteArray(cipherText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
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
    public static string GenerateKey()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateKey();
            return Convert.ToBase64String(aesAlg.Key);
        }
    }

    public static string GenerateIv()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateIV();
            return Convert.ToBase64String(aesAlg.IV);
        }
    }

    private static byte[][] GetHashKeys(string key)
    {
        byte[][] result = new byte[2][];
        Encoding enc = Encoding.UTF8;

        SHA256 sha2 = new SHA256CryptoServiceProvider();

        byte[] rawKey = enc.GetBytes(key);
        byte[] rawIV = enc.GetBytes(key);

        byte[] hashKey = sha2.ComputeHash(rawKey);
        byte[] hashIV = sha2.ComputeHash(rawIV);

        Array.Resize(ref hashIV, 16);

        result[0] = hashKey;
        result[1] = hashIV;

        return result;
    }

    public static string EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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
            aesAlgorithm.Mode = CipherMode.CBC;
            aesAlgorithm.Padding = PaddingMode.PKCS7;

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

    public static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
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
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

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

}
