using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Security.Permissions;

namespace OrchardHUN.Shoutbox
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission WriteMessage = new Permission { Name = "WriteShoutboxMessage" };

        public virtual Feature Feature { get; set; }

        public static Localizer T { get; set; }


        static Permissions()
        {
            T = NullLocalizer.Instance;

            WriteMessage.Description = T("Write Shoutbox messages.").Text;
        }


        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                WriteMessage
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] {WriteMessage}
                },
            };
        }

    }
}
