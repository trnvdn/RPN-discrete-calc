using System.Collections;
using System.Diagnostics;

namespace tryCDMapp
{
    public class SetCalculatuion
    {
        public int A;
        public int B;
        public int C;

        public int SetCalc(int first, int second,string operand)
        {
            int RESULTAT = 0;
            int temprp = 0;
            switch(operand)
            {
                case "+":
                    RESULTAT = Math.Max(first, second);
                    break;
                case "*":
                {
                    RESULTAT = Math.Min(first, second);
                    break;
                }
                case "`":
                {
                    switch (first)
                    {
                        case 0:
                            RESULTAT = 1;
                            break;
                        case 1:
                            RESULTAT = 0;
                            break;
                    }
                    break;
                }
                case "->":
                {
                    if (first == 1 && second == 0)
                    {
                        RESULTAT = 0;
                    }
                    else
                    {
                        RESULTAT = 1;
                    }
                    break;
                }
                case "~":
                {
                    if (first == second)
                    {
                        RESULTAT = 1;
                    }
                    else
                    {
                        RESULTAT = 0;
                    }
                    break;
                }
                    
            }
            

            return RESULTAT;
        }

        public int negation(int a)
        {
            int answ = 0;
            if (a == 1)
            {
                answ = 0;
            }
            else
            {
                answ = 1;
            }
            return answ;
        }

        public void SetsRead(string name)
        {
            int takedvalue = 0;
            bool complete = false;
            while (complete == false)
            {
                Console.WriteLine($"Enter elems for {name} set.");
                string line = Console.ReadLine();
                if (line != "")
                {
                    bool succes = int.TryParse(line,out int value);
                    if (succes)
                    {
                        switch (value)
                        {
                            case 1:
                                takedvalue = value;
                                complete = true;
                                break;
                            case 0:
                                takedvalue = value;
                                complete = true;
                                break;
                            default:
                                Console.WriteLine("Error!Enter 1 or 0!");
                                complete = false;
                                break;
                        }    
                    }
                    else
                    {
                        Console.WriteLine("Error!Enter 1 or 0!");
                        complete = false;
                    }
                }
                else
                {
                    Console.WriteLine("Error!Enter 1 or 0!");
                    complete = false;
                }
            }
            switch (name)
            {
                case "A":
                    A = takedvalue;
                    Console.WriteLine(name+ " = " + "{ " + A + " }");
                    break;
                case "B":
                    B = takedvalue;
                    Console.WriteLine(name+ " = " + "{ " + B + " }");
                    break;
                case "C":
                    C  = takedvalue;
                    Console.WriteLine(name+ " = " + "{ " + C + " }");
                    break;
            }
        }

    }
}

    

