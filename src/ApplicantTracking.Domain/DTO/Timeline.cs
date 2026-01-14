using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Enumerators;

namespace ApplicantTracking.Domain.DTO
{
    public class Timeline
    {
        public Timeline(int idAggregateRoot, TimelineTypes idTimelineType, string oldData, string newData)
        {
            IdAggregateRoot = idAggregateRoot;
            IdTimelineType = idTimelineType;
            OldData = oldData;
            NewData = newData;
            CreatedAt = DateTime.UtcNow;
        }

        public int IdTimeline { get; set; }
        public int IdAggregateRoot { get; set; } // IdCandidate
        public TimelineTypes IdTimelineType { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
