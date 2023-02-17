using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal class Teacher : IDepartmentMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }
        public int DepartmentId { get; set; }

        public Teacher(int id, string name, string surename, int age, char sex, int departmentId)
        {
            Id = id;
            Name = name;
            Surename = surename;
            Age = age;
            Sex = sex;
            DepartmentId = departmentId;
        }
    }
}
