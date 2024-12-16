using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class CryptoService : ICryptoService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public string CriptografarString(string texto, string chave)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(chave);

            using (var sha256 = SHA256.Create())
            {
                keyBytes = sha256.ComputeHash(keyBytes);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.GenerateIV();
                byte[] iv = aesAlg.IV;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv))
                using (var msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length);

                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(texto);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string DescriptografarString(string textoCriptografado, string chave)
        {
            byte[] fullCipher = Convert.FromBase64String(textoCriptografado);
            byte[] keyBytes = Encoding.UTF8.GetBytes(chave);

            using (var sha256 = SHA256.Create())
            {
                keyBytes = sha256.ComputeHash(keyBytes);
            }

            byte[] iv = new byte[16];
            Array.Copy(fullCipher, 0, iv, 0, iv.Length);

            byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];
            Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = iv;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(cipherBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public string HashearSenha(string senha)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerificarSenhaHasheada(string senha, string senhaHasheada)
        {
            byte[] hashBytes = Convert.FromBase64String(senhaHasheada);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
