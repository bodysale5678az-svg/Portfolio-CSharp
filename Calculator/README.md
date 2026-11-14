# 🧮 Calculator — C#

A command-line calculator that evaluates **full arithmetic expressions** with correct operator precedence and parentheses, built with a **recursive-descent parser** and an **expression tree**.

```
> 2 + 3 * 4
  (2 + (3 * 4))
  = 14
```

---

## ✨ Features

- `+ - * / ^` and parentheses
- Correct precedence: `^` > `* /` > `+ -`
- Right-associative power: `2^3^2 = 512`
- Unary minus: `-(5 + 5) = -10`
- Clean error messages (division by zero, bad syntax)
- Prints the parsed tree so you can *see* the parse

---

## 🏗️ Design

| Type | Responsibility |
|---|---|
| `Node` (abstract) | `Evaluate()` + `ToString()` |
| `NumberNode` | Literal value |
| `BinaryNode` | `+ - * / ^` with two children |
| `NegateNode` | Unary minus |
| `Parser` | Text → `Node` tree |

**Modern C# used:** abstract classes, sealed classes, pattern matching (`switch` expressions), tuples, `TryParse`, local functions.

```
Grammar:
  expression := term (('+'|'-') term)*
  term       := power (('*'|'/') power)*
  power      := unary ('^' unary)*        (right-assoc)
  unary      := '-' unary | primary
  primary    := number | '(' expression ')'
```

---

## 🚀 Run

```bash
dotnet run
```

---

## 💡 Examples

| Input | Result |
|---|---|
| `2 + 3 * 4` | 14 |
| `(2 + 3) * 4` | 20 |
| `2 ^ 3 ^ 2` | 512 |
| `-(5 + 5)` | -10 |
| `1 / 0` | error |

---

## 👤 Author

**Mahmoud Saleh** — [@bodysale5678az-svg](https://github.com/bodysale5678az-svg)
