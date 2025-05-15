using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime Brithday { get; set; }
        public bool Dlt { get; set; } = false;

        //Relation

        public List<PhoneBook> phones { get; set; }
    }
}
