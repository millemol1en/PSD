using PSD.Assignment_01;

namespace PSD_Csharp.Assignment_01;

// AUTHOR: Victor La Cuur
// Assignment 1.4

// Our prior data type in F#, expr, is now an abstract class.
abstract class Expr
{
    // Abstract methods promises to be used in its child classes.
    public abstract override string ToString();

    public abstract int eval (Dictionary<string, int> env);

    public abstract Expr simplify();
}

// It's either a constant ...
class CstI : Expr
{
    public int Value { get; }

    public CstI(int value)
    {
        Value = value;
    }

    public override int eval (Dictionary<string, int> env)
    {
        return Value;
    }

    public override Expr simplify()
    {
        return this; //no simplification for constants! 
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

// ... or a variable ...
class Var : Expr
{
    public string Name { get;}

    public Var(string name)
    {
        Name = name;
    }

    public override int eval(Dictionary<string, int> env)
    {
        return env[Name];
    }

    public override Expr simplify()
    {
        return this; //neither any simplifications for Vars
    }

    public override string ToString()
    {
        return Name;
    }
}

// The binary operations is with, fucking, either Expr or Var, y'know? 
abstract class Binop : Expr 
{
    public Expr Left { get; }
    public Expr Right { get; }

    protected Binop(Expr left, Expr right)
    {
        Left = left;
        Right = right;
    }
}

class Add : Binop
{
    public Add(Expr left, Expr right) : base(left, right) { }

    public override int eval(Dictionary<string, int> env)
    {
        var leftVal = Left.eval(env);
        var rightVal = Right.eval(env);
        return leftVal + rightVal;

    }

    public override Expr simplify()
    {
        var leftVal = Left.simplify();
        var rightVal = Right.simplify();

        if (leftVal is CstI l && l.Value == 0) return rightVal;
        if (rightVal is CstI r && r.Value == 0) return leftVal;

        return new Add(leftVal, rightVal);

    }

    public override string ToString()
    {
        return $"({Left} + {Right})";
    }
}

class Mul : Binop
{
    public Mul(Expr left, Expr right) : base(left, right) { }

    public override int eval(Dictionary<string, int> env)
    {
        var leftVal = Left.eval(env);
        var rightVal = Right.eval(env);
        return leftVal * rightVal;
    }

    public override Expr simplify()
    {
        var leftVal = Left.simplify();
        var rightVal = Right.simplify();

        if (leftVal is CstI { Value: 1 }) return rightVal; //funny alternative way to write it, Rider's own recommendation.
        if (rightVal is CstI r && r.Value == 1) return leftVal;
        if (leftVal is CstI ll && ll.Value == 0) return new CstI (0);
        if (rightVal is CstI rr && rr.Value == 0) return new CstI(0);
        return new Mul(leftVal, rightVal);

    }

    public override string ToString()
    {
        return $"({Left} * {Right})";
    }
}

// Add a method Expr simplify() that returns a new expression where al-
// gebraic simplifications have been performed, as in part (iv) of Exercise 1.2.



class Sub : Binop
{
    public Sub(Expr left, Expr right) : base(left, right) { }

    public override int eval(Dictionary<string, int> env)
    {
        var leftVal = Left.eval(env);
        var rightVal = Right.eval(env);
        return leftVal - rightVal;
    }

    public override Expr simplify()
    {
        var leftVal = Left.simplify();
        var rightVal = Right.simplify();
        
        //constnat check variables
        if (leftVal is CstI l && rightVal is CstI r && l.Value == r.Value) return new CstI(0);

        return new Sub(leftVal, rightVal);

    }

    public override string ToString()
    {
        return $"({Left} - {Right})";
    }
}

class Program
{
    static void Main()
    {
        Expr e1 = new Add(new CstI(17),new Sub(new Var("x"), new Var("y")));
        Expr e2 = new Mul(new CstI(17), new CstI(3));
        Expr e3 = new Sub(new Var("x"), new Var("y"));
        Console.WriteLine(e1.ToString());
        Console.WriteLine(e2.ToString());
        Console.WriteLine(e3.ToString());
        
        // stack machine
        var env = new Dictionary<string, int>
        {
            { "x", 6},
            { "y", 7}
        };
        
        // expr to be evaluated
        
        //NOTE: We look for variables in our env, if the variable is not in the env,
        // we die.
        Expr myExpr = new Add(new Var("x"), new CstI(10));
        
        // evaluated result
        var res = myExpr.eval(env);
        Console.WriteLine(res);
        
        Console.WriteLine("Check simplify: \n");

        Expr e5 = new Add(new CstI(0), new CstI(10));
        Expr e6 = new Add(new CstI(10), new CstI(0));

        Expr e7 = new Mul(new CstI(1), new CstI(5));
        Expr e8 = new Mul(new CstI(5), new CstI(1));
        Expr e9 = new Mul(new CstI(0), new CstI(5));
        Expr e10 = new Mul(new CstI(5), new CstI(0));

        Expr e11 = new Sub(new CstI(10), new CstI(10));

        Console.WriteLine(e5.simplify());
        Console.WriteLine(e6.simplify());
        Console.WriteLine(e7.simplify());
        Console.WriteLine(e8.simplify());
        Console.WriteLine(e9.simplify());
        Console.WriteLine(e10.simplify());
        Console.WriteLine(e11.simplify());
        
        

    }
}