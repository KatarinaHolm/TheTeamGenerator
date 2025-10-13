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

        public string NameToAdd { get; set; } = "";

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

                Console.WriteLine();

                int choice = ReadIntChoice(1, 6);

                isRunning = HandleMenuChoice(choice);
            }
        }

       

        //Method to print menu on screen
        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("=== The Team Generator ===");
            Console.WriteLine();

            Console.ResetColor();

            Console.WriteLine("[1] Print class list");
            Console.WriteLine("[2] Delete name from class list");
            Console.WriteLine("[3] Add name to class list");
            Console.WriteLine("[4] Generate teams");
            Console.WriteLine("[5] Print teams");
            Console.WriteLine("[6] Exit program");
        }

        //MENU
        //Method to read and validate user choice on the menu
        public int ReadIntChoice(int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write("Enter your choice: ");
                string? userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    bool success = int.TryParse(userInput.Trim(), out int parsedValue);

                    if (success && parsedValue >= minValue && parsedValue <= maxValue)
                    {
                        return parsedValue;
                    }
                }

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter a valid number between {minValue} and {maxValue}.");
                Console.ResetColor();

                Thread.Sleep(1000);

                Console.WriteLine();
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
                    Console.WriteLine("=== Class List ===");
                    Console.WriteLine();
                    PrintClassList();
                    Console.WriteLine();
                    PrintClassListCount();
                    ReturnToMenu();
                    break;

                //[2] Delete name from class list
                case 2:
                    Console.Clear();
                    ShowInterfaceToDeleteName();
                    ReturnToMenu();
                    break;

                //[3] Add name to class list
                case 3:                    
                    Console.Clear();
                    ShowInterfaceToAdd();
                    ReturnToMenu();
                    break;

                //[4] Generate teams
                case 4:                    
                    Console.Clear();
                    ShowInterfaceToGenerateTeam();
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

                    Console.ReadLine();
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
            Console.Write("\nPress Enter to return to menu ");
            Console.ReadKey();            
        }

        //PRINT CLASS LIST
        //Method for printing classlist
        public void PrintClassList()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Class list");
            Console.ResetColor();
            Console.WriteLine();

           
            int count = 0;

            for (int i = 0; i < classList.ClassList.Count; i++)
            {
                Console.Write($"{i+1, -2} {classList.ClassList[i].NameOfStudent, -15}");

                if (count.Equals(4))
                {
                    Console.WriteLine("\n");
                    count = 0;
                }
                else
                {
                    count++;
                }                    
            }
                       
        } 

        //Method to print number of students on the class list
        public void PrintClassListCount()
        {            
            Console.WriteLine("Number of students on class list: " + classList.Count());
        }

       //DELETE NAME FROM CLASS LIST
        //Method to show deletion options 
        public void ShowInterfaceToDeleteName()
        {
           

            bool ContinueDeleting = true;

            while (ContinueDeleting)
            {
                Console.Clear();
                Console.WriteLine("== Delete Names From Class list ==");
                Console.WriteLine();

                PrintClassList();
                Console.WriteLine();

                PrintInstructionToDelete();

                ContinueDeleting = ReadIntToDelete(1, classList.Count());

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
            Console.WriteLine();
            Console.WriteLine("Enter number(s) of the students to delete from class list (comma-separated). Press Enter to return to menu.");                       
        }
                        
        //Method to validate user input
       
        public bool ReadIntToDelete(int minValue, int maxValue)
        {        
            while (true)
            {
                AskForNamesToDelete();
                string userInput = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    IndexOfNamesToDelete.Clear();
                    return false;
                }

                List<int> ParsedIndexes = SplitAndParseInput(userInput, minValue, maxValue);
                              
                if (ParsedIndexes.Count == 0)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;                   
                    Console.WriteLine($"Please enter valid number(s) between {minValue} and {maxValue} (separate several numbers with \",\").");
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
        public void AskForNamesToDelete()
        {
            Console.WriteLine();
            Console.Write("Enter number(s) (or Enter to return): ");            
        }


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
            Console.WriteLine("Do you want to delete the following names from the class list?");
            Console.WriteLine();

            foreach (var indexnumber in IndexOfNamesToDelete)
            {
                Console.WriteLine(classList.ClassList[indexnumber].NameOfStudent);
            }
            Console.WriteLine();
        }

        //Method to read and validate answer to Yes/No-question
        public string ReadChoiceYesNo()
        {
            string? userInput;
            while (true)
            {
                Console.Write("Enter your choice (Y/N): ");
                userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput))
                {
                    userInput = userInput.ToLower().Trim();

                    if (userInput == "y" || userInput == "n")
                    {
                        return userInput;
                    }
                }

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter valid answer (y/n)! ");
                Console.ResetColor();

                Thread.Sleep(1000);

                Console.WriteLine();
            }         
        }


        //Method to execute different operations depending on users choice in the interface for DeleteName-part of program
        //Note: Part of deleting should be moved to Roster?
        public bool HandleChoiceToDelete(string userinput)
        {
            if (userinput== "y")
            {
                foreach (var indexnumber in IndexOfNamesToDelete.OrderByDescending(x => x))
                {
                    classList.ClassList.RemoveAt(indexnumber);
                }

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Deletion completed.");
                Console.ResetColor();

                Thread.Sleep(500);

                IndexOfNamesToDelete.Clear();
            }

            else
            {
                Console.WriteLine();
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

        //ADD NAME TO CLASS LIST
        //Method to show interface of adding name
        public void ShowInterfaceToAdd()
        {
            

            bool ContinueAdding = true;

            while (ContinueAdding)
            {
                Console.Clear();
                Console.WriteLine("== Add Name To Class list ==");
                Console.WriteLine();

                PrintClassList();
                Console.WriteLine();

                PrintInstructionToAdd();

                ContinueAdding = ReadName();

                if (!ContinueAdding)
                {
                    return;
                }
                
                PrintNameToAdd();
                
                string userInput = ReadChoiceYesNo();

                ContinueAdding = HandleChoiceToAdd(userInput); // KOlla om behöver redigeras.
                
            }
        }

        //Method to Print instruction of adding
        public void PrintInstructionToAdd()
        {
            Console.Write("Enter name of student to add (or Enter to return to menu): "); 
        }

        //Method to reading name and validate user input
        public bool ReadName()
        {
            NameToAdd = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(NameToAdd))
            {
                return false;
            }

            NameToAdd = NameToAdd.Trim();

            return true;
            
        }

        //Ask to add name
        public void PrintNameToAdd()
        {
            Console.WriteLine();
            Console.Write($"Do you want to add {NameToAdd} to class list? ");
            Console.WriteLine();
        }

        // Method to handle Adding name
        //Note: Should the actual adding be moved to Roster
        public bool HandleChoiceToAdd(string userinput)
        {
            if (userinput == "y")
            {
                classList.ClassList.Add(new Student(NameToAdd));

                ConfirmAdding();

                NameToAdd = "";
            }

            else
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Name not added.");
                Console.ResetColor();

                Thread.Sleep(500);

                Console.WriteLine();
                Console.WriteLine("Do you want to startover to add a name?");

                string startOverYesNO = ReadChoiceYesNo();

                if (startOverYesNO == "y")
                {
                    Thread.Sleep(1000);
                    return true;
                }

                else
                {
                    return false;
                }
            }

            AskToContinueAdd();

            string userInput = ReadChoiceYesNo();

            if (userInput == "y")
            {
                Thread.Sleep(1000);

                return true;
            }
          
            return false;
        }

        //Method to confirm adding of name
        public void ConfirmAdding()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Name added to class list.");
            Console.ResetColor();

            Thread.Sleep(500);
        }

        //Method asking to add more names
        public void AskToContinueAdd()
        {
            Console.WriteLine();
            Console.WriteLine("Do you want to add another name?");
        }

        //GENERATE TEAMS
        //Metod to show interface of generating team
        public void ShowInterfaceToGenerateTeam()
        {      
            Console.Clear();

            Console.WriteLine("== Generate Teams ==\n");
            
            PrintClassListCount();

            AskNumberOfMembers();

            //Add return to menu? 

            int numberOfMembers = ReadIntChoice(2, 10);
            
            TeamBuilder.GenerateRandomTeams(numberOfMembers);

            Thread.Sleep(500);

            PrintCountDownGenerating();
                       
            Console.Clear();            
            
            PrintTeams();
            
        }

        //Method asking user to choose number of members per team
        public void AskNumberOfMembers()
        {
            Console.WriteLine();
            Console.WriteLine("How many members per team would you like?"); //Or Enter to return to Menu
        }

        public void PrintCountDownGenerating()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Generating teams");
            Thread.Sleep(500);

            Console.Write(" .");
            Thread.Sleep(500);

            Console.Write(" .");
            Thread.Sleep(500);

            Console.Write(" .");
            Thread.Sleep(1000);
        }

        //PRINT TEAMS     
        //Method for printing the generated teams five teams at a time,
        //Also handeling if there are less than five teams to print in a row. 
        public void PrintTeams()
        {
            Console.WriteLine("== Generated Teams ==");
            Console.WriteLine();

            if (TeamBuilder.Teams.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No teams have been generated yet.");
                Console.ResetColor();

                return;
            }          

            //Loop for printing 5 kolumns of teams at at time
            for (int i = 0; i < TeamBuilder.Teams.Count; i += 5)
            {
                //Printing team names of the five teams 
                for (int j = 0; j < 5; j++)
                {
                    if (i + j + 1 > TeamBuilder.Teams.Count)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"Team {i + j +1 , -10}");
                    Console.ResetColor();

                }

                Console.WriteLine("\n");                

                //Calculate highest number of members in the five teams we are going to print
                int maxMembers = TeamBuilder.Teams.Skip(i).Take(5).Max(team => team.Count);

              
                //Loop for making columns the right number of rows
                for (int k = 0; k < maxMembers; k++)
                {
                    //Loop for printing a row of one team member each from the five teams
                    for (int l = 0; l < 5; l++)
                    {
                        if (i + l >= TeamBuilder.Teams.Count)
                        {
                            break;
                        }

                        else if (TeamBuilder.Teams[i + l].Count > k)
                        {
                            Console.Write($"{TeamBuilder.Teams[i + l][k].NameOfStudent,-15}");
                        }

                        else
                        {
                            Console.Write("".PadRight(15));
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("\n");

            }
        }
    }
}
