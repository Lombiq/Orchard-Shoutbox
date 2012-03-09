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
using Orchard.Core.Common.Models;
using Orchard.Data;
using OrchardHUN.Shoutbox.Models;

namespace OrchardHUN.Shoutbox.Controllers
{
    [Themed]
    public class ShoutboxController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly dynamic _shapeFactory;

        public Localizer T { get; set; }

        public ShoutboxController(
            IOrchardServices orchardServices, 
            ITransactionManager transactionManager, 
            IShapeFactory shapeFactory)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _transactionManager = transactionManager;
            _shapeFactory = shapeFactory;

            T = NullLocalizer.Instance;
        }

        public ShapePartialResult GetMessages(int shoutboxId)
        {
            var shoutbox = _contentManager.Get<ShoutboxPart>(shoutboxId);

            var messages = _contentManager
                                    .Query("ShoutboxMessage")
                                    .Where<CommonPartRecord>(record => record.Container.Id == shoutboxId)
                                    .OrderByDescending(record => record.CreatedUtc)
                                    .Slice(shoutbox.MaxMessageCount);

            var messageShapes = messages.Select(message => _contentManager.BuildDisplay(message, "ShoutboxWidget"));

            var shape = _shapeFactory.DisplayTemplate(
                            TemplateName: "MessageList",
                            Model: new ShoutboxMessageViewModel { MessageShapes = messageShapes },
                            Prefix: null);

            return new ShapePartialResult(this, shape);
        }

        [HttpPost]
        public void SaveMessage(int shoutboxId)
        {
            if (_orchardServices.Authorizer.Authorize(Permissions.WriteMessage))
            {
                // This below is the standard workflow, see Orchard.Core.Contents.Controllers.AdminController or http://orchard.codeplex.com/workitem/18412
                // To summarize: to modify fields properly, the content item has to be created first. Since the Message has a TextField, the item
                // has to be first created, then updated with POST data. Since that UpdateEditor() also sets if the model is valid, if it happens not
                // to be then we have to roll back that Create() with TransactionManager.
                // Updating (validating) the item causes the problem described in the issue.
                var message = _contentManager.New("ShoutboxMessage");
                _contentManager.Create(message);
                _contentManager.UpdateEditor(message, this);

                if (ModelState.IsValid)
                {
                    message.As<CommonPart>().Container = _contentManager.Get(shoutboxId);
                }
                else _transactionManager.Cancel();
            }
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
