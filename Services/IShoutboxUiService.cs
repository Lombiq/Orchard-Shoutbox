using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;

namespace OrchardHUN.Shoutbox.Services
{
    public interface IShoutboxUiService : IDependency
    {
        dynamic CreateShoutboxMessageListShape(int shoutboxId);
    }
}
