﻿using Orchard.ContentManagement.MetaData;
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
                    .WithIdentity()
                );


            ContentDefinitionManager.AlterTypeDefinition("ShoutboxWidget",
                cfg => cfg
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                    .WithPart(typeof(ShoutboxPart).Name)
                    .WithSetting("Stereotype", "Widget")
                    .WithIdentity()
                );


            return 4;
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
                    .WithIdentity()
                );

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxMessage",
                cfg => cfg
                    .WithIdentity()
                );

            return 4;
        }
    }
}
