using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creditot.Domain.Entities
{
    public class Users
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Appeals> Appeals { get; set; } = new();
        public List<Categories> Categories { get; set; } = new();
        public List<UsersCategories> UsersCategories { get; set; } = new();
    }
}