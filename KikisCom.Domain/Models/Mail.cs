using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KikisCom.Domain.Models
{
    public class Mail
    {
        [Key]
        public Guid Id { get; set; }
        [EmailAddress]
        public string FromMail { get; set; }
        public string SMTPPatch { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPPassword { get; set; }
    }
}
