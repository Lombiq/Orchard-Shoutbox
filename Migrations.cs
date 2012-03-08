using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Core.Contents.Extensions;
using Orchard.ContentManagement.MetaData;

namespace OrchardHUN.Shoutbox
{
    class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            //SchemaBuilder.CreateTable(typeof(ShoutboxMessagePartRecord).Name,
            //    table => table
            //        .ContentPartRecord()
            //        .Column<int>("ShoutboxId")
            //);

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

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxBoxWidget",
                cfg => cfg
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart")
                    .WithSetting("Stereotype", "Widget")
            );


            return 1;
        }
    }
}
