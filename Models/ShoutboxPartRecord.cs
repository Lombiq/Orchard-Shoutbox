using Orchard.ContentManagement.Records;

namespace OrchardHUN.Shoutbox.Models
{
    public class ShoutboxPartRecord : ContentPartRecord
    {
        public virtual int MaxMessageCount { get; set; }

        public ShoutboxPartRecord()
        {
            MaxMessageCount = 15;
        }
    }
}
