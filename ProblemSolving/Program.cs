// ============================================================
//  Problem Solving — C# algorithm collection with self-tests
//  Author: Mahmoud Saleh
// ============================================================
namespace ProblemSolving;

/// <summary>Collection of classic algorithm problems as pure static methods.</summary>
public static class Algorithms
{
    /// <summary>Indices of the two numbers that add up to target. O(n).</summary>
    public static (int First, int Second) TwoSum(int[] nums, int target)
    {
        var seen = new Dictionary<int, int>();   // value -> index
        for (int i = 0; i < nums.Length; i++)
        {
            int need = target - nums[i];
            if (seen.TryGetValue(need, out int j))
                return (j, i);
            seen[nums[i]] = i;
        }
        return (-1, -1);
    }

    /// <summary>True when the text reads the same forwards and backwards (ignoring punctuation/case).</summary>
    public static bool IsPalindrome(string s)
    {
        int i = 0, j = s.Length - 1;
        while (i < j)
        {
            while (i < j && !char.IsLetterOrDigit(s[i])) i++;
            while (i < j && !char.IsLetterOrDigit(s[j])) j--;
            if (char.ToLowerInvariant(s[i]) != char.ToLowerInvariant(s[j])) return false;
            i++; j--;
        }
        return true;
    }

    /// <summary>Classic FizzBuzz sequence.</summary>
    public static string[] FizzBuzz(int n)
    {
        var result = new string[n];
        for (int i = 1; i <= n; i++)
        {
            result[i - 1] = (i % 15 == 0) ? "FizzBuzz"
                          : (i % 3 == 0)  ? "Fizz"
                          : (i % 5 == 0)  ? "Buzz"
                                          : i.ToString();
        }
        return result;
    }

    /// <summary>n-th Fibonacci number using memoisation. O(n).</summary>
    public static long Fibonacci(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        return Memo(n);

        static long Memo(int k)
        {
            var memo = new Dictionary<int, long> { [0] = 0, [1] = 1 };
            for (int i = 2; i <= k; i++) memo[i] = memo[i - 1] + memo[i - 2];
            return memo[k];
        }
    }

    /// <summary>Factorial (iterative).</summary>
    public static long Factorial(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        long r = 1;
        for (int i = 2; i <= n; i++) r *= i;
        return r;
    }

    /// <summary>Reversed copy of the string.</summary>
    public static string Reverse(string s)
    {
        var chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>True when both strings use the same letters the same number of times.</summary>
    public static bool IsAnagram(string a, string b)
    {
        if (a.Length != b.Length) return false;
        var count = new int[26];
        foreach (char c in a)
            if (char.IsLetter(c)) count[char.ToLowerInvariant(c) - 'a']++;
        foreach (char c in b)
            if (char.IsLetter(c)) count[char.ToLowerInvariant(c) - 'a']--;
        return count.All(v => v == 0);
    }

    /// <summary>Index of target in a sorted array, or -1. O(log n).</summary>
    public static int BinarySearch(int[] sorted, int target)
    {
        int lo = 0, hi = sorted.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (sorted[mid] == target) return mid;
            if (sorted[mid] < target) lo = mid + 1;
            else hi = mid - 1;
        }
        return -1;
    }

    /// <summary>True when all brackets are properly nested.</summary>
    public static bool BalancedParentheses(string s)
    {
        var stack = new Stack<char>();
        foreach (char c in s)
        {
            if (c is '(' or '[' or '{') stack.Push(c);
            else if (c is ')' or ']' or '}')
            {
                if (stack.Count == 0) return false;
                char top = stack.Pop();
                if ((c == ')' && top != '(') ||
                    (c == ']' && top != '[') ||
                    (c == '}' && top != '{')) return false;
            }
        }
        return stack.Count == 0;
    }

    /// <summary>Longest prefix shared by every word.</summary>
    public static string LongestCommonPrefix(string[] words)
    {
        if (words.Length == 0) return "";
        string prefix = words[0];
        for (int i = 1; i < words.Length; i++)
        {
            int j = 0;
            while (j < prefix.Length && j < words[i].Length && prefix[j] == words[i][j]) j++;
            prefix = prefix[..j];
            if (prefix.Length == 0) break;
        }
        return prefix;
    }

    /// <summary>Greatest common divisor via Euclid's algorithm.</summary>
    public static int Gcd(int a, int b)
    {
        a = Math.Abs(a); b = Math.Abs(b);
        while (b != 0) (a, b) = (b, a % b);
        return a;
    }
}

internal static class Program
{
    private static int _passed, _failed;

    private static void Check<T>(string name, T actual, T expected)
    {
        if (EqualityComparer<T>.Default.Equals(actual, expected))
        {
            _passed++;
            Console.WriteLine($"  [PASS] {name}");
        }
        else
        {
            _failed++;
            Console.WriteLine($"  [FAIL] {name}");
            Console.WriteLine($"         expected: {expected}");
            Console.WriteLine($"         actual  : {actual}");
        }
    }

    private static void Header(string title) => Console.WriteLine($"\n--- {title} ---");

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║  PROBLEM SOLVING  -  C# test suite       ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");

        Header("1. Two Sum");
        var (a1, b1) = Algorithms.TwoSum(new[] { 2, 7, 11, 15 }, 9);
        Check("twoSum([2,7,11,15], 9) first", a1, 0);
        Check("twoSum([2,7,11,15], 9) second", b1, 1);
        Check("no solution -> -1", Algorithms.TwoSum(new[] { 1, 2, 3 }, 100).First, -1);

        Header("2. Palindrome");
        Check("'racecar'", Algorithms.IsPalindrome("racecar"), true);
        Check("'A man, a plan, a canal: Panama'", Algorithms.IsPalindrome("A man, a plan, a canal: Panama"), true);
        Check("'hello'", Algorithms.IsPalindrome("hello"), false);

        Header("3. FizzBuzz");
        var fb = Algorithms.FizzBuzz(15);
        Check("n=3  -> Fizz", fb[2], "Fizz");
        Check("n=5  -> Buzz", fb[4], "Buzz");
        Check("n=15 -> FizzBuzz", fb[14], "FizzBuzz");
        Check("length 15", fb.Length, 15);

        Header("4. Fibonacci");
        Check("fib(0)", Algorithms.Fibonacci(0), 0L);
        Check("fib(10)", Algorithms.Fibonacci(10), 55L);
        Check("fib(50)", Algorithms.Fibonacci(50), 12586269025L);

        Header("5. Factorial");
        Check("5!", Algorithms.Factorial(5), 120L);
        Check("0!", Algorithms.Factorial(0), 1L);
        Check("10!", Algorithms.Factorial(10), 3628800L);

        Header("6. Reverse");
        Check("'hello'", Algorithms.Reverse("hello"), "olleh");

        Header("7. Anagram");
        Check("'listen' / 'silent'", Algorithms.IsAnagram("listen", "silent"), true);
        Check("'hello' / 'world'", Algorithms.IsAnagram("hello", "world"), false);

        Header("8. Binary Search");
        Check("7 in [1,3,5,7,9]", Algorithms.BinarySearch(new[] { 1, 3, 5, 7, 9 }, 7), 3);
        Check("1 (first)", Algorithms.BinarySearch(new[] { 1, 3, 5, 7, 9 }, 1), 0);
        Check("100 (missing)", Algorithms.BinarySearch(new[] { 1, 3, 5, 7, 9 }, 100), -1);

        Header("9. Balanced Parentheses");
        Check("'()[]{}'", Algorithms.BalancedParentheses("()[]{}"), true);
        Check("'{[()()]}'", Algorithms.BalancedParentheses("{[()()]}"), true);
        Check("'([)]'", Algorithms.BalancedParentheses("([)]"), false);

        Header("10. Longest Common Prefix");
        Check("[flower,flow,flight]", Algorithms.LongestCommonPrefix(new[] { "flower", "flow", "flight" }), "fl");
        Check("[dog,cat]", Algorithms.LongestCommonPrefix(new[] { "dog", "cat" }), "");

        Header("11. GCD");
        Check("gcd(48,18)", Algorithms.Gcd(48, 18), 6);
        Check("gcd(17,5)", Algorithms.Gcd(17, 5), 1);

        Console.WriteLine("\n==============================================");
        Console.WriteLine($"  PASSED: {_passed}   FAILED: {_failed}");
        Console.WriteLine("==============================================\n");

        Environment.Exit(_failed == 0 ? 0 : 1);
    }
}
