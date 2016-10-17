using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using OrchardHUN.Shoutbox.Models;
using System.Collections.Generic;
using OrchardHUN.Shoutbox.Services;
using Orchard.ContentManagement.Handlers;
using Orchard.Projections.Models;

namespace OrchardHUN.Shoutbox.Drivers
{
    public class ShoutboxPartDriver : ContentPartDriver<ShoutboxPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IShoutboxUiService _shoutboxUiService;

        protected override string Prefix
        {
            get { return "OrchardHUN.Shoutbox"; }
        }


        public ShoutboxPartDriver(
            IContentManager contentManager,
            IShoutboxUiService shoutboxUiService)
        {
            _contentManager = contentManager;
            _shoutboxUiService = shoutboxUiService;
        }


        protected override DriverResult Display(ShoutboxPart part, string displayType, dynamic shapeHelper)
        {
            var results = new List<DriverResult>(3);

            results.Add(
                ContentShape("Parts_ShoutboxPart_Messages", () => shapeHelper.Parts_ShoutboxPart_Messages(MessageListShape: _shoutboxUiService.CreateShoutboxMessageListShape(part.Id)))
                );

            if (part.ProjectionId != 0)
            {
                results.Add(
                    ContentShape("Parts_ShoutboxPart_ProjectionLink", 
                        () => shapeHelper.Parts_ShoutboxPart_ProjectionLink(Projection: _contentManager.Get(part.ProjectionId)))
                        );
            }

            results.Add(
                ContentShape("Parts_ShoutboxPart_Form",
                    () =>
                    {
                        var message = _contentManager.New("ShoutboxMessage");

                        return shapeHelper.Parts_ShoutboxPart_Form(MessageEditorShape: _contentManager.BuildEditor(message));
                    })
                );

            return Combined(results.ToArray());
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

        protected override void Exporting(ShoutboxPart part, ExportContentContext context)
        {
            ExportInfoset(part, context);

            if (part.ProjectionId > 0)
            {
                var projectionPart = _contentManager.Get<ProjectionPart>(part.ProjectionId);
                if (projectionPart != null)
                {
                    var projectionIdentity = _contentManager.GetItemMetadata(projectionPart).Identity;
                    context.Element(part.PartDefinition.Name).SetAttributeValue("ProjectionId", projectionIdentity.ToString());
                }
            }
        }

        protected override void Importing(ShoutboxPart part, ImportContentContext context)
        {
            ImportInfoset(part, context);
        }

        protected override void ImportCompleted(ShoutboxPart part, ImportContentContext context)
        {
            var projection = context.Attribute(part.PartDefinition.Name, "ProjectionId");
            if (projection != null)
            {
                part.ProjectionId = context.GetItemFromSession(projection).As<ProjectionPart>().Id;
            }
        }
    }
}
