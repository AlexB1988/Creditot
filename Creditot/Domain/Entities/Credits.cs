using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditot.Domain.Entities
{
    public class Credits
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public long UsersCategoriesId { get; set; }
        public double? Sum { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UsersCategories? UsersCategories { get; set; }
    }
}
