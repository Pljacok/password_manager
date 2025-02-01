using System;
using System.Security.Cryptography;
using System.Text;

class PasswordManager
{
    static void Main()
    {
        Console.WriteLine("🔐 Менеджер паролів");
        Console.Write("Введіть пароль для шифрування: ");
        string password = Console.ReadLine();
        
        string encrypted = Encrypt(password);
        Console.WriteLine($"Зашифрований пароль: {encrypted}");
        
        string decrypted = Decrypt(encrypted);
        Console.WriteLine($"Розшифрований пароль: {decrypted}");
    }

    static string Encrypt(string text)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes key
            aes.IV = new byte[16];
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(encrypted);
        }
    }

    static string Decrypt(string encryptedText)
    {
        byte[] data = Convert.FromBase64String(encryptedText);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("1234567890123456");
            aes.IV = new byte[16];
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}