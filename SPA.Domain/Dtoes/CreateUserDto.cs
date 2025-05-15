using SPA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Dtoes
{
    public class CreateUserDto
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string Family { get; set; }
        [Display(Name = "سن")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public int Age { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string Address { get; set; }
        [Display(Name = "تاریخ تولد")]
        public string PersianBrithday { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime Brithday
        {
            get
            {
                return DateTimeConverter.ShamsiToMiladi(PersianBrithday);
            }
            set
            {
            }
        }

        [Display(Name = "شماره تلفن")]
        // in new List<string>() zadam ke khataye null nade ke felan vase valid bardashtam
        [MinLength(1, ErrorMessage = "حداقل یک شماره تلفن الزامی است")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public List<string> PhoneNumbers { get; set; }
    }
}
