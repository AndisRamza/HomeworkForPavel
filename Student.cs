using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal class Student : IEquatable<Student>, IDepartmentMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }
        public int DepartmentId { get; set; }
        public Student(int id, string name, string surename, int age, char sex, int departmentId)
        {
            Id = id;
            Name = name;
            Surename = surename;
            Age = age;
            Sex = sex;
            DepartmentId = departmentId;
        }

        /* Another option is overriding object inherited function  */
        //public override bool Equals(object obj)
        //{
        //    var other = (Student)obj;
        //    return this.Id == other.Id &&
        //            this.Name == other.Name &&
        //             this.Surename == other.Surename &&
        //              this.Age == other.Age &&
        //               this.Sex == other.Sex &&
        //                this.DepartmentId == other.DepartmentId;
        //}

        public bool Equals(Student other)
        {
            return Id == other.Id &&
                    Name == other.Name &&
                     Surename == other.Surename &&
                      Age == other.Age &&
                       Sex == other.Sex &&
                        DepartmentId == other.DepartmentId;
        }

    }
}
