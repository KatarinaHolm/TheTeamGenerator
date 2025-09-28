using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamGenerator
{
    public class Roster
    {
        //Property
        public List<Student> ClassList { get; set; }

        //Constructor
        public Roster()
        {
            ClassList = new List<Student>
            {
                new Student("Anna M"),
                new Student("Juri"),
                new Student("Frida"),
                new Student("Shimeng"),
                new Student("Anna S"),
                new Student("Ali"),
                new Student("Ida"),
                new Student("Alexander"),
                new Student("Erik A"),
                new Student("Sara"),
                new Student("Jonas"),
                new Student("Erik B"),
                new Student("Mohammad"),
                new Student("Gustaf")
            };
        }
        
        public int Count()
        {
            return ClassList.Count();  
        }

    }
}
