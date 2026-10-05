using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp2
{
    public class Student : Person
    {
        public int Grade { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Grade}";
        }
    }
}
