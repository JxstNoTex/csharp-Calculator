using System;
using System.Globalization;

namespace Calculator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Calculator");
            Console.WriteLine("Please enter your equation");
            bool quit = false;
            while(!quit)
            {
                string input = Console.ReadLine();
                
                if (input == null || input == "quit" || input == "q")
                    quit = true;
                else
                {
                    input = input.ToLower();
                    string prepared = "";
                    bool IsOperator = true;

                    if (input.Contains("pi"))
                        input = input.Replace("pi", "3.141592653");

                    if (input.Contains("e"))
                        input = input.Replace("e", "2.718281828");

                    /*
                     * sinus = s
                     * Kosinus = c
                     * Tangens = t
                     * Wurzel = w
                     */
                    for (int i = 0; i < input.Length; i++)
                    {

                        if (!IsOperator && input[i] == '(')
                            prepared += "*(";
                        else if (input[i] == ',' || input[i] == '.')
                            prepared += '.';
                        else if (IsOperator && input[i] == '-')
                            prepared += '_';
                        else if ("1234567890+-*/^%-()_sct!w".Contains(input[i]))
                            prepared += input[i];

                        if ("+-*/%^sct!w".Contains(input[i]))
                            IsOperator = true;
                        else if ("1234567890_.".Contains(input[i]))
                            IsOperator = false;
                    }
                   
                    Console.WriteLine(Calculate(prepared));
                }

            }
        }

        static string Calculate(string input)
        {

            input = input.Replace(" ", "");
            int Operator;
            string num0 = "", num1 = "";
            while (input.Contains('(') && input.Contains(')'))
            {
                input = input.Substring(0, input.IndexOf('(')) + Calculate(input[(input.IndexOf('(') + 1)..input.LastIndexOf(')')]) +
                    input.Substring(input.LastIndexOf(')') + 1);
            }
            while (input.Contains('!'))
            {
                Operator = input.IndexOf('!');

                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num0 += input[i];
                }
                string result = DoubleToString(Factorial(double.Parse(num0, CultureInfo.InvariantCulture))).Replace('-', '_');

                input = input.Substring(0, Operator) + "*" + result + input.Substring(Operator + 1 + num0.Length);
                if (input[0] == '*') input = input.Remove(0, 1);

                num0 = "";
                num1 = "";
            }

            while (input.Contains('w'))
            {
                Operator = input.IndexOf('w');

                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num0 += input[i];
                }

                num0 = num0.Replace('_', '-');

                string result = DoubleToString(Math.Sqrt(double.Parse(num0, CultureInfo.InvariantCulture))).Replace('-', '_');

                input = input.Substring(0, Operator) + "*" + result + input.Substring(Operator + 1 + num0.Length);
                if (input[0] == '*') input = input.Remove(0, 1);

                num0 = "";
                num1 = "";
            }
            while (input.Contains('s'))
            {
                Operator = input.IndexOf('s');

                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num0 += input[i];
                }
                string result = DoubleToString(Math.Sin(double.Parse(num0, CultureInfo.InvariantCulture))).Replace('-','_');

                input = input.Substring(0, Operator ) + "*" + result + input.Substring(Operator + 1 + num0.Length);
                if (input[0] == '*') input = input.Remove(0, 1);

                num0 = "";
                num1 = "";
            }
            while (input.Contains('c'))
            {
                Operator = input.IndexOf('c');

                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num0 += input[i];
                }

                num0 = num0.Replace('_', '-');
                string result = DoubleToString(Math.Cos(double.Parse(num0, CultureInfo.InvariantCulture))).Replace('-', '_');

                input = input.Substring(0, Operator) + "*" + result + input.Substring(Operator + 1 + num0.Length);
                if (input[0] == '*') input = input.Remove(0, 1);

                num0 = "";
                num1 = "";
            }
            while (input.Contains('t'))
            {
                Operator = input.IndexOf('t');

                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num0 += input[i];
                }

                num0 = num0.Replace('_', '-');
                string result = DoubleToString(Math.Tan(double.Parse(num0, CultureInfo.InvariantCulture))).Replace('-', '_');

                input = input.Substring(0, Operator) + "*" + result + input.Substring(Operator + 1 + num0.Length);
                if (input[0] == '*') input = input.Remove(0, 1);

                num0 = "";
                num1 = "";
            }
            while (input.Contains('^'))
            {
                Operator = input.IndexOf('^');
                
                for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                {
                    num0 = input[i] + num0;
                }
                
                num0 = num0.Replace('_', '-');
                num1 = num1.Replace('_', '-');

                string result = DoubleToString(Math.Pow(double.Parse(num0, CultureInfo.InvariantCulture), double.Parse(num1,
                CultureInfo.InvariantCulture))).Replace('-','_');

                
                input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                num0 = "";
                num1 = "";
            }
            while(input.Contains('%'))
            {
                Operator = input.IndexOf('%');
                
                for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                {
                    num0 = input[i] + num0;
                }
                for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                {
                    num1 += input[i];
                }

                num0 = num0.Replace('_', '-');
                num1 = num1.Replace('_', '-');
                string result = DoubleToString(double.Parse(num0, CultureInfo.InvariantCulture) % double.Parse(num1,
                    CultureInfo.InvariantCulture)).Replace('-', '_');

                
                input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                num0 = "";
                num1 = "";
            }
            while (input.Contains('*') || input.Contains('/'))
            {
                if (input.IndexOf('*') >= 0 && (input.IndexOf('*') < input.IndexOf('/') || input.IndexOf('/') < 0))
                {
                    Operator = input.IndexOf('*');
                    
                    for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                    {
                        num0 = input[i] + num0;
                    }
                    for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                    {
                        num1 += input[i];
                    }
                    num0 = num0.Replace('_', '-');
                    num1 = num1.Replace('_', '-');
                    string result = DoubleToString(double.Parse(num0, CultureInfo.InvariantCulture) * double.Parse(num1,
                    CultureInfo.InvariantCulture)).Replace('-', '_');
                    

                    input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                    num0 = "";
                    num1 = "";
                }
                else
                {
                    Operator = input.IndexOf('/');
                    
                    for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                    {
                        num0 = input[i] + num0;
                    }
                    for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                    {
                        num1 += input[i];
                    }
                    num0 = num0.Replace('_', '-');
                    num1 = num1.Replace('_', '-');
                    string result = DoubleToString(double.Parse(num0, CultureInfo.InvariantCulture) / double.Parse(num1,
                    CultureInfo.InvariantCulture)).Replace('-', '_');

                    input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                    num0 = "";
                    num1 = "";
                }
            }
            while (input.Contains('+') || input.Contains('-'))
            {
                if (input.IndexOf('+') >= 0 && (input.IndexOf('+') < input.IndexOf('-') || input.IndexOf('-') < 0))
                {
                    Operator = input.IndexOf('+');
                    
                    for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                    {
                        num0 = input[i] + num0;
                    }
                    for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                    {
                        num1 += input[i];
                    }
                    num0 = num0.Replace('_', '-');
                    num1 = num1.Replace('_', '-');
                    string result = DoubleToString(double.Parse(num0, CultureInfo.InvariantCulture) + double.Parse(num1,
                   CultureInfo.InvariantCulture)).Replace('-', '_');

                    input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                    num0 = "";
                    num1 = "";
                }
                else
                {
                    Operator = input.IndexOf('-');
                    
                    for (int i = Operator - 1; i >= 0 && "1234567890_.".Contains(input[i]); i--)
                    {
                        num0 = input[i] + num0;
                    }
                    for (int i = Operator + 1; i < input.Length && "1234567890_.".Contains(input[i]); i++)
                    {
                        num1 += input[i];
                    }
                    num0 = num0.Replace('_', '-');
                    num1 = num1.Replace('_', '-');
                    string result = DoubleToString(double.Parse(num0, CultureInfo.InvariantCulture) - double.Parse(num1,
                   CultureInfo.InvariantCulture)).Replace('-', '_');

                    input = input.Substring(0, Operator - num0.Length) + result + input.Substring(Operator + 1 + num1.Length);
                    num0 = "";
                    num1 = "";
                }
            }

            input = input.Replace("_", "-");
            return input;
        }

        private static double Factorial(double num)
        {
            if (num == 0)
                return 1;
            else
                return num * Factorial(num - 1);
        }

        // von https://codereview.stackexchange.com/q/149382
        private static string DoubleToString(double d)
        {
            

            string R = d.ToString("R", CultureInfo.InvariantCulture).Replace(",", "");
            int i = R.IndexOf('E');

            if (i < 0)
                return R;

            string G17 = d.ToString("G17", CultureInfo.InvariantCulture);

            if (!G17.Contains('E'))
                return G17.Replace(",", "");

            // manual parsing
            string beforeTheE = R.Substring(0, i);
            int E = Convert.ToInt32(R.Substring(i + 1));

            i = beforeTheE.IndexOf('.');

            if (i < 0)
                i = beforeTheE.Length;
            else
                beforeTheE = beforeTheE.Replace(".", "");

            i += E;

            while (i < 1)
            {
                beforeTheE = "0" + beforeTheE;
                i++;
            }

            while (i > beforeTheE.Length)
            {
                beforeTheE += "0";
            }

            if (i == beforeTheE.Length)
                return beforeTheE;

            return String.Format("{0}.{1}", beforeTheE.Substring(0, i), beforeTheE.Substring(i));
        }
    }
}
