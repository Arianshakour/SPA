using SPA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Dtoes
{
    public class EditUserDto
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name = "سن")]
        public int Age { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime Brithday { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string PersianBrithday
        {
            get
            {
                return DateTimeConverter.MiladiToShamsi(Brithday);
            }
            set
            {
                Brithday = DateTimeConverter.ShamsiToMiladi(value);
            }
        }

        public List<PNDto> PhoneNumbers { get; set; } = new List<PNDto>();

        public class PNDto
        {
            public int? Id { get; set; }
            public string Number { get; set; }
        }
    }
}
