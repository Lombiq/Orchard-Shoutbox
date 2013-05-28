using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Themes;
using OrchardHUN.Shoutbox.Models;
using OrchardHUN.Shoutbox.ViewModels;
using OrchardHUN.Shoutbox.Services;

namespace OrchardHUN.Shoutbox.Controllers
{
    [Themed]
    public class ShoutboxController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _transactionManager;
        private readonly IShoutboxUiService _shoutboxUiService;

        public Localizer T { get; set; }

        public ShoutboxController(
            IOrchardServices orchardServices, 
            ITransactionManager transactionManager,
            IShoutboxUiService shoutboxUiService)
        {
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _transactionManager = transactionManager;
            _shoutboxUiService = shoutboxUiService;

            T = NullLocalizer.Instance;
        }

        // This is not really needed now, but we may add auto-update functionality (like in a chat window) later
        public ShapePartialResult FetchMessages(int shoutboxId)
        {
            return new ShapePartialResult(this, _shoutboxUiService.CreateShoutboxMessageListShape(shoutboxId));
        }

        [HttpPost]
        public ShapePartialResult SaveMessage(int shoutboxId)
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

            return FetchMessages(shoutboxId);
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
