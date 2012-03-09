using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.UI.Resources;

namespace OrchardHUN.Shoutbox
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("Shoutbox").SetUrl("Shoutbox.js").SetDependencies("jQuery");
            manifest.DefineStyle("Shoutbox").SetUrl("orchardhun-shoutbox.css");
        }
    }
}
