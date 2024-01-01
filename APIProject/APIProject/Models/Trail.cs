using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class Trail
    {
        public Trail()
        {
            TrailComments = new HashSet<TrailComment>();
        }

        public int TrailId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Difficulty { get; set; }
        public decimal? Length { get; set; }
        public decimal? ElevationGain { get; set; }
        public decimal? ElevationLoss { get; set; }
        public string? Features { get; set; }

        public virtual ICollection<TrailComment> TrailComments { get; set; }
    }
}
