using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamGenerator
{
    public class ConsoleUI
    {
        Roster classList { get; set; } = new Roster();

        TeamGenerator TeamBuilder;
        public ConsoleUI()
        {
            TeamBuilder = new TeamGenerator(classList);
        }

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
        private void PrintMenu()
        {
            Console.WriteLine("The Team Generator\n");
            Console.WriteLine("[1] Print class list");
            Console.WriteLine("[2] Delete name from class list");
            Console.WriteLine("[3] Add name to class list");
            Console.WriteLine("[4] Generate teams");
            Console.WriteLine("[5] Print teams");
            Console.WriteLine("[6] Exit program");
        }

        public int ReadMenuChoice(int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write("\nEnter your choice: ");
                string? userInput = Console.ReadLine().Trim();
                
                bool success = int.TryParse(userInput, out int parsedValue);
                if (success && parsedValue >= minValue && parsedValue <= maxValue)
                    {
                        return parsedValue;
                    }                  
                    
                Console.WriteLine($"\nPlease enter a valid number between {minValue} and {maxValue}.");
                Thread.Sleep(2000);  
            }
        }
      
        public bool HandleMenuChoice(int userChoice)
        {           
            switch (userChoice)
            {
                //[1] Print class list
                case 1:                    
                    Console.Clear();
                    PrintClassList();
                    ReturnToMenu();
                    break;

                //[2] Delete name from class list
                case 2:                    
                    Console.Clear();
                    PrintFeatureInMaking();
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
                    Console.WriteLine("Thank you for using the Team Generator!");
                    return false;
                    

                default:
                    Console.Clear();
                    Console.WriteLine("Oops, something went wrong.");
                    ReturnToMenu();
                    break;
            }
            return true;
        }

        public void PrintFeatureInMaking()
        {
            Console.WriteLine("Sorry this feature isn't available at the moment.");
        }

        public void ReturnToMenu()
        {
            Console.Write("\nPress Enter to return to menu ");
            Console.ReadLine();
        }

        //Method for printing classlist + numbers of students on classlist
        public void PrintClassList()
        {
            
            Console.WriteLine("Classlist: ");
            foreach (var student in classList.ClassList)
            {
                Console.WriteLine(student.NameOfStudent);
            }

            Console.WriteLine("\nNumber of students: " + classList.Count());
        } 

        //Metod for printing the generated teams
        public void PrintTeams()
        {            
            for (int i = 0; i < TeamBuilder.Teams.Count; i++)
            {
                Console.WriteLine($"\nTeam {i+1}");

                foreach (var student in TeamBuilder.Teams[i])
                {
                    Console.WriteLine(student.NameOfStudent);
                }
            }
        }
    }
}
