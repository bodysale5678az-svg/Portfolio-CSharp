# 🔐 Password Generator — C#

A **cryptographically secure** password generator written in C#.

```
Length (4-128) [16]:
Include uppercase? (y/n) [y]:
Include digits?    (y/n) [y]:
Include symbols?   (y/n) [y]:

  Password : k$9Tv]2mQa#Le7Xr
  Strength : Very strong (105 bits)
```

---

## ✨ Features

- Uses **`RandomNumberGenerator`** (CSPRNG) — **not** `Random`
- **Rejection sampling** to avoid modulo bias
- Guarantees **at least one character from every selected class**
- **Fisher–Yates shuffle** so guaranteed characters aren't in fixed positions
- Configurable length (4–128) and character classes
- Entropy-based strength estimate
- Interactive prompts with sensible defaults

---

## 🧠 Security details

| Concern | How it's handled |
|---|---|
| Predictable randomness | `RandomNumberGenerator.Fill` instead of `Random` |
| Modulo bias | Rejection sampling loop |
| Weak composition | One char per class forced, then shuffled |
| Short passwords | Minimum length enforced (4) |

> ⚠️ This is a learning project — for production systems prefer a password **manager**, and never log generated passwords.

---

## 🚀 Run

```bash
dotnet run
```

---

## 👤 Author

**Mahmoud Saleh** — [@bodysale5678az-svg](https://github.com/bodysale5678az-svg)
