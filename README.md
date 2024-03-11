# SwitchExplorer

[![image](https://user-images.githubusercontent.com/79026235/152910441-59ba653c-5607-4f59-90c0-bc2851bf2688.png)Download the zip file](https://github.com/LesFerch/SwitchExplorer/releases/download/1.1.0/SwitchExplorer.zip)

## How to Download and Run

1. Download the zip file using the link above.
2. Extract **SwitchExplorer.exe**.
3. Right-click **SwitchExplorer.exe**, select Properties, check Unblock, and click OK.
4. Optionally move **SwitchExplorer.exe** to the folder of your choice.
5. Double-click **SwitchExplorer.exe** to toggle Explorer
6. If you skipped step 3, then, in the SmartScreen window, click More info and then Run anyway.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen any time you download a new executable and may require extra steps to whitelist the file.

## Switch the Windows 11 default Explorer

This program switches (i.e. toggles) the Windows 11 default file manager back and forth between the the current version and the Windows 10 Explorer. That is, if your current Explorer is the Windows 11 version, double-clicking this program will switch to the Windows 10 version (the same one you can get to via the Control Panel). Double-clicking again will switch back to the current Windows 11 Explorer.

The switch is accomplished by applying or removing the registry entries shown [here](https://www.elevenforum.com/t/restore-classic-file-explorer-with-ribbon-in-windows-11.620/#Three). You do not need to look at those registry entries. The link is provided as an FYI only. SwitchExplorer does not install any software. It only sets some user profile registry entries and restarts Explorer.

**Windows 10 Explorer on Windows 11:**

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/6a3d68c4-2af0-4115-a8c9-65f353abe523)

**Note**: SwitchExplorer.exe requires Windows 11 22H2 or 23H2 with build revision 3007 or higher.

**Note**: When switched to the Windows 10 Explorer, the Details pane will still be the new Windows 11 version. If you want to get the old Details pane, that allows direct editing of metadata, you will need to directly launch the Windows 10 Explorer. You can do that with the [OldExplorer](https://lesferch.github.io/OldExplorer) tool. Another option is to enable the Windows 7 style details pane using **OldNewExplorer**.


## Install OldNewExplorer for Additional Options

[OldNewExplorer](https://www.oldnewexplorer.com/) is a free tool from the author of [StartAllBack](https://www.startallback.com/) that provides some Windows 7 features on newer Windows versions. One of those features is the bottom-of-window details pane shown below. This allows for direct editing of file properties.

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/0476927f-c6c7-4360-9cd5-421a0415d8e2)

After installing **OldNewExplorer** and enabling **Show details pane on the bottom**:

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/ada55165-c5b2-46cf-873c-01e0fc717022)

## Alternatives

[StartAllBack](https://www.startallback.com/) and [ExplorerPatcher](https://github.com/valinet/ExplorerPatcher) are two alternatives that provide many additional features, but require an install which adds processes to Windows. SwitchExplorer, by itself, adds no additional processes. Adding OldNewExplorer will add a process, but it's a very tiny footprint.

## Credits

Thanks to YouTube user CRUCC for discovering the registry values that make this possible: https://www.youtube.com/watch?v=fLKqFMdmo8k
\
\
\
[![image](https://user-images.githubusercontent.com/79026235/153264696-8ec747dd-37ec-4fc1-89a1-3d6ea3259a95.png)](https://github.com/LesFerch/SwitchExplorer)
