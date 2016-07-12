using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;
using OrchardHUN.Shoutbox.Models;

namespace OrchardHUN.Shoutbox
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition("ShoutboxMessageFieldsPart",
                part => part
                    .WithField("Message", field =>
                    {
                        field
                            .OfType("TextField")
                            .WithSetting("TextFieldSettings.Flavor", "small");
                    })
                );

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxMessage",
                cfg => cfg
                    .WithPart("ShoutboxMessageFieldsPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
                );


            ContentDefinitionManager.AlterTypeDefinition("ShoutboxWidget",
                cfg => cfg
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                    .WithPart(typeof(ShoutboxPart).Name)
                    .WithPart("IdentityPart")
                    .WithSetting("Stereotype", "Widget")
                );


            return 3;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("ShoutboxMessageFieldsPart",
                part => part
                    .WithField("Message", field => {
                        field
                            .WithSetting("TextFieldSettings.Flavor", "small");
                    })
                );

            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.DropTable("ShoutboxPartRecord");

            return 3;
        }

        public int UpdateFrom3()
        {
            ContentDefinitionManager.AlterTypeDefinition("ShoutboxWidget",
                cfg => cfg
                    .WithPart("IdentityPart"));

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxMessage",
                cfg => cfg
                    .WithPart("IdentityPart"));

            return 4;
        }
    }
}
