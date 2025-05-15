using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Dtoes
{
    public class DeleteUserDto
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }

        public List<PNDtoForDelete> PhoneNumbers { get; set; } = new List<PNDtoForDelete>();

        public class PNDtoForDelete
        {
            public int? Id { get; set; }
            public string Number { get; set; }
        }
    }
}
