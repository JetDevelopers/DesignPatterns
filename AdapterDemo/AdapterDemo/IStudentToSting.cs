using System.Collections.Generic;

namespace AdapterDemo
{
    public interface IStudentDetailAdapter
    {
         string ListsPatterns(IEnumerable<Student> students);
    }
}