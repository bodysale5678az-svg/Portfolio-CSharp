# 🎯 Number Guessing Game — C#

A polished console guessing game with adaptive bounds, duplicate detection and a performance rating.

```
I picked a number between 1 and 100. Can you find it?
Your guess (1-100): 50
  ↑ Too low.
Your guess (51-100): 75
  ↓ Too high.
...
```

---

## ✨ Features

- Random target between **1 and 100**
- **Adaptive range** — bounds shrink after each guess so bad input is impossible
- **Duplicate detection** — warns when you repeat a number
- **Attempt counter** with rating (Excellent / Good / Keep practising)
- **Best-score tracking** across rounds in one session
- Replay loop

---

## 🧠 What it demonstrates

- `Random` and input validation
- `List<T>` for history
- `Math.Max` / `Math.Min` for tightening bounds
- `string.StartsWith(..., StringComparison.OrdinalIgnoreCase)`
- A clean game loop with state

---

## 🚀 Run

```bash
dotnet run
```

---

## 👤 Author

**Mahmoud Saleh** — [@bodysale5678az-svg](https://github.com/bodysale5678az-svg)
