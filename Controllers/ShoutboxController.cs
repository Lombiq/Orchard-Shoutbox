using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Themes;
using System.Web.Mvc;
using Orchard.Mvc;
using Orchard.ContentManagement;
using Orchard;
using Orchard.Localization;
using Orchard.DisplayManagement;
using OrchardHUN.Shoutbox.ViewModels;

namespace OrchardHUN.Shoutbox.Controllers
{
    [Themed]
    public class ShoutboxController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly dynamic _shapeFactory;

        public Localizer T { get; set; }

        public ShoutboxController(IOrchardServices orchardServices, IShapeFactory shapeFactory)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;

            T = NullLocalizer.Instance;
        }

        public ShapePartialResult GetMessages(int shoutboxId)
        {
            return null;

            var shape = _shapeFactory.DisplayTemplate(
                            TemplateName: "Messages",
                            Model: new ShoutboxMessageViewModel { },
                            Prefix: null);

            return new ShapePartialResult(this, shape);
        }

        [HttpPost]
        public ShapePartialResult SaveMessage(int shoutboxId)
        {
            if (_orchardServices.Authorizer.Authorize(Permissions.WriteMessage, T("You're not allowed to post messages.")))
            {
                var message = _contentManager.New("ShoutboxMessage");
                _contentManager.UpdateEditor(message, this);

                if (ModelState.IsValid)
                {
                    _contentManager.Create(message);
                }
            }

            return GetMessages(shoutboxId);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}
