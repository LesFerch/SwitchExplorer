using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SwitchExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int buildNumber = int.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", "").ToString());
            int UBR = int.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "UBR", "").ToString());
            if ((buildNumber < 21996) || (UBR < 3007))
            {
                MessageBox.Show("Windows 11 build revision 3007 or higher required", typeof(Program).Namespace);
                return;
            }

            string E10A = @"HKEY_CURRENT_USER\Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}";
            string E10B = @"HKEY_CURRENT_USER\Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}";

            if ((Registry.GetValue(E10A, null, null) == null) && (Registry.GetValue(E10B, null, null) == null))
            {
                Registry.SetValue(E10A, "", "CLSID_ItemsViewAdapter", RegistryValueKind.String);
                Registry.SetValue($"{E10A}\\InProcServer32", "", "C:\\Windows\\System32\\Windows.UI.FileExplorer.dll_", RegistryValueKind.String);
                Registry.SetValue($"{E10A}\\InProcServer32", "ThreadingModel", "Apartment", RegistryValueKind.String);
                Registry.SetValue(E10B, "", "File Explorer Xaml Island View Adapter", RegistryValueKind.String);
                Registry.SetValue($"{E10B}\\InProcServer32", "", "C:\\Windows\\System32\\Windows.UI.FileExplorer.dll_", RegistryValueKind.String);
                Registry.SetValue($"{E10B}\\InProcServer32", "ThreadingModel", "Apartment", RegistryValueKind.String);
            }
            else
            {
                try
                {
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\CLSID\{2aa9162e-c906-4dd9-ad0b-3d24a8eef5a0}", false);
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\CLSID\{6480100b-5a83-4d1e-9f69-8ae5a88e9a33}", false);
                }
                catch 
                {
                }
            }

            var processes = Process.GetProcessesByName("explorer");
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }

            Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);

        }

    }
}
