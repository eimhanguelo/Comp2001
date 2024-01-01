using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class UserProfile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
