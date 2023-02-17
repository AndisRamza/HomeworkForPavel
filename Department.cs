using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> DepartmentMembers { get; set; }

        public Department(int id, string name, List<Teacher> departmentMembers) {
            Id = id;
            Name = name;
            DepartmentMembers = departmentMembers;
        }
    }
}
