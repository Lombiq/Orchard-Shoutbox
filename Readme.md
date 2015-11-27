# Orchard Shoutbox Readme



## Project Description

A lightweight shoutbox module for the Orchard CMS


## Features

- Shoutbox (message box) widget with AJAX posting, configurable number of messages to show, optional projection link
- Projector filter so a projection for a specific shoutbox can be simply set up
- Ability to have an arbitrary number of different Shoutbox widgets on the site


## Documentation

### Installation and usage

#### Basics

After enabling the module you'll see a new widget type, Shoutbox Widget. Add this widget to the zone where you want the Shoutbox to be shown.  
Initially all authenticated users will be allowed to write messages, edit the WriteMessage permission if you want to alter this.

#### Listing older messages (and moderating) with Projector

If you want to moderate (edit and delete) messages or you want to display messages older than the last n you specified to be displayed in the widget, you should set up a projection. Just configure a projection with a query on the Shoutbox Message content. Shoutbox includes a Projector filter provider you can use to filter the messages for a specific Shoutbox Widget.  
If you want an "All messages" link to be displayed below a Shoutbox Widget, configure the widget's projection id property to be the numerical id of the projection.


You can install the module from the [Gallery](https://gallery.orchardproject.net/List/Modules/Orchard.Module.OrchardHUN.Shoutbox).

The module's source is available in two public source repositories, automatically mirrored in both directions with [Git-hg Mirror](https://githgmirror.com):

- [https://bitbucket.org/Lombiq/orchard-shoutbox](https://bitbucket.org/Lombiq/orchard-shoutbox) (Mercurial repository)
- [https://github.com/Lombiq/Orchard-Shoutbox](https://github.com/Lombiq/Orchard-Shoutbox) (Git repository)

Bug reports, feature requests and comments are warmly welcome, **please do so via GitHub**.
Feel free to send pull requests too, no matter which source repository you choose for this purpose.

This project is developed by [Lombiq Technologies Ltd](http://lombiq.com/). Commercial-grade support is available through Lombiq.