using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditot.Domain.Entities
{
    public class UsersCategories
    {
        public long Id { get; set; }

        public long UsersId { get; set; }
        public Users? Users { get; set; }
        public long CategoriesId { get; set; }
        public Categories? Categories { get; set; }
        public List<Credits> Credits { get; set; } = new();
    }
}
