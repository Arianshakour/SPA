using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Domain.Entities
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List<User> UserList { get; set; }
        public Pager pager { get; set; }
        public List<DropDown> dropDowns { get; set; } = new List<DropDown>
        {
            new DropDown { value = 4, text = "4" },
            new DropDown { value = 8, text = "8" },
            new DropDown { value = -1, text = "همه" }
        };
        public List<DropDown> dropDownsForSort { get; set; } = new List<DropDown>
        {
            new DropDown {value = 11 , text = "جدید ترین"},
            new DropDown {value = 22 , text = "قدیمی ترین"}
        };
        public UserViewModel()
        {
            user = new User();
            UserList = new List<User>();
            pager = new Pager();
            //DeletedUserList = new List<User>();
        }
        //public List<User> DeletedUserList { get; set; }
        //public int pageSizeSelector { get; set; }
    }
}
