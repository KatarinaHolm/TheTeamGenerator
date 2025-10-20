using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTeamGenerator
{
    //Student is a own class cause I have an idea to save information about which classmates the student have been in group with before.
    public class Student
    {
        public string NameOfStudent { get; set; }

        public Student(string name)
        {
            NameOfStudent = name;
        }
    }
}
