using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ComText { get; set; }
        public string Ip { get; set; }
        public DateTime ComDate { get; set; }
        public int ArtId { get; set; }
    }
}
