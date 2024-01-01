using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
