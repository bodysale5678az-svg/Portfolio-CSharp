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

    // ---------- 12. Fibonacci (iterative) ----------
    /// <summary>n-th Fibonacci number computed iteratively.</summary>
    public static long FibIterative(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        long a = 0, b = 1;
        for (int i = 0; i < n; i++) (a, b) = (b, a + b);
        return a;
    }

    // ---------- 13. Count vowels ----------
    public static int CountVowels(string s)
        => s.Count(c => "aeiouAEIOU".Contains(c));

    // ---------- 14. Sum of digits ----------
    public static int SumOfDigits(int n)
    {
        n = Math.Abs(n);
        int sum = 0;
        while (n > 0) { sum += n % 10; n /= 10; }
        return sum;
    }

    // ---------- 15. Max subarray sum (Kadane) ----------
    public static long MaxSubarraySum(int[] nums)
    {
        if (nums.Length == 0) throw new ArgumentException("empty array", nameof(nums));
        long best = nums[0], current = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            current = Math.Max(nums[i], current + nums[i]);
            best = Math.Max(best, current);
        }
        return best;
    }

    // ---------- 16. Move zeros to end ----------
    public static void MoveZeros(int[] nums)
    {
        int write = 0;
        for (int read = 0; read < nums.Length; read++)
            if (nums[read] != 0) (nums[write], nums[read]) = (nums[read], nums[write++]);
    }

    // ---------- 17. Remove duplicates (sorted) ----------
    public static int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 0) return 0;
        int write = 1;
        for (int read = 1; read < nums.Length; read++)
            if (nums[read] != nums[write - 1]) nums[write++] = nums[read];
        return write;
    }

    // ---------- 18. Is prime ----------
    public static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n < 4) return true;
        if (n % 2 == 0) return false;
        for (int i = 3; (long)i * i <= n; i += 2)
            if (n % i == 0) return false;
        return true;
    }

    // ---------- 19. Count words ----------
    public static int CountWords(string s)
        => s.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

    // ---------- 20. Capitalize words ----------
    public static string CapitalizeWords(string s)
        => string.Join(' ', s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .Select(w => char.ToUpperInvariant(w[0]) + w[1..]));

    // ---------- 21. Power (fast exponentiation) ----------
    public static long Power(long baseValue, int exp)
    {
        if (exp < 0) throw new ArgumentOutOfRangeException(nameof(exp));
        long result = 1;
        while (exp > 0)
        {
            if ((exp & 1) == 1) result *= baseValue;
            baseValue *= baseValue;
            exp >>= 1;
        }
        return result;
    }

    // ---------- 22. Second largest ----------
    public static int SecondLargest(int[] nums)
    {
        if (nums.Length < 2) throw new ArgumentException("need at least 2 elements", nameof(nums));
        int first = int.MinValue, second = int.MinValue;
        foreach (int n in nums)
        {
            if (n > first) { second = first; first = n; }
            else if (n > second && n != first) second = n;
        }
        return second;
    }

    // ---------- 23. Rotate left ----------
    public static void RotateLeft(int[] nums, int k)
    {
        if (nums.Length == 0) return;
        int n = nums.Length;
        k %= n;
        Array.Reverse(nums, 0, k);
        Array.Reverse(nums, k, n - k);
        Array.Reverse(nums);
    }

    // ---------- 24. Missing number 0..n ----------
    public static int MissingNumber(int[] nums)
    {
        int n = nums.Length;
        int expected = n * (n + 1) / 2;
        return expected - nums.Sum();
    }

    // ---------- 25. Longest word ----------
    public static string LongestWord(string s)
        => s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .OrderByDescending(w => w.Length)
            .FirstOrDefault() ?? string.Empty;
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

        Header("12. Fibonacci (iterative)");
        Check("fibIter(0)", Algorithms.FibIterative(0), 0L);
        Check("fibIter(10)", Algorithms.FibIterative(10), 55L);
        Check("fibIter(30)", Algorithms.FibIterative(30), 832040L);
        Check("matches memo fib(20)", Algorithms.FibIterative(20), Algorithms.Fibonacci(20));

        Header("13. Count vowels");
        Check("'hello' -> 2", Algorithms.CountVowels("hello"), 2);
        Check("'EDUCATION' -> 5", Algorithms.CountVowels("EDUCATION"), 5);
        Check("'xyz' -> 0", Algorithms.CountVowels("xyz"), 0);

        Header("14. Sum of digits");
        Check("12345", Algorithms.SumOfDigits(12345), 15);
        Check("999", Algorithms.SumOfDigits(999), 27);
        Check("-456", Algorithms.SumOfDigits(-456), 15);

        Header("15. Max subarray sum (Kadane)");
        Check("[-2,1,-3,4,-1,2,1,-5,4] = 6",
            Algorithms.MaxSubarraySum(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }), 6L);
        Check("[1,2,3,4] = 10", Algorithms.MaxSubarraySum(new[] { 1, 2, 3, 4 }), 10L);
        Check("[-5,-2,-9] = -2", Algorithms.MaxSubarraySum(new[] { -5, -2, -9 }), -2L);

        Header("16. Move zeros to end");
        {
            var v = new[] { 0, 1, 0, 3, 12 };
            Algorithms.MoveZeros(v);
            Check("-> 1,3,12,0,0", string.Join(",", v), "1,3,12,0,0");
        }

        Header("17. Remove duplicates (sorted)");
        {
            var v = new[] { 1, 1, 2, 2, 3, 4, 4 };
            int n = Algorithms.RemoveDuplicates(v);
            Check("unique count = 4", n, 4);
            Check("first 4 are 1,2,3,4", string.Join(",", v[..n]), "1,2,3,4");
        }

        Header("18. Is prime");
        Check("2", Algorithms.IsPrime(2), true);
        Check("97", Algorithms.IsPrime(97), true);
        Check("1", Algorithms.IsPrime(1), false);
        Check("100", Algorithms.IsPrime(100), false);

        Header("19. Count words");
        Check("'hello world' -> 2", Algorithms.CountWords("hello world"), 2);
        Check("spaces -> 3", Algorithms.CountWords("  the quick  brown  "), 3);
        Check("empty -> 0", Algorithms.CountWords(""), 0);

        Header("20. Capitalize words");
        Check("'hello world'", Algorithms.CapitalizeWords("hello world"), "Hello World");
        Check("'the quick brown'", Algorithms.CapitalizeWords("the quick brown"), "The Quick Brown");

        Header("21. Power (fast exponentiation)");
        Check("2^10", Algorithms.Power(2, 10), 1024L);
        Check("3^5", Algorithms.Power(3, 5), 243L);
        Check("5^0", Algorithms.Power(5, 0), 1L);

        Header("22. Second largest");
        Check("[3,1,4,1,5,9,2,6] -> 6",
            Algorithms.SecondLargest(new[] { 3, 1, 4, 1, 5, 9, 2, 6 }), 6);
        Check("[10,20,30] -> 20", Algorithms.SecondLargest(new[] { 10, 20, 30 }), 20);

        Header("23. Rotate left");
        {
            var v = new[] { 1, 2, 3, 4, 5 };
            Algorithms.RotateLeft(v, 2);
            Check("k=2 -> 3,4,5,1,2", string.Join(",", v), "3,4,5,1,2");
        }

        Header("24. Missing number");
        Check("[3,0,1] -> 2", Algorithms.MissingNumber(new[] { 3, 0, 1 }), 2);
        Check("[0,1,2,4] -> 3", Algorithms.MissingNumber(new[] { 0, 1, 2, 4 }), 3);

        Header("25. Longest word");
        Check("'the quick brown fox'", Algorithms.LongestWord("the quick brown fox"), "quick");
        Check("'I love programming'", Algorithms.LongestWord("I love programming"), "programming");

        Console.WriteLine("\n==============================================");
        Console.WriteLine($"  PASSED: {_passed}   FAILED: {_failed}");
        Console.WriteLine("==============================================\n");

        Environment.Exit(_failed == 0 ? 0 : 1);
    }
}
