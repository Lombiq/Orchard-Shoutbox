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
                        field.OfType("TextField");
                    })
            );

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxMessage",
                cfg => cfg
                    .WithPart("ShoutboxMessageFieldsPart")
                    .WithPart("CommonPart")
            );

            SchemaBuilder.CreateTable(typeof(ShoutboxPartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column<int>("MaxMessageCount")
            );

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxWidget",
                cfg => cfg
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                    .WithPart(typeof(ShoutboxPart).Name)
                    .WithSetting("Stereotype", "Widget")
            );


            return 1;
        }
    }
}
