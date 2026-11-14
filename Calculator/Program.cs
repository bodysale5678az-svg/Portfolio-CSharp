// ============================================================
//  Calculator — expression evaluator in C#
//  Author: Mahmoud Saleh
// ============================================================
namespace Calculator;

/// <summary>Base node of an arithmetic expression tree.</summary>
public abstract class Node
{
    public abstract double Evaluate();
    public abstract override string ToString();
}

/// <summary>A literal number.</summary>
public sealed class NumberNode : Node
{
    private readonly double _value;
    public NumberNode(double value) => _value = value;
    public override double Evaluate() => _value;
    public override string ToString() => _value.ToString("0.####");
}

/// <summary>A binary operation (+ - * / ^).</summary>
public sealed class BinaryNode : Node
{
    private readonly char _op;
    private readonly Node _left, _right;

    public BinaryNode(char op, Node left, Node right) => (_op, _left, _right) = (op, left, right);

    public override double Evaluate()
    {
        double a = _left.Evaluate();
        double b = _right.Evaluate();

        return _op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => b == 0 ? throw new DivideByZeroException("Division by zero") : a / b,
            '^' => Math.Pow(a, b),
            _ => throw new InvalidOperationException($"Unknown operator {_op}")
        };
    }

    public override string ToString() => $"({_left} {_op} {_right})";
}

/// <summary>Unary minus.</summary>
public sealed class NegateNode : Node
{
    private readonly Node _operand;
    public NegateNode(Node operand) => _operand = operand;
    public override double Evaluate() => -_operand.Evaluate();
    public override string ToString() => $"-{_operand}";
}

/// <summary>
/// Recursive-descent parser.
/// Grammar: expression := term (('+'|'-') term)*
///          term       := power (('*'|'/') power)*
///          power      := unary ('^' unary)*   (right-assoc)
///          unary      := '-' unary | primary
///          primary    := number | '(' expression ')'
/// </summary>
public sealed class Parser
{
    private readonly string _src;
    private int _pos;

    public Parser(string input) => _src = input;

    public Node Parse()
    {
        var node = Expression();
        SkipSpaces();
        if (_pos != _src.Length)
            throw new FormatException($"Unexpected character at position {_pos}");
        return node;
    }

    private void SkipSpaces()
    {
        while (_pos < _src.Length && char.IsWhiteSpace(_src[_pos])) _pos++;
    }

    private bool Match(char c)
    {
        SkipSpaces();
        if (_pos < _src.Length && _src[_pos] == c) { _pos++; return true; }
        return false;
    }

    private Node Expression()
    {
        var node = Term();
        while (true)
        {
            if (Match('+')) node = new BinaryNode('+', node, Term());
            else if (Match('-')) node = new BinaryNode('-', node, Term());
            else break;
        }
        return node;
    }

    private Node Term()
    {
        var node = Power();
        while (true)
        {
            if (Match('*')) node = new BinaryNode('*', node, Power());
            else if (Match('/')) node = new BinaryNode('/', node, Power());
            else break;
        }
        return node;
    }

    private Node Power()
    {
        var baseNode = Unary();
        if (Match('^')) return new BinaryNode('^', baseNode, Power());
        return baseNode;
    }

    private Node Unary()
    {
        if (Match('-')) return new NegateNode(Unary());
        if (Match('+')) return Unary();
        return Primary();
    }

    private Node Primary()
    {
        SkipSpaces();
        if (Match('('))
        {
            var inner = Expression();
            if (!Match(')')) throw new FormatException("Missing closing parenthesis");
            return inner;
        }

        SkipSpaces();
        int start = _pos;
        while (_pos < _src.Length && (char.IsDigit(_src[_pos]) || _src[_pos] == '.')) _pos++;

        if (start == _pos) throw new FormatException($"Expected a number at position {_pos}");

        var text = _src[start.._pos];
        if (!double.TryParse(text, out double value))
            throw new FormatException($"Invalid number '{text}'");
        return new NumberNode(value);
    }
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║     CALCULATOR  -  C#                    ║");
        Console.WriteLine("║     + - * / ^ and parentheses            ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine("Type an expression. Commands: q quit, h help\n");

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line is null) break;

            line = line.Trim();
            if (line.Length == 0) continue;
            if (line is "q" or "quit" or "exit") break;
            if (line is "h" or "help")
            {
                Console.WriteLine("Operators: + - * / ^  and ( )");
                Console.WriteLine("^ is right-associative: 2^3^2 = 512\n");
                continue;
            }

            try
            {
                var tree = new Parser(line).Parse();
                Console.WriteLine($"  {tree}");
                Console.WriteLine($"  = {tree.Evaluate():0.##########}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ! Error: {ex.Message}\n");
            }
        }

        Console.WriteLine("Goodbye.");
    }
}
