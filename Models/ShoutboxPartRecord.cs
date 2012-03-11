using Orchard.ContentManagement.Records;

namespace OrchardHUN.Shoutbox.Models
{
    public class ShoutboxPartRecord : ContentPartRecord
    {
        public virtual int MaxMessageCount { get; set; }
        public virtual int ProjectionId { get; set; }

        public ShoutboxPartRecord()
        {
            MaxMessageCount = 15;
        }
    }
}
