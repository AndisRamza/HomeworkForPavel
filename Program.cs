using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal class Program
    {
        /* Functions are build for convenience.
         * And I know, that they violate SRP, OCP, Abstract level ,etc. */
        static void ConsoleWriteWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void DisplayLinqSelectExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Select  ===" +
                " \nSelect        Select Query", ConsoleColor.Green);

            var studentsNames = students.Select(student => student.Name).ToList();

            var studentsNamesQuery = from student in students
                                     select student.Name;
            var studentNamesTwoExamples = studentsNames.Zip(studentsNamesQuery, (regular, query) => regular + "\t\t" + query);
            foreach (var names in studentNamesTwoExamples)
            {
                Console.WriteLine(names);
            }
        }
        static void DisplayLinqWhereExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Where  ===" +
                            " \nWhere        Where Query", ConsoleColor.Green);

            var studentNamesOlderThan24 = students.Where(student => student.Age > 24).Select(student => student.Name).ToList();
            var studentNamesOlderThan24Query = from student in students
                                               where student.Age > 24
                                               select student.Name;

            var studentNamesOlderThan24TwoExamples = studentNamesOlderThan24.Zip(studentNamesOlderThan24Query,
                (regular, query) => regular + "\t\t" + query);

            foreach (var names in studentNamesOlderThan24TwoExamples)
            {
                Console.WriteLine(names);
            }
        }
        static void DisplayLinqOrderByExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  OrderBy  ===" +
                            " \nOrderBy        OrderBy Query", ConsoleColor.Green);

            var studentNamesOlderByNames = students.OrderBy(student => student.Name).Select(student => student.Name).ToList();
            var studentNamesOlderByNamesQuery = from student in students
                                                orderby student.Name
                                                select student.Name;

            var studentNamesOlderByNamesTwoExamples = studentNamesOlderByNames.Zip(studentNamesOlderByNamesQuery,
                (regular, query) => regular + "\t\t" + query);

            foreach (var names in studentNamesOlderByNamesTwoExamples)
            {
                Console.WriteLine(names);
            }
        }
        static void DisplayGroupsForGroupByExample(IEnumerable<IGrouping<int, Student>> studentGroup, IEnumerable<IGrouping<int, Student>> studentGroupQuery)
        {
            foreach ((var group, var groupQuery) in studentGroup.Zip(studentGroupQuery, (g1, g2) => (g1, g2)))
            {
                Console.WriteLine(group.Key + "\t\t" + groupQuery.Key);
                foreach ((var student, var studentQuery) in group.Zip(groupQuery, (s1, s2) => (s1, s2)))
                {
                    Console.WriteLine(student.Name + "\t\t" + studentQuery.Name);
                }
            }
        }
        static void DisplayLinqGroupByExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  GroupBy  ===" +
                            " \nGroupBy        GroupBy Query", ConsoleColor.Green);

            var studentsGroupedByDepartment = students.GroupBy(student => student.DepartmentId);
            var studentsGroupedByDepartmentQuery = from student in students
                                                   group student by student.DepartmentId;

            DisplayGroupsForGroupByExample(studentsGroupedByDepartment, studentsGroupedByDepartmentQuery);
        }
        static void DisplayLinqJoinExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Join  ===" +
                            " \nJoin        Join Query", ConsoleColor.Green);

            var anonTeachersStudentNames = students.Join(teachers,
                                                student => student.DepartmentId,
                                                teacher => teacher.DepartmentId,
                                                (student, teacher) => new
                                                {
                                                    StudentName = student.Name,
                                                    TeacherName = teacher.Name
                                                }).GroupBy(names => names.TeacherName);

            var anonStudentsTeachersNamesQuery = from student in students
                                                 join teacher in teachers
                                                 on student.DepartmentId equals teacher.DepartmentId
                                                 select new
                                                 {
                                                     StudentName = student.Name,
                                                     TeacherName = teacher.Name
                                                 };
            var anonTeacherStudentNamesQuery = from names in anonStudentsTeachersNamesQuery
                                               group names by names.TeacherName;

            #region PrintStuff
            foreach ((var group, var groupQuery) in anonTeachersStudentNames.Zip(anonTeacherStudentNamesQuery, (g1, g2) => (g1, g2)))
            {
                Console.WriteLine(group.Key + "\t\t" + groupQuery.Key);
                foreach ((var student, var studentQuery) in group.Zip(groupQuery, (s1, s2) => (s1, s2)))
                {
                    Console.WriteLine($"  {student.StudentName}  \t  {studentQuery.StudentName}");
                }
            }
            #endregion
        }
        static void DisplayLinqAllExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  All  ===", ConsoleColor.Green);

            var areAllStudentsOlderThan30 = students.All(student => student.Age > 30);

            Console.WriteLine($"Students are older than 30 : {areAllStudentsOlderThan30}");
        }
        static void DisplayLinqAnyExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Any  ===", ConsoleColor.Green);

            var areAnyStudentsOlderThan30 = students.Any(student => student.Age > 30);

            Console.WriteLine($"Any of student is older than 30 : {areAnyStudentsOlderThan30}");
        }
        static void DisplayLinqContainsExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Contains  ===", ConsoleColor.Green);

            var isArnoldStudent = students.Select(student => student.Name).Contains("Arnold");


            Student studentMaris2 = new Student(8, "Maris", "Velikij", 19, 'M', 1);
            var studentMaris = students.Where(student => student.Name == "Maris").First();
            var isMarisInStudentList = students.Contains(studentMaris, new StudentMemberComparer());
            var isMarisInStudentList2 = students.Contains(studentMaris2);

            Console.WriteLine($"Is Arnold a student : {isArnoldStudent}");
            Console.WriteLine($"Is Maris in student list with comparer: {isMarisInStudentList}");
            Console.WriteLine($"Is Maris in student list with IEquatable: {isMarisInStudentList2}");

        }
        static void DisplayLinqMaxExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Max  ===", ConsoleColor.Green);

            var oldestStudentAge = students.Max(student => student.Age);

            Console.WriteLine($"Oldest student age is : {oldestStudentAge}");

        }
        static void DisplayLinqCountExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Count  ===", ConsoleColor.Green);

            var numOfStudentsOlder25 = students.Count(student => student.Age > 25);

            Console.WriteLine($"Number of students older than 25 : {numOfStudentsOlder25}");

        }
        static void DisplayLinqSumExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Sum  ===", ConsoleColor.Green);
            var studentsAndTeachers = new List<IDepartmentMember>();

            studentsAndTeachers.AddRange(students);
            studentsAndTeachers.AddRange(teachers);

            var numOfMaleAmongStudentsTeachers = studentsAndTeachers.Sum(studAndTeach =>
            {
                if (studAndTeach.Sex == 'M') return 1;
                else return 0;
            });

            Console.WriteLine($"Number of male students: {numOfMaleAmongStudentsTeachers}");

        }
        static void DisplayLinqFirstAndFirstDefaultExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  First and FirstOrDefault  ===", ConsoleColor.Green);

            try
            {
                var studentKartoshka = students.First(student => student.Name == "Kartoshka");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No no, there is no student Kartoshka");
            }

            var studentKartoshkaDefault = students.FirstOrDefault(student => student.Name == "Kartoshka");
            Console.WriteLine($"Kartoshka is {studentKartoshkaDefault}"); // null ;(
        }
        static void DisplayLinqSingleAndSingleDefaultExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Single and SingleOrDefault  ===", ConsoleColor.Green);

            try
            {
                var oneArnold = students.Single(student => student.Name == "Igor");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No no, there is more than one student Igor");
            }

            var studentKartoshkaDefault = students.SingleOrDefault(student => student.Name == "Makaroooni");
            Console.WriteLine($"Makaroooni is {studentKartoshkaDefault}"); // null ;(
        }
        static void DisplayLinqSkipAndSkipWhileExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Skip and SkipWhile  ===", ConsoleColor.Green);

            var studentsAfterFifth = students.Skip(5).ToList();
            var studentsAfterFifthSkipWhile = students.SkipWhile(student => student.Id <= 5).ToList();

            var studentsNamesOfTwoExamples = studentsAfterFifth.Zip(studentsAfterFifthSkipWhile, (firstExamp, secondExamp) => firstExamp.Name + "\t\t" + secondExamp.Name);
            foreach (var names in studentsNamesOfTwoExamples)
            {
                Console.WriteLine($"\t{names}");
            }

        }
        static void DisplayLinqTakeAndTakeWhileExample(List<Student> students, List<Teacher> teachers)
        {
            ConsoleWriteWithColor("\n  ===  Linq  Take and TakeWhile  ===", ConsoleColor.Green);

            var firstTwoStudents = students.Take(2).ToList();
            var firstTwoStudentsTakeWhile = students.TakeWhile(student => student.Id <= 2).ToList();

            var studentsNamesOfTwoExamples = firstTwoStudents.Zip(firstTwoStudentsTakeWhile, (firstExamp, secondExamp) => firstExamp.Name + "\t\t" + secondExamp.Name);
            foreach (var names in studentsNamesOfTwoExamples)
            {
                Console.WriteLine($"\t{names}");
            }
        }

        static void Main(string[] args)
        {
            List<Student> students = StudentDataBase.Students;
            List<Teacher> teachers = TeacherDataBase.Teachers;


            //DisplayLinqSelectExample(students, teachers);

            //DisplayLinqWhereExample(students, teachers);

            //DisplayLinqOrderByExample(students, teachers);

            //DisplayLinqGroupByExample(students, teachers);

            //DisplayLinqJoinExample(students, teachers);

            //DisplayLinqAllExample(students, teachers);

            //DisplayLinqAnyExample(students, teachers);

            DisplayLinqContainsExample(students, teachers); /* Inside of Student class, there is commented out another aproach */

            //DisplayLinqMaxExample(students, teachers);

            //DisplayLinqCountExample(students, teachers);

            //DisplayLinqSumExample(students, teachers);

            //DisplayLinqFirstAndFirstDefaultExample(students, teachers);

            //DisplayLinqSingleAndSingleDefaultExample(students, teachers);

            //DisplayLinqSkipAndSkipWhileExample(students, teachers);

            //DisplayLinqTakeAndTakeWhileExample(students, teachers);

        }
    }
}
