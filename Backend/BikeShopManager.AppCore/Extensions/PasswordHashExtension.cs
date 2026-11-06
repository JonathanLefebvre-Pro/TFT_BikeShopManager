using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace BikeShopManager.AppCore.Extensions.Argon2;

public class PasswordHashExtension
{
    private const int parallelism = 1;
    private const int iterations = 2;
    private const int memorySize = 19456;
    private const int hashSize = 32;
    private const int saltSize = 32;

    public static byte[] Argon2idHash(
        string password,
        byte[] salt,
        int degreeOfParallelism,
        int memorySize,
        int iterations
    )
    {
        using Argon2id a2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = degreeOfParallelism,
            MemorySize = memorySize,
            Iterations = iterations,
        };

        return a2.GetBytes(hashSize);
    }

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
        byte[] hash = Argon2idHash(password, salt, parallelism, memorySize, iterations);

        string b64(byte[] b) => Convert.ToBase64String(b).TrimEnd('=');
        return $"$argon2id$v=19$m={memorySize},t={iterations},p={parallelism}${b64(salt)}${b64(hash)}";
    }

    public static bool VerifyPassword(string password, string hash)
    {
        //P0 : empty
        //P1 : Algo (argon2id)
        //P2 : Version (v=19)
        //P3.1 : Memory size
        //P3.2 : Iterations
        //P3.3 : Parallelism
        //P4 : Salt
        //P5 : Hash

        //Split the hash
        string[] parts = hash.Split('$', StringSplitOptions.RemoveEmptyEntries);
        //For the time being, it could break if the stored hash was not build with the same Argon2 (argon2i or argon2d) algo or with another alog from Argon2
        string algo = parts[0];
        string version = parts[1].Split('=').Last();
        string[] parameters = parts[2].Split(',');
        int memory = int.Parse(parameters[0].Split('=').Last());
        int iterations = int.Parse(parameters[1].Split('=').Last());
        int parallelism = int.Parse(parameters[2].Split('=').Last());
        byte[] storedSalt = FromBase64NoPadding(parts[3]);
        byte[] storedHash = FromBase64NoPadding(parts[4]);

        byte[] actual = Argon2idHash(password, storedSalt, parallelism, memory, iterations);
        return CryptographicOperations.FixedTimeEquals(actual, storedHash);
    }

    public static byte[] FromBase64NoPadding(string value)
    {
        var padding = (4 - value.Length % 4) % 4;
        return Convert.FromBase64String(value.PadRight(value.Length + padding, '='));
    }
}
