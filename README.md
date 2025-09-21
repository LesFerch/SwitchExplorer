# SwitchExplorer

[![image](https://github.com/LesFerch/WinSetView/assets/79026235/0188480f-ca53-45d5-b9ff-daafff32869e)Download the zip file](https://github.com/LesFerch/SwitchExplorer/releases/download/2.1.0/SwitchExplorer.zip)

## How to Download and Run

1. Download the zip file using the link above.
2. Extract **SwitchExplorer.exe**.
3. Right-click **SwitchExplorer.exe**, select Properties, check Unblock, and click OK.
4. Optionally move **SwitchExplorer.exe** to the folder of your choice.
5. Double-click **SwitchExplorer.exe** to set your Explorer and context menu preferences.
6. If you skipped step 3, then, in the SmartScreen window, click More info and then Run anyway.

**Note**: Some antivirus software may falsely detect the download as a virus. This can happen any time you download a new executable and may require extra steps to whitelist the file.

## Switch the Windows 11 default Explorer and Context Menu

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/b2a47468-4b91-48fd-94d3-cfdc2c659e0b)

With this tool you can:

- Set the default Explorer in Windows 11 to the Windows 10 version (the same one you can access via the Control Panel).
- Set the Windows 11 context menu (right-click menu) to the Windows 10 (classic) layout.

Of course, you can also use the tool to undo the changes and switch back to the Windows 11 defaults.

SwitchExplorer does not install any software. It only sets some user profile registry entries and closes Explorer windows (so the change can take effect). It can be used by both Standard and Administrator users and can be run from a flash drive. There is nothing to install. Just download and run.

Usage:
  
GUI: Double-click the app to use the interface to select your desired Explorer and Context menu options.

Command line: SwitchExplorer [/e10] [/e11] [/c10] [/c11] [/x]

`/e10`  Select the Windows 10 Explorer\
`/e11`  Select the Windows 11 Explorer\
`/c10`  Select the Windows 10 context menu\
`/c11`  Select the Windows 11 context menu\
`/x`    Do not close Explorer windows

**Note**: The `/x` option is typcially used when running this tool as part of a configuration script where you will be sigining out/in or restarting anyhow.

**Note:** Including both `/e10` and `/e11` or `/c10` and `/c11` on the command line will toggle between the Windows 10 and Windows 11 versions.

Technical details:

The Explorer and context menu changes are accomplished by applying or removing the registry entries shown [here](https://www.elevenforum.com/t/restore-classic-file-explorer-with-ribbon-in-windows-11.620/#Three) and [here](https://www.elevenforum.com/t/disable-show-more-options-context-menu-in-windows-11.1589/#One). You do not need to look at those registry entries. The links are provided as an FYI only.

**Windows 10 Explorer on Windows 11:**

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/9f768e17-a9d3-494e-9c9c-cc0161394c7e)

**Note**: SwitchExplorer.exe requires Windows 11 22H2 build 3007 or higher.

**Note**: When switched to the Windows 10 Explorer, the Details pane will still be the new Windows 11 version. If you want to get the old Details pane, that allows direct editing of metadata, you can do that by using the [OldExplorer](https://lesferch.github.io/OldExplorer) tool or by enabling the Windows 7 style details pane using **OldNewExplorer** (see below).


## Install OldNewExplorer for Additional Options

[OldNewExplorer](https://www.oldnewexplorer.com/) is a free tool from the author of [StartAllBack](https://www.startallback.com/) that provides some Windows 7 features on newer Windows versions. One of those features is the bottom-of-window details pane shown below. This allows for direct editing of file properties.

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/5e7e3053-8dd1-4af5-9c2d-efe9b8e5c61b)

After installing **OldNewExplorer** and enabling **Show details pane on the bottom**:

![image](https://github.com/LesFerch/SwitchExplorer/assets/79026235/7cbaa13c-7163-443c-8990-c87f7efa901d)

## Alternatives

[StartAllBack](https://www.startallback.com/) and [ExplorerPatcher](https://github.com/valinet/ExplorerPatcher) are two alternatives that provide many additional features, but require an install which adds processes to Windows. SwitchExplorer, by itself, adds no additional processes. Adding OldNewExplorer will add a process, but it's a very tiny footprint.

## Credits

Thanks to YouTube user CRUCC for discovering the registry values for switching Explorer: https://www.youtube.com/watch?v=fLKqFMdmo8k
\
\
Thanks to user Garlin at elevenforum.com for streamlining the registry entries to switch Explorer.
\
\
[![image](https://github.com/LesFerch/WinSetView/assets/79026235/63b7acbc-36ef-4578-b96a-d0b7ea0cba3a)](https://github.com/LesFerch/SwitchExplorer)






