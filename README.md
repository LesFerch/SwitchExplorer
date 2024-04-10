# SwitchExplorer

[![image](https://github.com/LesFerch/WinSetView/assets/79026235/0188480f-ca53-45d5-b9ff-daafff32869e)Download the zip file](https://github.com/LesFerch/SwitchExplorer/releases/download/2.0.0/SwitchExplorer.zip)

## How to Download and Run

1. Download the zip file using the link above.
2. Extract **SwitchExplorer.exe**.
3. Right-click **SwitchExplorer.exe**, select Properties, check Unblock, and click OK.
4. Optionally move **SwitchExplorer.exe** to the folder of your choice.
5. Double-click **SwitchExplorer.exe** to toggle Explorer
6. If you skipped step 3, then, in the SmartScreen window, click More info and then Run anyway.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen any time you download a new executable and may require extra steps to whitelist the file.

## Switch the Windows 11 default Explorer and Context Menu

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/ac13bdc1-a70c-489d-b3bb-b841944bbde8)

With this tool you can:

- Set the default Explorer in Windows 11 to the Windows 10 version (the same one you can access via the Control Panel).
- Set the Windows 11 context menu (right-click menu) to the Windows 10 (classic) layout.

Of course, you can also use the tool to undo the changes and switch back to the Windows 11 defaults.

SwitchExplorer does not install any software. It only sets some user profile registry entries and restarts Explorer. It can be used by both Standard and Administrator users and can be run from a flash drive. There is nothing to install. Just download and run.

Technical details:

The Explorer and context menu changes are accomplished by applying or removing the registry entries shown [here](https://www.elevenforum.com/t/restore-classic-file-explorer-with-ribbon-in-windows-11.620/#Three) and [here](https://www.elevenforum.com/t/disable-show-more-options-context-menu-in-windows-11.1589/#One). You do not need to look at those registry entries. The links are provided as an FYI only.

**Windows 10 Explorer on Windows 11:**

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/6a3d68c4-2af0-4115-a8c9-65f353abe523)

**Note**: SwitchExplorer.exe requires Windows 11 22H2 or higher with build revision 3007 or higher.

**Note**: When switched to the Windows 10 Explorer, the Details pane will still be the new Windows 11 version. If you want to get the old Details pane, that allows direct editing of metadata, you can do that by using the [OldExplorer](https://lesferch.github.io/OldExplorer) tool or by enabling the Windows 7 style details pane using **OldNewExplorer** (see below).


## Install OldNewExplorer for Additional Options

[OldNewExplorer](https://www.oldnewexplorer.com/) is a free tool from the author of [StartAllBack](https://www.startallback.com/) that provides some Windows 7 features on newer Windows versions. One of those features is the bottom-of-window details pane shown below. This allows for direct editing of file properties.

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/0476927f-c6c7-4360-9cd5-421a0415d8e2)

After installing **OldNewExplorer** and enabling **Show details pane on the bottom**:

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/a9ca7d07-380f-4479-a10a-245f0df47a0c)

## Alternatives

[StartAllBack](https://www.startallback.com/) and [ExplorerPatcher](https://github.com/valinet/ExplorerPatcher) are two alternatives that provide many additional features, but require an install which adds processes to Windows. SwitchExplorer, by itself, adds no additional processes. Adding OldNewExplorer will add a process, but it's a very tiny footprint.

## Credits

Thanks to YouTube user CRUCC for discovering the registry values that make this possible: https://www.youtube.com/watch?v=fLKqFMdmo8k
\
\
\
[![image](https://github.com/LesFerch/WinSetView/assets/79026235/63b7acbc-36ef-4578-b96a-d0b7ea0cba3a)](https://github.com/LesFerch/SwitchExplorer)
