using System;
using System.Collections.Generic;

namespace WpfApp.Models.Decanat
{
   internal class Student
   {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Burthday { get; set; }
        public double Rating { get; set; }
   }

    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
