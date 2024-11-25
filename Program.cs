using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;

namespace SwitchExplorer
{
    internal class Program
    {
        static string myName = typeof(Program).Namespace;
        static string myPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        static string myExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string myIcon = $@"{myPath}\{myName}.ico";
        static float ScaleFactor = GetScale();
        static bool Dark = isDark();
        static bool Win10Explorer;
        static bool Win10ContextMenu;

        static string E10A = @"Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}";
        static string E10B = @"Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}";
        static string CCMA = @"Software\Classes\CLSID\{86CA1AA0-34AA-4E8B-A509-50C905BAE2A2}";
        static string CCMB = $@"{CCMA}\InprocServer32";
        static string ADVK = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        static string TBAR = @"Software\Microsoft\Internet Explorer\Toolbar";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string NTkey = @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion";
            int buildNumber = int.Parse(Registry.GetValue(NTkey, "CurrentBuild", "").ToString());
            int UBR = int.Parse(Registry.GetValue(NTkey, "UBR", "").ToString());

            if (buildNumber < 21996)
            {
                MessageBox.Show("Windows 11 required", myName);
                return;
            }

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(E10A))
            {
                Win10Explorer = key != null;
            }

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(CCMB))
            {
                Win10ContextMenu = key != null;
            }

            var optionsDialog = new OptionsDialog();
            optionsDialog.ShowDialog();

        }

        // Get current screen scaling factor
        static float GetScale()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = graphics.DpiX;
                return dpiX / 96;
            }
        }

        // Determine if dark colors (theme) are being used
        public static bool isDark()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    object value = key.GetValue(valueName);
                    if (value is int intValue)
                    {
                        return intValue == 0;
                    }
                }
            }
            return false; // Return false if the key or value is missing
        }

        // Make dialog title bar black
        public enum DWMWINDOWATTRIBUTE : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref int pvAttribute, uint cbAttribute);

        static void DarkTitleBar(IntPtr hWnd)
        {
            var preference = Convert.ToInt32(true);
            DwmSetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref preference, sizeof(uint));

        }


        // Dialog for selecting Options
        public class OptionsDialog : Form
        {
            private RadioButton Win10ExplorerRadioButton;
            private RadioButton Win11ExplorerRadioButton;
            private RadioButton Win10ContextRadioButton;
            private RadioButton Win11ContextRadioButton;
            private Button buttonOK;

            public OptionsDialog()
            {
                InitializeComponents();
                Win10ExplorerRadioButton.Checked = Win10Explorer;
                Win10ContextRadioButton.Checked = Win10ContextMenu;
                Win11ExplorerRadioButton.Checked = !Win10Explorer;
                Win11ContextRadioButton.Checked = !Win10ContextMenu;
            }

            private void InitializeComponents()
            {
                Icon = Icon.ExtractAssociatedIcon(myExe);
                Text = myName;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                StartPosition = FormStartPosition.Manual;
                Width = (int)(300 * ScaleFactor);
                Height = (int)(270 * ScaleFactor);
                Font = new Font("Segoe UI", 10);

                //Explorer Group

                var ExplorerGroupBox = new GroupBox
                {
                    Name = "ExplorerGroupBox",
                    Text = "Select Explorer Type",
                    Location = new Point((int)(10 * ScaleFactor), (int)(10 * ScaleFactor)),
                    Width = Width - (int)(40 * ScaleFactor),
                    Height = (int)(80 * ScaleFactor)
                };

                if (Dark) { ExplorerGroupBox.ForeColor = Color.White; }

                Win10ExplorerRadioButton = new RadioButton
                {
                    Text = "Windows 10",
                    Location = new Point((int)(10 * ScaleFactor), (int)(20 * ScaleFactor)),
                    AutoSize = true
                };

                Win11ExplorerRadioButton = new RadioButton
                {
                    Text = "Windows 11",
                    Location = new Point((int)(10 * ScaleFactor), (int)(40 * ScaleFactor)),
                    AutoSize = true
                };

                ExplorerGroupBox.Controls.Add(Win10ExplorerRadioButton);
                ExplorerGroupBox.Controls.Add(Win11ExplorerRadioButton);

                //Context Group

                var contextMenuGroupBox = new GroupBox
                {
                    Name = "contextMenuGroupBox",
                    Text = "Select Context Menu Type",
                    Left = (int)(10 * ScaleFactor),
                    Top = (int)(100 * ScaleFactor),
                    Width = Width - (int)(40 * ScaleFactor),
                    Height = (int)(80 * ScaleFactor)
                };

                if (Dark) { contextMenuGroupBox.ForeColor = Color.White; }

                Win10ContextRadioButton = new RadioButton
                {
                    Text = "Windows 10",
                    Left = (int)(10 * ScaleFactor),
                    Top = (int)(20 * ScaleFactor),
                    AutoSize = true
                };

                Win11ContextRadioButton = new RadioButton
                {
                    Text = "Windows 11",
                    Left = (int)(10 * ScaleFactor),
                    Top = (int)(40 * ScaleFactor),
                    AutoSize = true
                };

                contextMenuGroupBox.Controls.Add(Win10ContextRadioButton);
                contextMenuGroupBox.Controls.Add(Win11ContextRadioButton);

                if (Dark)
                {
                    DarkTitleBar(Handle);
                    BackColor = Color.FromArgb(32, 32, 32);
                    ForeColor = Color.White;
                }

                Controls.Add(ExplorerGroupBox);
                Controls.Add(contextMenuGroupBox);

                buttonOK = new Button();
                buttonOK.Text = "OK";
                buttonOK.Left = (Width - (int)(80 * ScaleFactor)) / 2;
                buttonOK.Top = Height - (int)(74 * ScaleFactor);
                buttonOK.Width = (int)(75 * ScaleFactor);
                buttonOK.Height = (int)(26 * ScaleFactor);
                buttonOK.Click += new EventHandler(buttonOK_Click);
                buttonOK.Font = new Font("Segoe UI", 9);
                if (Dark)
                {
                    buttonOK.FlatStyle = FlatStyle.Flat;
                    buttonOK.FlatAppearance.BorderColor = SystemColors.Highlight;
                    buttonOK.FlatAppearance.BorderSize = 1;
                }
                Controls.Add(buttonOK);

                Point cursorPosition = Cursor.Position;
                int dialogX = Cursor.Position.X - Width / 2;
                int dialogY = Cursor.Position.Y - Height / 2;
                Screen screen = Screen.FromPoint(cursorPosition);
                int screenWidth = screen.WorkingArea.Width;
                int screenHeight = screen.WorkingArea.Height;
                int baseX = screen.Bounds.X;
                int baseY = screen.Bounds.Y;
                dialogX = Math.Max(baseX, Math.Min(baseX + screenWidth - Width, dialogX));
                dialogY = Math.Max(baseY, Math.Min(baseY + screenHeight - Height, dialogY));
                Location = new Point(dialogX, dialogY);
            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                SaveSettings();
                Close();
            }

            private void SaveSettings()
            {
                bool ExplorerRestartNeeded = false;
                string H = @"HKEY_CURRENT_USER\";

                if (Win10ContextRadioButton.Checked && !Win10ContextMenu)
                {
                    try
                    {
                        Registry.SetValue($@"{H}{CCMB}", "", "", RegistryValueKind.String);
                    }
                    catch
                    {
                    }
                    ExplorerRestartNeeded = true;
                }

                if (Win11ContextRadioButton.Checked && Win10ContextMenu)
                {
                    try
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(CCMA, false);
                    }
                    catch
                    {
                    }
                    ExplorerRestartNeeded = true;
                }

                if (Win10ExplorerRadioButton.Checked && !Win10Explorer)
                {
                    Registry.SetValue($@"{H}{E10A}", "", "CLSID_ItemsViewAdapter", RegistryValueKind.String);
                    Registry.SetValue($@"{H}{E10A}\InProcServer32", "", @"C:\Windows\System32\Windows.UI.FileExplorer.dll_", RegistryValueKind.String);
                    Registry.SetValue($@"{H}{E10A}\InProcServer32", "ThreadingModel", "Apartment", RegistryValueKind.String);
                    Registry.SetValue($@"{H}{E10B}", "", "File Explorer Xaml Island View Adapter", RegistryValueKind.String);
                    Registry.SetValue($@"{H}{E10B}\InProcServer32", "", @"C:\Windows\System32\Windows.UI.FileExplorer.dll_", RegistryValueKind.String);
                    Registry.SetValue($@"{H}{E10B}\InProcServer32", "ThreadingModel", "Apartment", RegistryValueKind.String);

                    byte[] binaryData = { 0x13, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x10, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x07, 0x00, 0x00, 0x5e, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    string SHBR = $@"{H}Software\Microsoft\Internet Explorer\Toolbar\ShellBrowser";
                    Registry.SetValue(SHBR, "ITBar7Layout", binaryData, RegistryValueKind.Binary);

                    ExplorerRestartNeeded = true;
                }

                if (Win11ExplorerRadioButton.Checked && Win10Explorer)
                {
                    try
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(E10A, false);
                        Registry.CurrentUser.DeleteSubKeyTree(E10B, false);
                    }
                    catch
                    {
                    }
                    ExplorerRestartNeeded = true;
                }

                Registry.SetValue($@"{H}{ADVK}", "AlwaysShowMenus", 0, RegistryValueKind.DWord);
                Registry.SetValue($@"{H}{TBAR}", "Locked", 1, RegistryValueKind.DWord);

                if (ExplorerRestartNeeded) RestartExplorer();
            }

            static void RestartExplorer()
            {
                var processes = Process.GetProcessesByName("explorer");
                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                    catch { }
                }
                Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
            }
        }

    }
}
