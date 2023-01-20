using System;
using System.IO;



namespace LogicalFunctionProgram
{
    class LogicalFunction
    {
        private string name;
        private string expression;
        private static Dictionary<string, LogicalFunction> functions = new Dictionary<string, LogicalFunction>();
        private List<string> variables;
        public LogicalFunction(string name, string expression)
        {
            this.name = name;
            this.expression = expression;
            this.variables = GetVariables(expression);
        }

        public static void AddFunction(string name, LogicalFunction func)
        {
            functions.Add(name, func);
        }


        public int Evaluate(Dictionary<string, int> values)
        {
            // check for missing values
            foreach (string variable in variables)
            {
                if (!values.ContainsKey(variable))
                {
                    throw new ArgumentException("Missing value for variable: " + variable);
                }
            }

            // evaluate the expression using the values
            // this implementation is just an example and it doesn't handle the precedence of the operators or the functions
            int result = 0;
            string[] tokens = expression.Split(" ");
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < tokens.Length; i++)
            {
                switch (tokens[i])
                {
                    case "&":
                        int op2 = stack.Pop();
                        int op1 = stack.Pop();
                        stack.Push(op1 & op2);
                        break;
                    case "|":
                        op2 = stack.Pop();
                        op1 = stack.Pop();
                        stack.Push(op1 | op2);
                        break;
                    case "!":
                        op1 = stack.Pop();
                        stack.Push(~op1);
                        break;
                    default
    :
                        if (Char.IsLetter(tokens[i][0]))
                        {
                            // check if token is a variable
                            if (values.ContainsKey(tokens[i]))
                            {
                                stack.Push(values[tokens[i]]);
                            }
                            else
                            {
                                // check if token is a function
                                if (functions.ContainsKey(tokens[i]))
                                {
                                    // get function and evaluate it
                                    LogicalFunction func = functions[tokens[i]];
                                    stack.Push(func.Evaluate(values));
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid variable or function: " + tokens[i]);
                                }
                            }
                        }
                        else
                        {
                            stack.Push(int.Parse(tokens[i]));
                        }
                        break;
                }
            }
            result = stack.Pop();
            return result;
        }

        private List<string> GetVariables(string expression)
        {
            // parse the expression to get the list of variables
            List<string> variables = new List<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (Char.IsLetter(expression[i]))
                {
                    string variable = "";
                    while (i < expression.Length && Char.IsLetter(expression[i]))
                    {
                        variable += expression[i];
                        i++;
                    }
                    if (!variables.Contains(variable))
                    {
                        variables.Add(variable);
                    }
                }
            }
            return variables;
        }
        public int SolveFunction(int[] param)
        {
            if (variables == null)
            {
                variables = GetVariables(expression);
            }
            if (param.Length != variables.Count)
            {
                throw new ArgumentException("Incorrect number of parameters.");
            }

            Dictionary<string, int> values = new Dictionary<string, int>();
            for (int i = 0; i < variables.Count; i++)
            {
                if (param[i] != 0 && param[i] != 1)
                {
                    throw new ArgumentException("Invalid value for variable: " + variables[i]);
                }
                values.Add(variables[i], param[i]);
            }
            return Evaluate(values);
        }
        public int[][] ConstructTruthTable()
        {
            int numVariables = variables.Count;
            int numRows = (int)Math.Pow(2, numVariables);

            int[][] truthTable = new int[numRows][];

            for (int i = 0; i < numRows; i++)
            {
                truthTable[i] = new int[numVariables + 1];
                for (int j = 0; j < numVariables; j++)
                {
                    truthTable[i][j] = (i >> j) & 1;
                }
            }

            Dictionary<string, int> values = new Dictionary<string, int>();
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    values[variables[j]] = truthTable[i][j];
                }
                truthTable[i][numVariables] = Evaluate(values);
            }

            return truthTable;
        }
        public static string FindLogicalFunction(string truthTableString)
        {
            // parse the truth table string
            string[] rows = truthTableString.Split(',');
            int[][] truthTable = new int[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                string[] cells = rows[i].Split();
                truthTable[i] = new int[cells.Length];
                for (int j = 0; j < cells.Length; j++)
                {
                    truthTable[i][j] = int.Parse(cells[j]);
                }
            }

            // find a logical function that matches the truth table
            string function = "";
            // this is just an example and it doesn't handle all cases
            if (truthTable[0][0] == 0 && truthTable[0][1] == 0 && truthTable[1][0] == 0 && truthTable[1][1] == 1)
            {
                function = "!a & !b";
            }
            else if (truthTable[0][0] == 0 && truthTable[0][1] == 1 && truthTable[1][0] == 1 && truthTable[1][1] == 1)
            {
                function = "!a | b";
            }
            else
            {
                function = "No matching function found";
            }

            return function;
        }

    }

}
