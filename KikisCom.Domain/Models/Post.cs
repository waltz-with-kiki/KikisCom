using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KikisCom.Domain.Models
{
    public class Post
    {
        public Guid id { get; set; }

        public string? text { get; set; }
        public string? photoPath { get; set; }
        public DateTime dateCreate { get; set; } = DateTime.UtcNow.AddHours(7);
        public string? data { get; set; }
    }
}
