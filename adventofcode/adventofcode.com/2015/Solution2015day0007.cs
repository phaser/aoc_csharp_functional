
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0007
{
    private record Register(bool IsNumber, int Number, string Reg = "");

    public record Instruction();
    public record NumberAssignment(int num, string dest) : Instruction;

    private record RegisterAssignment(string reg1, string dest) : Instruction;

    private record UnaryOperation(OpUnaryOperation operation, Register reg1, string dest) : Instruction;

    private record BinaryOperation(OpBinaryOperation operation, Register reg1, Register reg2, string dest) : Instruction;

    private enum OpUnaryOperation
    {
        NOT
    }

    private enum OpBinaryOperation
    {
        OR,
        RSHIFT,
        LSHIFT,
        AND
    }

    private static List<string> OpBinaryList = new(4)
    {
        OpBinaryOperation.OR.ToString(),
        OpBinaryOperation.RSHIFT.ToString(),
        OpBinaryOperation.LSHIFT.ToString(),
        OpBinaryOperation.AND.ToString(),
    };

    private record LocalEnvironment(Dictionary<string, int> Environment, List<Instruction> Instructions);
    
    public static Dictionary<string, int> CreateEnvironment()
        => new();

    public static int Solve(string input, bool overrideB = false)
        => new LocalEnvironment(CreateEnvironment(), ParseInstructions(input))
            .And(ExecuteNumberAssignments)
            .And(env => overrideB 
                ? env.Modify(e => e.Environment["b"] = 16076)
                : env)
            .And(SolveInternal);

    public static Instruction ParseInstruction(string instruction)
        => instruction.Split(' ')
            .And(ia => ia.Length switch
            {
                3 => char.IsDigit(ia[0][0])
                    ? new NumberAssignment(int.Parse(ia[0]), ia[2]) as Instruction
                    : new RegisterAssignment(ia[0], ia[2]),
                4 => new UnaryOperation(ia[0].MapToUnary(), ia[1].MapToRegister(), ia[3]),
                5 => new BinaryOperation(ia[1].MapToBinary(), ia[0].MapToRegister(), ia[2].MapToRegister(), ia[4]),
                _ => throw new NotImplementedException($"'{instruction}' not handled")
            });

    private static int SolveInternal(LocalEnvironment env)
    {
        while (!env.Environment.ContainsKey("a") && env.Instructions.Count > 0)
        {
            var usableInstructions = env.Instructions.Where(i => IsExecutable(env.Environment, i)).ToList();
            env.Instructions.RemoveAll(i => IsExecutable(env.Environment, i));
            foreach (var instruction in usableInstructions)
                Execute(env.Environment, instruction);
        }
        return env.Environment["a"];
    }

    private static LocalEnvironment ExecuteNumberAssignments(LocalEnvironment env)
        => env.Instructions.OfType<NumberAssignment>()
            .Select(i => Execute(env.Environment, i))
            .ToList()
            .And(_ => env.Modify(e => e.Instructions.RemoveAll(i => i is NumberAssignment)));

    private static List<Instruction> ParseInstructions(string input)
    {
        var instructions = input
            .Split("\r\n")
            .Select(i => i.Trim())
            .Select(ParseInstruction)
            .ToList();
        return instructions;
    }

    private static int Execute(Dictionary<string, int> env, Instruction instruction)
        => instruction is RegisterAssignment
            ? Execute(env, (instruction as RegisterAssignment)!)
            : instruction is UnaryOperation
                ? Execute(env, (instruction as UnaryOperation)!)
                : instruction is BinaryOperation
                    ? Execute(env, (instruction as BinaryOperation)!)
                    : throw new NotImplementedException($"Is executable for this type {instruction.GetType().Name} isn't supported");


    private static int Execute(Dictionary<string, int> env, NumberAssignment instruction)
    {
        env.Add(instruction.dest, instruction.num);
        return 0;
    }

    private static int Execute(Dictionary<string, int> env, RegisterAssignment instruction)
        => env.ContainsKey(instruction.reg1)
            .And(containsKey =>
            {
                if (!containsKey)
                    throw new ArgumentException(
                        $"Invalid instruction RegisterAssignment {instruction.reg1} -> {instruction.dest}");
                env[instruction.dest] = env[instruction.reg1];
                return 1;
            });

    private static int Execute(Dictionary<string, int> env, UnaryOperation instruction)
        => instruction.reg1.IsNumber
            ? env[instruction.dest] = ~instruction.reg1.Number
            : env[instruction.dest] = ~env[instruction.reg1.Reg];

    private static int Execute(Dictionary<string, int> env, BinaryOperation instruction)
        => instruction.operation switch
        {
            OpBinaryOperation.OR => env[instruction.dest] =
                instruction.reg1.GetValue(env) | instruction.reg2.GetValue(env),
            OpBinaryOperation.AND => env[instruction.dest] =
                instruction.reg1.GetValue(env) & instruction.reg2.GetValue(env),
            OpBinaryOperation.LSHIFT => env[instruction.dest] =
                instruction.reg1.GetValue(env) << instruction.reg2.GetValue(env),
            OpBinaryOperation.RSHIFT => env[instruction.dest] =
                instruction.reg1.GetValue(env) >> instruction.reg2.GetValue(env)
        };

    private static bool IsExecutable(Dictionary<string, int> env, Instruction instruction)
        => instruction is RegisterAssignment
            ? IsExecutable(env, (instruction as RegisterAssignment)!)
            : instruction is UnaryOperation
                ? IsExecutable(env, (instruction as UnaryOperation)!)
                : instruction is BinaryOperation
                    ? IsExecutable(env, (instruction as BinaryOperation)!)
                    : throw new NotImplementedException(
                        $"Is executable for this type {instruction.GetType().Name} isn't supported");

    private static bool IsExecutable(Dictionary<string, int> env, RegisterAssignment instruction)
        => env.ContainsKey(instruction.reg1);

    private static bool IsExecutable(Dictionary<string, int> env, UnaryOperation instruction)
        => instruction.reg1.IsNumber || (!instruction.reg1.IsNumber && env.ContainsKey(instruction.reg1.Reg));

    private static bool IsExecutable(Dictionary<string, int> env, BinaryOperation instruction)
        => (instruction.reg1.IsNumber || (!instruction.reg1.IsNumber && env.ContainsKey(instruction.reg1.Reg)))
    && (instruction.reg2.IsNumber || (!instruction.reg2.IsNumber && env.ContainsKey(instruction.reg2.Reg)));
    
    private static OpUnaryOperation MapToUnary(this string op)
        => op.Trim() switch
        {
            "NOT" => OpUnaryOperation.NOT,
            _ => throw new NotImplementedException($"{op} not handled")
        };

    private static OpBinaryOperation MapToBinary(this string op)
        => op.Trim() switch 
        {
            "OR" => OpBinaryOperation.OR,
            "LSHIFT" => OpBinaryOperation.LSHIFT,
            "RSHIFT" => OpBinaryOperation.RSHIFT,
            "AND" => OpBinaryOperation.AND,
            _ => throw new NotImplementedException($"{op} not handled")
        };

    private static Register MapToRegister(this string reg)
        => int.TryParse(reg, out var regValue)
            ? new Register(true, regValue)
            : new Register(false, 0, reg);

    private static int GetValue(this Register reg, Dictionary<string, int> env)
        => reg.IsNumber
            ? reg.Number
            : env[reg.Reg];
}
