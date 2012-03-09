using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Drivers;
using OrchardHUN.Shoutbox.Models;
using Orchard.ContentManagement;

namespace OrchardHUN.Shoutbox.Drivers
{
    public class ShoutboxPartDriver : ContentPartDriver<ShoutboxPart>
    {
        private readonly IContentManager _contentManager;

        protected override string Prefix
        {
            get { return "OrchardHUN.Shoutbox"; }
        }

        public ShoutboxPartDriver(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        protected override DriverResult Display(ShoutboxPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_ShoutboxPart",
                () =>
                {
                    var message = _contentManager.New("ShoutboxMessage");

                    return shapeHelper.Parts_ShoutboxPart(MessageEditorShape: _contentManager.BuildEditor(message));
                });
        }

        // GET
        protected override DriverResult Editor(ShoutboxPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ShoutboxPart_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.ShoutboxPart",
                    Model: part,
                    Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(ShoutboxPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
