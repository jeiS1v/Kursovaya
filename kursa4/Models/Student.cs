using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kursa4.Models
{
    public class Student
    {
        public int id { set; get; }

        public string Name { set; get; }

        public string Surname { set; get; }

        public string Patronymic { set; get; }

        public DateTime Date { set; get; }

        public string State { set; get; }

        public string Course { set; get; }

    }

}
