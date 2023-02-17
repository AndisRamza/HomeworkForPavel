using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal static class StudentDataBase
    {
        public static List<Student> Students
        {
            get
            {
                return new List<Student>()
                {
                    new Student(1,"Arnold","Swartch",42,'M',1) {},
                    new Student(2,"Samanta","Kabachok",19,'W',4) {},
                    new Student(3,"Irina","Izvilina",18,'W',1) {},
                    new Student(4,"Vika","Frika",17,'W',2) {},
                    new Student(5,"Masha","Kasha",22,'W',1) {},
                    new Student(6,"Igor","Velikij",19,'M',2) {},
                    new Student(7,"Igor","Velikij",19,'M',4) {},
                    new Student(8,"Maris","Velikij",19,'M',1) {},
                    new Student(9,"Vladik","Suharev",28,'M',2) {},
                    new Student(10,"Anton","Krapiva",26,'M',1) {},
                    new Student(11,"Marija","Bik",20,'W',1) {},
                    new Student(12,"Sveta","Tjmova",33,'W',3) {},
                    new Student(13,"Genadij","Krockodile",38,'M',3) {},
                    new Student(14,"Sabina","Grudistnaja",17,'W',3) {},
                    new Student(15,"Viktor","Serjeznij",22,'M',4) {},
                    new Student(16,"Alla","Smeshnaja",33,'W',4) {},
                    new Student(17,"Alla","Smeshnaja",28,'W',1) {},
                };
            }
        }
    }
}
