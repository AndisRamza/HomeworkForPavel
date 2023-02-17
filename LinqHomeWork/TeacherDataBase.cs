using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal static class TeacherDataBase
    {
        public static List<Teacher> Teachers
        {
            get
            {
                return new List<Teacher>()
                {
                    new Teacher(1,"Bariga","Korotkij",46,'M',1) {},
                    new Teacher(2,"Hloja","Kabachok",44,'W',1) {},
                    new Teacher(3,"Smuta","Izvilina",33,'W',2) {},
                    new Teacher(4,"Chernaja","Frika",35,'W',2) {},
                    new Teacher(5,"Lavashina","Kasha",28,'W',3) {},
                    new Teacher(6,"Turkov","Velikij",56,'M',3) {},
                    new Teacher(7,"Kabban","Velikij",29,'M',4) {},
                };
            }
        }
    }
}
