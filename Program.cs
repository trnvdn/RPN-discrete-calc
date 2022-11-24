using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace tryCDMapp
{
        public struct Obj
    {
        public Obj(string? var, int? value)
        {
            Var = null;
            var = Var;
            value = null;
            Value = 0;
            value = Value;
        }


        public string? Var { get; set; }

        public int Value { get; set; }

        public override string ToString() => $"({Var}, {Value})";
    }


        class Program
        {
            static void Main(string[] args)
            {
                bool isEnd = false;
                SetCalculatuion Sets = new SetCalculatuion();
                Obj item = new Obj();
                int couter = 1;
                int indycator = 0;

                Stack<Obj> operands = new Stack<Obj>();
                Stack<Obj> variables = new Stack<Obj>();
                List<string> RPN_vars = new List<string>();
                List<string> RPN_vars_values = new List<string>();
                List<string> RPN_ops = new List<string>();

                Sets.SetsRead("A");
                Sets.SetsRead("B");
                Sets.SetsRead("C");
                string line = Console.ReadLine();
                string[] parsedlinearr = line.Split(" ");
                for (int i = parsedlinearr.Length - 1;i >= 0;i--) 
                {
                    switch (parsedlinearr[i])
                    {
                        case "A":
                            item.Value = Sets.A;
                            item.Var = "A";
                            variables.Push(item);
                            RPN_vars.Add("A");
                            RPN_vars_values.Add(Convert.ToString(Sets.A));
                            break;
                        case "B":
                            item.Value = Sets.B;
                            item.Var = "B";
                            variables.Push(item);
                            RPN_vars.Add("B");
                            RPN_vars_values.Add(Convert.ToString(Sets.B));
                            break;
                        case "C":
                            item.Value = Sets.C;
                            item.Var = "C";
                            variables.Push(item);
                            RPN_vars.Add("C");
                            RPN_vars_values.Add(Convert.ToString(Sets.C));
                            break;
                        case "+":
                            item.Var = "+";
                            item.Value = 3;
                            operands.Push(item);
                            RPN_ops.Add("+");
                            break;
                        case "*":
                            item.Var = "*";
                            item.Value = 4;
                            operands.Push(item);
                            RPN_ops.Add("*");
                            break;
                        case "~":
                            item.Var = "~";
                            item.Value = 1;
                            operands.Push(item);
                            RPN_ops.Add("~");
                            break;
                        case "->":
                            item.Var = "->";
                            item.Value = 2;
                            operands.Push(item);
                            RPN_ops.Add("->");
                            break;
                        case "`":
                            item.Var = "`";
                            item.Value = 5;
                            operands.Push(item);
                            RPN_ops.Add("`");
                            break;
                    }

                    indycator++;
                }

                RPN_write(RPN_vars,RPN_ops,RPN_vars_values);
                
                while (isEnd == false)
                {
                    if (operands.Count != 0)
                    {
                        Calc(operands, variables, item,couter);
                        couter++;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }

                Console.WriteLine(variables.Peek().Value);
            }

            static void RPN_write(List<string> rpn_vars,List<string> rpn_ops,List<string> rpn_valus)
            {
                string expression = null;
                string vars = null;
                string operands = null;
                string values = null;
                for (int i = rpn_valus.Count - 1; i >= 0; i--)
                {
                    values += rpn_valus[i] + " ";
                }
                for (int j = rpn_vars.Count - 1; j >= 0; j--)
                {
                    vars += rpn_vars[j] + " ";
                }

                for (int k = rpn_ops.Count - 1; k >= 0; k--)
                {
                    operands += rpn_ops[k] + " ";
                }

                Console.WriteLine($"RPN expression with letters {vars}  ||  {operands}");
                Console.WriteLine($"RPN expression with numbers {values}  ||  {operands}");
            }
            public static void Calc(Stack<Obj> op, Stack<Obj> var, Obj item,int cout)
            {
                SetCalculatuion Sets = new SetCalculatuion();
                int result;
                int a = var.Peek().Value;
                string a_letter = var.Peek().Var;
                var.Pop();
                string operand = op.Peek().Var;
                if (operand == "`")
                {
                    op.Pop();
                    result = Sets.negation(a);
                    item.Value = result;
                    item.Var = operand + a_letter;
                    var.Push(item);
                    Console.WriteLine($"{cout}){var.Peek().Var} = {operand}{a} = {var.Peek().Value}");
                    
                }
                else
                {
                    int operand_priority = op.Peek().Value;
                    op.Pop();
                    if (op.Count != 0)
                    {
                        int temp_operand_priority = op.Peek().Value;
                        string temp_operand = op.Peek().Var;
                        if (operand_priority >= temp_operand_priority)
                        {
                            if (temp_operand == "`")
                            {
                                result = Sets.negation(a);
                                item.Value = result;
                                item.Var = temp_operand + a_letter;
                                Console.WriteLine($"{cout}){var.Peek().Var} = {temp_operand}{a_letter} = {var.Peek().Value}  ");
                            }
                            else
                            {
                                int b = var.Peek().Value;
                                string b_letter = var.Peek().Var;
                                var.Pop();
                                result = Sets.SetCalc(a, b, operand);
                                item.Value = result;
                                item.Var = a_letter + operand + b_letter;
                                var.Push(item);
                                Console.WriteLine($"{cout}){var.Peek().Var} = {a}{operand}{b} = {var.Peek().Value}");
                            }
                        }

                        if (operand_priority < temp_operand_priority)
                        {
                            op.Pop();
                            int b = var.Peek().Value;
                            string b_letter = var.Peek().Var;
                            var.Pop();
                            item.Value = operand_priority;
                            item.Var = operand;
                            op.Push(item);
                            if (temp_operand == "`")
                            {
                                result = Sets.negation(b);
                                item.Value = result;
                                item.Var = temp_operand + b_letter;
                                var.Push(item);
                                Console.WriteLine($"{cout}){var.Peek().Var} = {temp_operand}{b} = {var.Peek().Value}  ");
                                item.Var = a_letter;
                                item.Value = a;
                                var.Push(item);
                            }
                            else
                            {
                                int c = var.Peek().Value;
                                string c_letter = var.Peek().Var;
                                var.Pop();
                                item.Var = a_letter;
                                item.Value = a;
                                var.Push(item);
                                result = Sets.SetCalc(b, c, temp_operand);
                                item.Value = result;
                                item.Var = b_letter + temp_operand + c_letter;
                                var.Push(item);
                                Console.WriteLine($"{cout}){var.Peek().Var} = {b}{operand}{c} = {var.Peek().Value}  ");
                            }
                        }
                    }
                    else
                    {
                        int b = var.Peek().Value;
                        string b_letter = var.Peek().Var;
                        var.Pop();
                        result = Sets.SetCalc(a, b, operand);
                        item.Value = result;
                        item.Var = a_letter + operand + b_letter;
                        var.Push(item);
                        Console.WriteLine($"{cout}){var.Peek().Var} = {a}{operand}{b} = {var.Peek().Value}");
                    }
                    
                }


            }
           
            
        }
}

