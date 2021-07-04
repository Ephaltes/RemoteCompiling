% Code Execution, Easter Special
% Sam
% April 4, 2021

# Happy Easter!

## Code input:

```csharp
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            x *= 10;
            x /= 3;
            Console.WriteLine("x is " + x);
        }
    }
}
```

## Code output:

```csharp
{
  .remote-compile
  language=dotnet
  language-version=5.0.201
  args='argumentOne,secondArgument'
  stdin='this input is sent to the program'
}
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Arguments: ");

            foreach (string argument in args)
            {
                Console.WriteLine("+ " + argument);
            }

            string input = Console.ReadLine();
            Console.WriteLine("Received input: '" + input + "'");

            int x = 5;
            x *= 10;
            x /= 3;
            Console.WriteLine("x is " + x);
        }
    }
}
```
