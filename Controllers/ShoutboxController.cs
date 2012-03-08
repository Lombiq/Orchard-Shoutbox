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

namespace OrchardHUN.Shoutbox.Controllers
{
    [Themed]
    public class ShoutboxController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;

        public Localizer T { get; set; }

        public ShoutboxController(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }

        public ShapePartialResult GetMessages(int shoutboxId)
        {
            return null;
        }

        [HttpPost]
        public ShapePartialResult SaveMessage(int shoutboxId)
        {
            if (_orchardServices.Authorizer.Authorize(Permissions.ManageBlogs, T("Not allowed to create blogs")))
            {
                var message = _orchardServices.ContentManager.New("ShoutboxMessage");
                _orchardServices.ContentManager.UpdateEditor(message, this);

                if (ModelState.IsValid)
                {
                    _orchardServices.ContentManager.Create(message);
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
