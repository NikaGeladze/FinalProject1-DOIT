namespace Calculator;

public class Calculator
{
    private char[] operations = new char[] { '+', '-', '/', '*', 'q' };

    public void Run()
    {
        char op = ' ';
        while (op != 'q')
        {
            decimal num1 = ReadDecimal("Enter First number: ");
            decimal num2 = ReadDecimal("Enter Second Number: ");
            op = ReadOperation("Enter operation(+,-,*,/) or q to exit");
            

            if (op != 'q')
            {
                ValidateResult(num1,num2,op);
            }

        }
    }

    public decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            bool parsed = decimal.TryParse(Console.ReadLine(), out decimal value);
            if (parsed)
            {
                return value;
            }
            Console.WriteLine("Number was not in correct format.Try Again");
        }
    }
    public char ReadOperation(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            bool parsed = char.TryParse(Console.ReadLine(), out char value);
            if (parsed && operations.Contains(value))
            {
                return value;
            }
            Console.WriteLine("Operation was not in correct format.Try Again");
        }
    }

    public void ValidateResult(decimal num1, decimal num2, char op)
    {
        try
        {
            decimal result = Calculate(num1, num2, op);
            Console.WriteLine($"{num1} {op} {num2} = {result}");
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (OverflowException e)
        {
            Console.WriteLine("Result too big.Try Again");
        }
    }

    public decimal Calculate(decimal num1, decimal num2, char op)
    {
        decimal result = 0;
            
        switch (op)
        {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            case '/' :
                if (num2 == 0m) throw new DivideByZeroException("Can't divide by Zero.Try Again");
                result = num1 / num2;
                break;
            case '*':
                result = num1 * num2;
                break;
            default:
                throw new InvalidOperationException("Operation is invalid.Try Again");
        }

        return result;
    }
}