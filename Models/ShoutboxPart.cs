using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace OrchardHUN.Shoutbox.Models
{
    public class ShoutboxPart : ContentPart
    {
        [Required]
        public int MaxMessageCount
        {
            get { return this.Retrieve(x => x.MaxMessageCount); }
            set { this.Store(x => x.MaxMessageCount, value); }
        }

        public int ProjectionId
        {
            get { return this.Retrieve(x => x.ProjectionId); }
            set { this.Store(x => x.ProjectionId, value); }
        }
    }
}
