using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamGenerator
{
    public class ConsoleUI
    {
        //Setting up properties and fields
        Roster classList { get; set; } = new Roster();

        TeamGenerator TeamBuilder;

        public List<int> IndexOfNamesToDelete { get; set; } = new();

        public ConsoleUI()
        {
            TeamBuilder = new TeamGenerator(classList);
        }

        //Method to display menu
        public void ShowMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                PrintMenu();

                int choice = ReadMenuChoice(1, 6);
                isRunning = HandleMenuChoice(choice);
            }
        }
        //Method to print menu on screen
        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("=== The Team Generator ===\n");

            Console.ResetColor();

            Console.WriteLine("[1] Print class list");
            Console.WriteLine("[2] Delete name from class list");
            Console.WriteLine("[3] Add name to class list");
            Console.WriteLine("[4] Generate teams");
            Console.WriteLine("[5] Print teams");
            Console.WriteLine("[6] Exit program");
        }

        //Method to read and validate user choice on the menu
        public int ReadMenuChoice(int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write("\nEnter your choice: ");
                string? userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    bool success = int.TryParse(userInput.Trim(), out int parsedValue);
                    if (success && parsedValue >= minValue && parsedValue <= maxValue)
                    {
                        return parsedValue;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nPlease enter a valid number between {minValue} and {maxValue}.");
                Console.ResetColor();
                Thread.Sleep(1000);  
            }
        }
      
        //Method to pass user onward depending on choice from menu
        public bool HandleMenuChoice(int userChoice)
        {           
            switch (userChoice)
            {
                //[1] Print class list
                case 1:                    
                    Console.Clear();
                    PrintClassList();
                    Console.WriteLine("\nNumber of students: " + classList.Count());
                    ReturnToMenu();
                    break;

                //[2] Delete name from class list
                case 2:                
                    ShowInterfaceToDeleteName();
                    ReturnToMenu();
                    break;

                //[3] Add name to class list
                case 3:                    
                    Console.Clear();
                    PrintFeatureInMaking();
                    ReturnToMenu();
                    break;

                //[4] Generate teams
                case 4:                    
                    Console.Clear();
                    TeamBuilder.GenerateRandomTeams(4);
                    PrintTeams();
                    ReturnToMenu();
                    break;

                //[5] Print teams
                case 5:                    
                    Console.Clear();
                    PrintTeams();
                    ReturnToMenu();
                    break;

                //[6] Exit program
                case 6:                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Thank you for using the Team Generator!");
                    Console.ResetColor();
                    return false;
                    

                default:
                    Console.Clear();
                    Console.WriteLine("Oops, something went wrong.");
                    ReturnToMenu();
                    break;
            }
            return true;
        }

        //Method as a placeholder until function is ready to launch
        public void PrintFeatureInMaking()
        {
            Console.WriteLine("Sorry this feature isn't available at the moment.");
        }

        //Method to get user back to menu
        public void ReturnToMenu()
        {
            Console.Write("\nPress Enter to return to menu.");
            Console.ReadLine();
        }

        //Method for printing classlist + numbers of students on classlist
        public void PrintClassList()
        {
            
            Console.WriteLine("Class list: ");
            for (int i = 0; i < classList.ClassList.Count; i++)
            {
                Console.WriteLine($"[{i+1}] " + classList.ClassList[i].NameOfStudent);
            }
                       
        } 

       
        //Method to show deletion options 
        public void ShowInterfaceToDeleteName()
        {
            bool ContinueDeleting = true;

            while (ContinueDeleting)
            {
                Console.Clear();
                Console.WriteLine("== Delete Names from Class list ==\n");
                PrintClassList();
                Console.WriteLine();

                PrintInstructionToDelete();

                ContinueDeleting = ReadIntChoice(1, classList.Count());
                if (!ContinueDeleting)
                {
                    return;
                }

                PrintNamesToDelete();
                string userInput = ReadChoiceYesNo();
                ContinueDeleting = HandleChoiceToDelete(userInput);
            }
        }
        

        //Method for asking user to choose names to delete
        public void PrintInstructionToDelete()
        {
            Console.WriteLine("\nEnter number(s) of students to delete from class list (comma-separated). Press Enter to return to menu.");                       
        }
                        
        //Method to validate user input
       
        public bool ReadIntChoice(int minValue, int maxValue)
        {        
            while (true)
            {
                string userInput = AskForNamesToDelete();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    IndexOfNamesToDelete.Clear();
                    return false;
                }

                List<int> ParsedIndexes = SplitAndParseInput(userInput, minValue, maxValue);
                              
                if (ParsedIndexes.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nPlease enter valid number(s) between {minValue} and {maxValue} (separate several numbers with \",\").");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                }
                else
                {
                    IndexOfNamesToDelete = ParsedIndexes;
                    break;
                }                               
            }
            return true;
        }

        //Method to ask for names to delete

        public string AskForNamesToDelete()
        {
            Console.Write("\nEnter number(s) to delete (or Enter to return to menu): ");
            return Console.ReadLine() ?? "";
        }


        //Nånting blir fel då användaren ska ge 0 för att återgå och vid val av 1 så blir det noll. Ev. fixa i både handle choice to delete och print? Går på annat sätt? under else i ReadIntChoice? 

        //Method to split userinput and parse substrings in to int
        public List<int> SplitAndParseInput(string userInput, int minValue, int maxValue)
        {
            List<int> ParsedIndexes = new List<int>();

            string[]? separator = { ",", "." };

            string[] SplitResult = userInput.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            bool sucess = false;

            for (int i = 0; i < SplitResult.Length; i++)
            {
                sucess = int.TryParse(SplitResult[i], out int parsedValue);
                if (sucess && parsedValue >= minValue && parsedValue <= maxValue)
                {
                    ParsedIndexes.Add(parsedValue - 1);
                }
            }

            return ParsedIndexes;
        }

        //Method to print choosen names to delete
        public void PrintNamesToDelete()
        {
            Console.WriteLine();
            Console.WriteLine("\nDo you want to delete the following names from the class list?\n");

            foreach (var indexnumber in IndexOfNamesToDelete)
            {
                Console.WriteLine(classList.ClassList[indexnumber].NameOfStudent);
            }
        }

        //Method to read and validate answer to Yes/No-question
        public string ReadChoiceYesNo()
        {
            string? userInput;
            while (true)
            {
                Console.Write("\nEnter your choice (Y/N): ");
                userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput))
                {
                    userInput = userInput.ToLower().Trim();

                    if (userInput == "y" || userInput == "n")
                    {
                        return userInput;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter valid answer (y/n)! ");
                Console.ResetColor();
            }         
        }


        //Method to execute different operations depending on users choice in the interface for DeleteName-part of program
        public bool HandleChoiceToDelete(string userinput)
        {
            if (userinput== "y")
            {
                foreach (var indexnumber in IndexOfNamesToDelete.OrderByDescending(x => x))
                {
                    classList.ClassList.RemoveAt(indexnumber);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDeletion completed.");
                Console.ResetColor();

                IndexOfNamesToDelete.Clear();
            }

            else
            {
                Console.WriteLine("Do you want to startover to enter names to delete?");
                string startOverYesNO= ReadChoiceYesNo();

                if (startOverYesNO=="y")
                {
                    Thread.Sleep(1000);
                    return true;
                }
            }
            
            return false;            
        }

              
        //Method for printing the generated teams
        public void PrintTeams()
        {            
            for (int i = 0; i < TeamBuilder.Teams.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine($"\nTeam {i+1}");
                Console.ResetColor();
                Console.WriteLine();

                foreach (var student in TeamBuilder.Teams[i])
                {
                    Console.WriteLine(student.NameOfStudent);
                }
            }
        }
    }
}
