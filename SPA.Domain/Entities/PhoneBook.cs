using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Entities
{
    public class PhoneBook
    {
        [Key]
        public int PhonebookId { get; set; }
        public string Phonebook { get; set; }

        public int UserId { get; set; }

        //Relation
        [ForeignKey("UserId")]
        public User user { get; set; }
    }
}
