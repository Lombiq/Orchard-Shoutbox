using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardHUN.Shoutbox.Models;
using Orchard.Core.Common.Models;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using OrchardHUN.Shoutbox.ViewModels;

namespace OrchardHUN.Shoutbox.Services
{
    public class ShoutboxUiService : IShoutboxUiService
    {
        private readonly IContentManager _contentManager;
        private readonly dynamic _shapeFactory;

        public ShoutboxUiService(
            IContentManager contentManager,
            IShapeFactory shapeFactory)
        {
            _contentManager = contentManager;
            _shapeFactory = shapeFactory;
        }

        public dynamic CreateShoutboxMessageListShape(int shoutboxId)
        {
            var shoutbox = _contentManager.Get<ShoutboxPart>(shoutboxId);

            var messages = _contentManager
                                    .Query("ShoutboxMessage")
                                    .Where<CommonPartRecord>(record => record.Container.Id == shoutboxId)
                                    .OrderByDescending(record => record.CreatedUtc)
                                    .Slice(shoutbox.MaxMessageCount);

            var messageShapes = messages.Select(message => _contentManager.BuildDisplay(message, "ShoutboxWidget"));

            return _shapeFactory.DisplayTemplate(
                                    TemplateName: "MessageList",
                                    Model: new ShoutboxMessageViewModel { MessageShapes = messageShapes },
                                    Prefix: null);
        }
    }
}
