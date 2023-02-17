using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnLinqWithPavel
{
    public interface IDepartmentMember
    {
        int Id { get; set; }
        string Name { get; set; }
        string Surename { get; set; }
        int Age { get; set; }
        char Sex { get; set; }
        int DepartmentId { get; set; }
    }
}
