using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    internal class StudentMemberComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student firstMember, Student secondMember)
        {
            if (object.ReferenceEquals(firstMember, secondMember)) return true;
            return string.Equals(firstMember.Name, secondMember.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Student member)
        {
            return member.GetHashCode();
        }
    }
}
