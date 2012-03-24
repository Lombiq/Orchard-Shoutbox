using System;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.DisplayManagement;
using Orchard.Forms.Services;
using Orchard.Localization;
using Orchard.Projections.Descriptors.Filter;
using Orchard.Widgets.Models;

namespace OrchardHUN.Shoutbox.Projections
{
    public class ShoutboxFilter : Orchard.Projections.Services.IFilterProvider
    {
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }

        public ShoutboxFilter(IContentManager contentManager)
        {
            _contentManager = contentManager;

            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeFilterContext describe)
        {
            describe.For("Content", T("Shoutbox"), T("Shoutbox"))
                .Element("ShoutboxMessages", T("Shoutbox Messages"), T("Messages from a Shoutbox widget"),
                    ApplyFilter,
                    DisplayFilter,
                    "ShoutboxMessagesFilter"
                );
        }

        public void ApplyFilter(FilterContext context)
        {
            context.Query.ForType("ShoutboxMessage");

            if (String.IsNullOrEmpty(context.State.ShoutboxWidget)) return;

            context.Query.Where(x => x.ContentPartRecord<CommonPartRecord>(), x => x.Eq("Container.Id", context.State.ShoutboxWidget));
        }

        public LocalizedString DisplayFilter(FilterContext context)
        {
            if (String.IsNullOrEmpty(context.State.ShoutboxWidget))
            {
                return T("Any Shoutbox message");
            }

            int widgetId = int.Parse(context.State.ShoutboxWidget);
            return T("Messages from the Shoutbox \"{0}\"", _contentManager.Get<WidgetPart>(widgetId).Title);
        }
    }

    public class ContentTypesFilterForms : IFormProvider
    {
        private dynamic _shapeFactory { get; set; }
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }

        public ContentTypesFilterForms(
            IShapeFactory shapeFactory,
            IContentManager contentManager)
        {
            _shapeFactory = shapeFactory;
            _contentManager = contentManager;

            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context)
        {
            Func<IShapeFactory, object> form =
                shape =>
                {
                    var f = _shapeFactory.Form(
                        Id: "FromShoutboxWidget",
                        _Parts: _shapeFactory.SelectList(
                            Id: "shoutbox-widget", Name: "ShoutboxWidget",
                            Title: T("Shoutbox Widget"),
                            Description: T("Select a Shoutbox widget."),
                            Size: 10,
                            Multiple: false
                            )
                        );

                    foreach (var widget in _contentManager.Query<WidgetPart, WidgetPartRecord>("ShoutboxWidget").List())
                    {
                        f._Parts.Add(new SelectListItem { Value = widget.Id.ToString(), Text = widget.Title });
                    }

                    return f;
                };

            context.Form("ShoutboxMessagesFilter", form);

        }
    }
}
