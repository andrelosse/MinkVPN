# MinkVPN

Github: @andrelosse

This project is still a work in progress ***

VPN project in order to study the implementation of a graphical user interface using XAML and WPF.
It's important to notice that there are still some issues with the overall layout of the application.

In the current stage of development, you can sucessfully connect to three PPTP VPN servers provided by:

--> https://www.vpnbook.com/

There are some features that will be added in future versions:

-> More servers to choose from;
-> A connection status page with more info about the servers;
-> A dark/light modes options in the Settings menu;
-> A redefined UI and it's migration from WPF to WinUI 3.

--------------------------------------------------------------------

08/24/2022 uptade:

--> Added three server options to connect to;
--> Fixed ListView and it's data binding in ProtectionView and ProtectionViewModel;
--> General improvements in the code;

09/18/2022 update:

--> Resize the window is now possible (The content is not yet responsive);
--> The exception thrown when the user tries to connect to a server without choosing one from the list now does not break the application;