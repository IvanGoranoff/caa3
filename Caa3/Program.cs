using System;
using System.IO;
using LogicalFunctionProgram;

namespace LogicalFunctionProgram
{
    class Program
    {
       
    static void Main(string[] args)
    {
        // create an instance of the LogicalFunction class
        LogicalFunction func1 = new LogicalFunction("func1", "a & b");
        // Define a dictionary to store user-defined functions
        Dictionary<string, LogicalFunction> functions = new Dictionary<string, LogicalFunction>();

        // Introduction of logical functions
        Console.WriteLine("Enter a command (DEFINE, SOLVE, ALL, FIND, EXIT):");
        string command = Console.ReadLine();
        while (command.ToUpper() != "EXIT")
        {
            switch (command.ToUpper())
            {
                case "DEFINE":
                    // Get function name and expression
                    Console.WriteLine("Enter function name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter function expression:");
                    string expression = Console.ReadLine();

                    // Define the function
                    LogicalFunction func = new LogicalFunction(name, expression);
                    functions.Add(name, func);
                    break;
                case "SOLVE":
                    // Get function name
                    Console.WriteLine("Enter function name:");
                    string funcName = Console.ReadLine();
                    // Get parameters
                    Console.WriteLine("Enter parameters, separated by space:");
                    string[] parameters = Console.ReadLine().Split();
                    // Convert parameters to integers
                    int[] param = new int[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                            try
                    {
                        param[i] = int.Parse(parameters[i]);
                    }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                                parameters[i] = Console.ReadLine();
                                i--;
                            }
                        // Solve the function
                        int result = functions[funcName].SolveFunction(param);
                    Console.WriteLine("Result = {0}", result);
                    break;
                case "ALL":
                    // Get function name
                    Console.WriteLine("Enter function name:");
                    string functionName = Console.ReadLine();

                        // Construct a truth table for the function
                        int[][] truthTable = functions[functionName].ConstructTruthTable();

                        Console.WriteLine("Truth table:");
                    foreach (int[] row in truthTable)
                    {
                        foreach (int cell in row)
                        {
                            Console.Write(cell + " ");
                        }
                        Console.WriteLine();
                    }
                    break;
                case "FIND":
                    // Get the truth table to find a function for
                    Console.WriteLine("Enter the truth table (comma separated):");
                    string truthTableString = Console.ReadLine();
                    // Find a logical function that matches the truth table
                    string function = LogicalFunction.FindLogicalFunction(truthTableString);
                    Console.WriteLine("Function: {0}", function);
                    break;
                default:
                    Console.WriteLine("Invalid command. Please enter a valid command (DEFINE, SOLVE, ALL, FIND, EXIT).");
                    break;
            }

            // Get the next command
            Console.WriteLine("Enter a command (DEFINE, SOLVE, ALL, FIND, EXIT):");
            command = Console.ReadLine();
        }
    }
}

    }