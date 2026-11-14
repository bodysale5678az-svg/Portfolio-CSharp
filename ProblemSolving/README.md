# 🧩 Problem Solving — C#

A collection of **11 classic algorithm problems** in modern C#, each checked by a built-in test runner.

```
==============================================
  PASSED: 29   FAILED: 0
==============================================
```

---

## 📚 Problems

| # | Problem | Technique | Complexity |
|---|---|---|---|
| 1 | Two Sum | Dictionary | O(n) |
| 2 | Palindrome | Two pointers | O(n) |
| 3 | FizzBuzz | Conditionals | O(n) |
| 4 | Fibonacci | Memoisation | O(n) |
| 5 | Factorial | Iteration | O(n) |
| 6 | Reverse string | `Array.Reverse` | O(n) |
| 7 | Anagram | Frequency array | O(n) |
| 8 | Binary search | Divide & conquer | O(log n) |
| 9 | Balanced parentheses | `Stack<T>` | O(n) |
| 10 | Longest common prefix | Incremental shrink | O(n·m) |
| 11 | GCD | Euclid | O(log n) |

---

## ✨ Modern C# features used

- **Tuples** — `(int First, int Second) TwoSum(...)`
- **Switch expressions** and pattern matching (`is '(' or '[' or '{'`)
- **Range/index operators** — `prefix[..j]`
- **Tuple deconstruction** — `(a, b) = (b, a % b)`
- **Local functions** — inside `Fibonacci`
- **LINQ** — `.All(...)`, `.First`
- Nullable-aware, `Environment.Exit` for CI-friendly results

---

## 🚀 Run

```bash
dotnet run
```

Exit code is `0` when every test passes, `1` otherwise — so it can run in CI.

---

## 👤 Author

**Mahmoud Saleh** — [@bodysale5678az-svg](https://github.com/bodysale5678az-svg)
