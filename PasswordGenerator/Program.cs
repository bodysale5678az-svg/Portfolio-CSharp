// ============================================================
//  Password Generator — C#
//  Author: Mahmoud Saleh
// ============================================================
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator;

/// <summary>Cryptographically secure password generator.</summary>
public static class Generator
{
    private const string Lower   = "abcdefghijklmnopqrstuvwxyz";
    private const string Upper   = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits  = "0123456789";
    private const string Symbols = "!@#$%^&*()-_=+[]{};:,.<>?";

    /// <summary>Returns a random index in [0, max) using a CSPRNG.</summary>
    private static int SecureIndex(int max)
    {
        var bytes = new byte[4];
        while (true)
        {
            RandomNumberGenerator.Fill(bytes);
            uint value = BitConverter.ToUInt32(bytes, 0);
            // Rejection sampling to avoid modulo bias
            uint limit = uint.MaxValue - (uint.MaxValue % (uint)max);
            if (value < limit) return (int)(value % (uint)max);
        }
    }

    /// <summary>
    /// Builds a password that contains at least one of every selected
    /// character class, then shuffles it with a Fisher–Yates pass.
    /// </summary>
    public static string Generate(int length, bool useUpper, bool useDigits, bool useSymbols)
    {
        if (length < 4) throw new ArgumentOutOfRangeException(nameof(length), "Minimum length is 4");

        var pools = new List<string> { Lower };
        if (useUpper)   pools.Add(Upper);
        if (useDigits)  pools.Add(Digits);
        if (useSymbols) pools.Add(Symbols);

        string all = string.Concat(pools);
        var chars = new List<char>(length);

        // guarantee one char from every selected class
        foreach (var pool in pools)
            chars.Add(pool[SecureIndex(pool.Length)]);

        // fill the remainder from the combined alphabet
        while (chars.Count < length)
            chars.Add(all[SecureIndex(all.Length)]);

        // shuffle so the guaranteed chars are not at fixed positions
        for (int i = chars.Count - 1; i > 0; i--)
        {
            int j = SecureIndex(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }

        return new string(chars.ToArray());
    }

    /// <summary>Rough strength estimate based on length and variety.</summary>
    public static string Strength(int length, int classes)
    {
        double bits = length * Math.Log2(classes switch
        {
            1 => 26,
            2 => 52,
            3 => 62,
            _ => 90
        });

        return bits switch
        {
            < 40 => $"Weak   ({bits:0} bits)",
            < 60 => $"Fair   ({bits:0} bits)",
            < 80 => $"Strong ({bits:0} bits)",
            _    => $"Very strong ({bits:0} bits)"
        };
    }
}

internal static class Program
{
    private static int AskInt(string prompt, int min, int max, int fallback)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            if (int.TryParse(input, out int v) && v >= min && v <= max) return v;
            Console.WriteLine($"  ! Enter a number between {min} and {max}.");
        }
    }

    private static bool AskYesNo(string prompt, bool fallback)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            if (input.StartsWith("y", StringComparison.OrdinalIgnoreCase)) return true;
            if (input.StartsWith("n", StringComparison.OrdinalIgnoreCase)) return false;
            Console.WriteLine("  ! Answer y or n.");
        }
    }

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║     PASSWORD GENERATOR  -  C#            ║");
        Console.WriteLine("║     cryptographically secure             ║");
        Console.WriteLine("╚══════════════════════════════════════════╝\n");

        while (true)
        {
            int length    = AskInt("Length (4-128) [16]: ", 4, 128, 16);
            bool upper    = AskYesNo("Include uppercase? (y/n) [y]: ", true);
            bool digits   = AskYesNo("Include digits?    (y/n) [y]: ", true);
            bool symbols  = AskYesNo("Include symbols?   (y/n) [y]: ", true);

            int classes = 1 + (upper ? 1 : 0) + (digits ? 1 : 0) + (symbols ? 1 : 0);

            string password = Generator.Generate(length, upper, digits, symbols);

            Console.WriteLine($"\n  Password : {password}");
            Console.WriteLine($"  Strength : {Generator.Strength(length, classes)}\n");

            if (!AskYesNo("Generate another? (y/n) [y]: ", true)) break;
            Console.WriteLine();
        }

        Console.WriteLine("Goodbye.");
    }
}
