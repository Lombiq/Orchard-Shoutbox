using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using System.ComponentModel.DataAnnotations;

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
    }
}
