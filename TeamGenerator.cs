using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamGenerator
{
    public class TeamGenerator
    {
        Random random = new Random();
        public List<List<Student>> Teams { get; private set; } = new ();
        
        public Roster ClassList { get; set; }

        public List<Student> AllStudents => ClassList.ClassList;

        public TeamGenerator(Roster classList)
        {
            ClassList = classList;
        }


        public int GetRandomNumber(int maxValue)
        {            
            int randomIndexNumber = random.Next(maxValue);
            return randomIndexNumber;
        }
        
        public void GenerateRandomTeams(int membersPerTeam)
        {
            Teams.Clear();

            List<Student> tempList = new(AllStudents);

            while(tempList.Count > membersPerTeam)
            {
                List<Student> team = new();

                for (int i = 0; i < membersPerTeam; i++)
                {
                    int index = GetRandomNumber(tempList.Count);

                    team.Add(tempList[index]);
                    tempList.RemoveAt(index);                    
                }

                Teams.Add(team);                               
            }

            if (tempList.Count > 0)
            {
                int teamIndex = 0;
                while (tempList.Count > 0)
                {
                    int index = GetRandomNumber(tempList.Count);
                    Teams[teamIndex].Add(tempList[index]);
                    tempList.RemoveAt(index);

                    teamIndex++;
                    if (teamIndex >= Teams.Count)
                        teamIndex = 0;
                }
            }
        }          
    }
}
