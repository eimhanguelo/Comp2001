using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class TrailCommentsView
    {
        public int TrailId { get; set; }
        public string TrailName { get; set; } = null!;
        public string? TrailDescription { get; set; }
        public string? TrailLocation { get; set; }
        public string? TrailDifficulty { get; set; }
        public decimal? TrailLength { get; set; }
        public decimal? TrailElevationGain { get; set; }
        public decimal? TrailElevationLoss { get; set; }
        public string? TrailFeatures { get; set; }
        public int TrailCommentId { get; set; }
        public int CommentUserId { get; set; }
        public string? CommentUsername { get; set; }
        public string? TrailComment { get; set; }
        public DateTime? CommentDateCreated { get; set; }
    }
}
