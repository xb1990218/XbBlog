using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public int IsAdmin { get; set; }
        public int IsDisable { get; set; }
    }
}
