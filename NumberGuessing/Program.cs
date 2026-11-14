// ============================================================
//  Number Guessing Game — C#
//  Author: Mahmoud Saleh
// ============================================================
namespace NumberGuessing;

internal static class Program
{
    private static int ReadGuess(int low, int high)
    {
        while (true)
        {
            Console.Write($"Your guess ({low}-{high}): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value >= low && value <= high)
                return value;
            Console.WriteLine($"  ! Enter a number between {low} and {high}.");
        }
    }

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║     NUMBER GUESSING GAME                 ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        var rng = new Random();
        int bestScore = int.MaxValue;

        while (true)
        {
            int target = rng.Next(1, 101);
            int attempts = 0;
            int low = 1, high = 100;
            var guessed = new List<int>();

            Console.WriteLine("\nI picked a number between 1 and 100. Can you find it?");

            while (true)
            {
                int guess = ReadGuess(low, high);
                attempts++;

                if (guessed.Contains(guess))
                {
                    Console.WriteLine("  ! You already tried that number.");
                    continue;
                }
                guessed.Add(guess);

                if (guess < target)
                {
                    Console.WriteLine("  ↑ Too low.");
                    low = Math.Max(low, guess + 1);
                }
                else if (guess > target)
                {
                    Console.WriteLine("  ↓ Too high.");
                    high = Math.Min(high, guess - 1);
                }
                else
                {
                    Console.WriteLine($"\n  ✓ Correct! The number was {target}.");
                    Console.WriteLine($"  You needed {attempts} attempt(s).");

                    if (attempts < bestScore) bestScore = attempts;
                    Console.WriteLine($"  Best score this session: {bestScore}");

                    string rating = attempts <= 5 ? "Excellent"
                                  : attempts <= 8 ? "Good"
                                  : "Keep practising";
                    Console.WriteLine($"  Rating: {rating}");
                    break;
                }
            }

            Console.Write("\nPlay again? (y/n): ");
            var answer = Console.ReadLine();
            if (answer is null || !answer.StartsWith("y", StringComparison.OrdinalIgnoreCase))
                break;
        }

        Console.WriteLine("\nThanks for playing!");
    }
}
