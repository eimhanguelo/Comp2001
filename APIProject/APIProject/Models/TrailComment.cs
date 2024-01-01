using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class TrailComment
    {
        public int TrailCommentId { get; set; }
        public int TrailId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Trail Trail { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
