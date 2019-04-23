using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA.Models
{
    public class Reciver : Entity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public ICollection<Mail> Mails { get; set; } = new List<Mail>();
    }
}
