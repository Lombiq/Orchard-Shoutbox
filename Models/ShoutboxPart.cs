using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace OrchardHUN.Shoutbox.Models
{
    public class ShoutboxPart : ContentPart<ShoutboxPartRecord>
    {
        [Required]
        public int MaxMessageCount
        {
            get { return Record.MaxMessageCount; }
            set { Record.MaxMessageCount = value; }
        }

        public int ProjectionId
        {
            get { return Record.ProjectionId; }
            set { Record.ProjectionId = value; }
        }
    }
}
