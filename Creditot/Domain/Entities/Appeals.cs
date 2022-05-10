using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditot.Domain.Entities
{
    public class Appeals
    {
        public long Id { get; set; }
        public Users? Users { get; set; }
    }
}
