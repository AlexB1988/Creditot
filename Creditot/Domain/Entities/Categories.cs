using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditot.Domain.Entities
{
    public class Categories
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Users> Users = new();
        public List<UsersCategories> UsersCategories { get; set; } = new();
    }
}
