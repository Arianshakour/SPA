using SPA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Dtoes
{
    public class DetailsUserDto
    {
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
        public string PersianBrithday
        {
            get
            {
                return DateTimeConverter.MiladiToShamsi(Brithday);
            }
            set
            {
            }
        }

        public List<PNDtoForDetail> PhoneNumbers { get; set; } = new List<PNDtoForDetail>();

        public class PNDtoForDetail
        {
            public int? Id { get; set; }
            public string Number { get; set; }
        }
    }
}
