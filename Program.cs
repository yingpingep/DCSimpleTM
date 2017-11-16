using System;
using System.Collections.Generic;
using DC_TurningMachine;

namespace DC_TurningMachine
{
    class Program
    {
        static void Main(string[] args)
        {            
            // TODO:
            // 1. Basic logic:
            //   a. Give mode. (ADD, COUNT OR SUBTRACTION)    
            //   b. Give an input string with only '0' or '1'.
            //   c. Read the input string by char
            //   d. Read transition rule.
            //   e. Do things.
            // 2. Create transition rule table. (Accroding Turning Machine and build for ADD and COUNT and SUBTRACTION.)

            // This is the sample of how to change cursor Move.            
            // Console.SetCursorMove(Console.CursorLeft + 1, Console.CursorTop); 

            Console.Write("Please enter an string made by '0' or '1': ");
            string userInput = Console.ReadLine();               
            string initStatus = "0";            

            AddTransition add = new AddTransition();
            add.ReadTheLine(userInput.ToCharArray(), initStatus, 0, add.AddTRs);
        }        
    }    

    public abstract class TransitionBase
    {
        public void ReadTheLine(char[] input, string initStatus, int curPosition, IList<TransitionRule> list)
        {
            char direction = ' ';
            string status = initStatus;
            while (direction != 'S' || curPosition != input.Length)
            {
                foreach (var item in list)
                {
                    if (string.Equals(status, item.CurrentStatus) && input[curPosition] == item.CurrentInput)
                    {  
                        Console.WriteLine($"\nCurrent position: {curPosition},\tCurrent status: {status},\tRead input: {input[curPosition]}");
                        Console.WriteLine($"Direction: {item.Direction},\tNext Status: {item.AfterStatus},\tChange input from {input[curPosition]} to {item.ChangeTo}");

                        Console.Write("OLD: ");
                        int left = Console.CursorLeft + curPosition;
                        Console.Write(input);         

                        Console.SetCursorPosition(left, Console.CursorTop);
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(input[curPosition]);     
                        Console.ResetColor();                        
                        Console.WriteLine();

                        status = item.AfterStatus;
                        input[curPosition] = item.ChangeTo;                        

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("NEW: ");
                        Console.ResetColor();
                        Console.Write(input);
                        Console.SetCursorPosition(left, Console.CursorTop);
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(input[curPosition]);     
                        Console.ResetColor();                        
                        Console.WriteLine();

                        switch (item.Direction)
                        {
                            case 'R':
                                curPosition += 1;
                                break;
                            case 'L':
                                curPosition -= 1;
                                break;
                            case 'E':   
                                break;
                            case 'S':
                                direction = 'S';                                
                                Console.WriteLine();                                
                                break;
                            default:
                                break;
                        }                        
                        break;
                    }
                }                
            }
            Console.WriteLine("DONE.");
        }
    }
    
    public class TransitionRule
    {
        public string CurrentStatus { get; set; }
        public char CurrentInput { get; set; }
        public string AfterStatus { get; set; }
        public char ChangeTo { get; set; }   
        public char Direction { get; set; }     

        public TransitionRule(string currentStatus, char currentInput, string afterStatus,char changeTo, char direction)
        {
            CurrentStatus = currentStatus;
            CurrentInput = currentInput;
            AfterStatus = afterStatus;
            ChangeTo = changeTo;
            Direction = direction;
        }
    }

    public class AddTransition : TransitionBase
    {
        public List<TransitionRule> AddTRs = new List<TransitionRule>()
        {
            new TransitionRule("0", '0', "0", '0', 'R'),
            new TransitionRule("0", '1', "1", '1', 'R'),
            new TransitionRule("1", '0', "10", '1', 'R'),            
            new TransitionRule("1", '1', "1", '1', 'R'),
            new TransitionRule("10", '0', "11", '0', 'L'),
            new TransitionRule("10", '1', "10", '1', 'R'),
            new TransitionRule("11", '0', " ", ' ', 'E'),
            new TransitionRule("11", '1', "0", '0', 'S')
        };
        
    }
}
