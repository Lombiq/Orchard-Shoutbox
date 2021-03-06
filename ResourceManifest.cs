﻿using Orchard.UI.Resources;

namespace OrchardHUN.Shoutbox
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("Shoutbox").SetUrl("orchardhun-shoutbox.js").SetDependencies("jQuery");
            manifest.DefineStyle("Shoutbox").SetUrl("orchardhun-shoutbox.css");
        }
    }
}
