% Code Execution, Final
% Sam
% June 27, 2021

# Welcome!

## Code input C#:

``` csharp
using System;

namespace ConsoleApp
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

## Code output C#:

``` {
  .remote-compile
  language=mono
  language-version=6.12.0
}
using System;

namespace ConsoleApp
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

## Code input C++

``` c++
#include <iostream>

int main() {
    int x = 5;
    x *= 10;
    x /= 3;
    std::cout << "x is " << x << std::endl;
    return 0;
}
```

## Code output C++

``` {
  .remote-compile
  language=c++
  language-version=10.2.0
}
#include <iostream>

int main()
{
    int x = 5;
    x *= 10;
    x /= 3;
    std::cout << "x is " << x << std::endl;
    return 0;
}
```

## Code input Python 3

``` python
x = 5;
x *= 10;
x /= 3;
print("x is " + str(x));
```

## Code output Python 3

``` {
  .remote-compile
  language=python
  language-version=3.9.1
}
x = 5;
x *= 10;
x /= 3;
print("x is " + str(x));
```
