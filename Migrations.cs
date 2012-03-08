using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using Orchard.Core.Contents.Extensions;
using Orchard.ContentManagement.MetaData;
using OrchardHUN.Shoutbox.Models;

namespace OrchardHUN.Shoutbox
{
    public class Migrations : DataMigrationImpl
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

            SchemaBuilder.CreateTable(typeof(ShoutboxPartRecord).Name,
                table => table
                    .ContentPartRecord()
                    .Column<int>("MaxMessageCount")
            );

            ContentDefinitionManager.AlterTypeDefinition("ShoutboxWidget",
                cfg => cfg
                    .WithPart("WidgetPart")
                    .WithPart("CommonPart")
                    .WithPart(typeof(ShoutboxPart).Name)
                    .WithSetting("Stereotype", "Widget")
            );


            return 1;
        }
    }
}
