using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using OrchardHUN.Shoutbox.Models;

namespace OrchardHUN.Shoutbox.Handlers
{
    public class ShoutboxPartHandler : ContentHandler
    {
        public ShoutboxPartHandler(IRepository<ShoutboxPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
