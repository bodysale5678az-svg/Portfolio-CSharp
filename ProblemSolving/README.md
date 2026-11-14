# 🧩 Problem Solving — C#

![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)
![Tests](https://img.shields.io/badge/tests-64%20passing-brightgreen?style=flat-square)

A collection of **25 classic algorithm problems** in modern C#, each checked by a built-in test runner.

```
==============================================
  PASSED: 64   FAILED: 0
==============================================
```

---

## 📚 Problems

### Arrays & searching
| # | Problem | Technique | Complexity |
|---|---|---|---|
| 1 | Two Sum | Dictionary | O(n) |
| 8 | Binary search | Divide & conquer | O(log n) |
| 15 | Max subarray sum | Kadane | O(n) |
| 16 | Move zeros to end | Two pointers | O(n) |
| 17 | Remove duplicates (sorted) | Two pointers | O(n) |
| 22 | Second largest | Single pass | O(n) |
| 23 | Rotate left by k | Triple reverse | O(n) |
| 24 | Missing number | Arithmetic sum | O(n) |

### Strings
| # | Problem | Technique | Complexity |
|---|---|---|---|
| 2 | Palindrome | Two pointers | O(n) |
| 6 | Reverse string | `Array.Reverse` | O(n) |
| 7 | Anagram | Frequency array | O(n) |
| 9 | Balanced parentheses | `Stack<T>` | O(n) |
| 10 | Longest common prefix | Incremental shrink | O(n·m) |
| 13 | Count vowels | `string.Contains` + LINQ | O(n) |
| 19 | Count words | `Split` | O(n) |
| 20 | Capitalize words | LINQ projection | O(n) |
| 25 | Longest word | LINQ ordering | O(n log n) |

### Math
| # | Problem | Technique | Complexity |
|---|---|---|---|
| 3 | FizzBuzz | Conditionals | O(n) |
| 4 | Fibonacci (memoised) | Dictionary DP | O(n) |
| 5 | Factorial | Iteration | O(n) |
| 11 | GCD | Euclid | O(log n) |
| 12 | Fibonacci (iterative) | Tuple swap | O(n) |
| 14 | Sum of digits | Modulo loop | O(log n) |
| 18 | Is prime | Trial division | O(√n) |
| 21 | Power | Fast exponentiation | O(log n) |

---

## ✨ Modern C# used

- **Tuples** — `(int First, int Second) TwoSum(...)`
- **Switch expressions** and pattern matching (`is '(' or '[' or '{'`)
- **Range/index operators** — `v[..n]`, `w[1..]`
- **Tuple deconstruction** — `(a, b) = (b, a % b)`
- **Local functions** — inside `Fibonacci`
- **LINQ** — `.Count`, `.Sum`, `.Select`, `.OrderByDescending`
- `Environment.Exit` for CI-friendly results

---

## 🚀 Run

```bash
dotnet run
```

Exit code is `0` when every test passes, `1` otherwise — so it can run in CI.

---

## 👤 Author

**Mahmoud Saleh** — [@bodysale5678az-svg](https://github.com/bodysale5678az-svg)
