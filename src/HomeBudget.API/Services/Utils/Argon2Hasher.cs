using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace HomeBudget.API.Services.Utils;

public static class Argon2Hasher
{
    private const int KeySize = 128 / 8;
    private const int Iterations = 3;
    private const int Parallelism = 4;
    private const int Memory = 64 * 1024;
    
    public static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(KeySize);
    }

    public static byte[] GenerateHash(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

        argon2.Salt = salt;
        argon2.Iterations = Iterations;
        argon2.DegreeOfParallelism = Parallelism;
        argon2.MemorySize = Memory;

        return argon2.GetBytes(KeySize);
    }

    public static bool ValidateHash(string password, byte[] hash, byte[] salt)
    {
        var passwordHash = GenerateHash(password, salt);
        return hash.SequenceEqual(passwordHash);
    }
}