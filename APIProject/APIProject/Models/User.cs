using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class User
    {
        public User()
        {
            Profiles = new HashSet<Profile>();
            TrailComments = new HashSet<TrailComment>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<TrailComment> TrailComments { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
