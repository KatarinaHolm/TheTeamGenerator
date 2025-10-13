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
                new Student("Alexis"),
                new Student("Robin"),
                new Student("Charlie"),
                new Student("Nova"),
                new Student("Elliot"),
                new Student("Luna"),
                new Student("Sam"),
                new Student("Mira"),
                new Student("Riley"),
                new Student("Jade"),
                new Student("Leo"),
                new Student("Tove"),
                new Student("Felix"),
                new Student("Noah"),
                new Student("Alva"),
                new Student("Elin"),
                new Student("Viktor"),
                new Student("Siri"),
                new Student("Matteo"),
                new Student("Stella"),
                new Student("Liv"),
                new Student("Adrian"),
                new Student("Milo"),
                new Student("Freja"),
                new Student("Linnea"),
                new Student("Tilde"),
                new Student("Oliver"),
                new Student("Nora"),
                new Student("Casper"),
                new Student("Ebba"),
                new Student("Elton"),
                new Student("Saga"),
                new Student("David"),
                new Student("Rami"),
                new Student("Louise"),
                new Student("Isa"),
                new Student("William"),
                new Student("Emil"),
                new Student("Naomi"),
                new Student("Hugo")

            };
        }
        
        public int Count()
        {
            return ClassList.Count();  
        }
              
    }
}
