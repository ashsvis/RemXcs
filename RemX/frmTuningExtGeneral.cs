using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using BaseServer;

namespace RemXcs
{
    public partial class frmTuning : Form
    {
        private void SetShellMode(bool shellmode)
        {
            RegistryKey regkey;
            if (shellmode)
            {
                using (regkey = Registry.CurrentUser.CreateSubKey(
                    "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
                {
                    regkey.SetValue("Shell", Application.ExecutablePath);
                }
                using (regkey = Registry.CurrentUser.CreateSubKey(
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
                {
                    regkey.SetValue("DisableChangePassword", 1);
                    regkey.SetValue("DisableLockWorkstation", 1);
                    regkey.SetValue("DisableTaskMgr", 1);
                }
            }
            else
            {
                using (regkey = Registry.CurrentUser.CreateSubKey(
                    "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
                {
                    regkey.SetValue("Shell", String.Empty);
                }
                using (regkey = Registry.CurrentUser.CreateSubKey(
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"))
                {
                    regkey.SetValue("DisableChangePassword", 0);
                    regkey.SetValue("DisableLockWorkstation", 0);
                    regkey.SetValue("DisableTaskMgr", 0);
                }
            }
        }

        private void LoadGeneralSettings()
        {
            Properties.Settings settings = Properties.Settings.Default;
            tbObjectName.Text = settings.ObjectName;
            cbStationNumber.SelectedIndex = settings.Station - 1;
            tbStationName.Text = settings.StationName;
            cbScreenSize.SelectedIndex = settings.ScreenSize;
            cbScreensCount.SelectedIndex = settings.ScreensCount;
            cbSoundMode.SelectedIndex = settings.SoundMode;
            cbSystemShell.Checked = settings.SystemShell;
            nudDisplayTimeout.Value = settings.DisplayTimeout;
            //----------------------------------
            cbRootScheme.Items.Clear();
            int i = 0;
            foreach (KeyValuePair<string, string> item in Data.GetSchemesList())
            {
                cbRootScheme.Items.Add(item.Key);
                if (item.Key.Equals(settings.RootScheme))
                    cbRootScheme.SelectedIndex = i;
                i++;
            }
            //----------------------------------
            cbWindowMode.Checked = settings.WindowMode;
            //----------------------------------
            lvFetchServers.Items.Clear();
            string loadedservers = settings.LoadedFetchServers;
            if (loadedservers.Length > 0)
            {
                string[] items = loadedservers.Split(new char[] { ';' });
                foreach (string item in items)
                {
                    string filename = Application.StartupPath + "\\" + item + ".exe";
                    if (File.Exists(filename))
                    {
                        FileVersionInfo myFileVersionInfo =
                            FileVersionInfo.GetVersionInfo(filename);
                        lvFetchServers.Items.Add(item).SubItems.
                            Add(myFileVersionInfo.Comments);
                    }
                    else
                        lvFetchServers.Items.Add(item).SubItems.Add("[не найден]");
                }
            }
            //----------------------------------
            lvRemoteCameras.Items.Clear();
            string remotecameras = settings.RemoteCameras;
            if (remotecameras.Length > 0)
            {
                string[] items = remotecameras.Split(new char[] { ';' });
                int n = 0;
                foreach (string item in items)
                {
                    if ((n & 0x01) > 0)
                        lvRemoteCameras.Items[lvRemoteCameras.Items.Count - 1].
                            SubItems.Add(item);
                    else
                        lvRemoteCameras.Items.Add(item);
                    n++;
                }
            }
            //----------------------------------
            cbTrends.SelectedIndex = settings.RemoveTrends;
            cbMinutes.SelectedIndex = settings.RemoveMinutes;
            cbHours.SelectedIndex = settings.RemoveHours;
            cbDays.SelectedIndex = settings.RemoveDays;
            cbMonths.SelectedIndex = settings.RemoveMonths;
            cbLogs.SelectedIndex = settings.RemoveLogs;
            cbReports.SelectedIndex = settings.RemoveReports;
            udGroups.Value = settings.GroupsCount;
            udTableGroups.Value = settings.TableGroupsCount;
        }

        private void SaveGeneralSettings()
        {
            Properties.Settings settings = Properties.Settings.Default;
            bool ShellModeChanged = settings.SystemShell != cbSystemShell.Checked;
            settings.ObjectName = tbObjectName.Text;
            settings.Station = cbStationNumber.SelectedIndex + 1;
            settings.StationName = tbStationName.Text;
            settings.ScreenSize = cbScreenSize.SelectedIndex;
            settings.ScreensCount = cbScreensCount.SelectedIndex;
            settings.SoundMode = cbSoundMode.SelectedIndex;
            settings.SystemShell = cbSystemShell.Checked;
            settings.DisplayTimeout = (int)nudDisplayTimeout.Value;
            if (cbRootScheme.SelectedIndex >= 0)
                settings.RootScheme = cbRootScheme.Items[cbRootScheme.SelectedIndex].ToString(); 
            settings.WindowMode = cbWindowMode.Checked;
            //----------------------------------------
            List<string> items = new List<string>();
            foreach (ListViewItem item in lvFetchServers.Items) items.Add(item.Text);
            settings.LoadedFetchServers = String.Join(";", items.ToArray());
            //----------------------------------------
            items = new List<string>();
            foreach (ListViewItem item in lvRemoteCameras.Items)
            {
                items.Add(item.Text);
                items.Add(item.SubItems[1].Text);
            }
            settings.RemoteCameras = String.Join(";", items.ToArray());
            //----------------------------------------
            settings.RemoveTrends = cbMinutes.SelectedIndex;
            settings.RemoveHours = cbHours.SelectedIndex;
            settings.RemoveDays = cbDays.SelectedIndex;
            settings.RemoveMonths = cbMonths.SelectedIndex;
            settings.RemoveLogs = cbLogs.SelectedIndex;
            settings.RemoveReports = cbReports.SelectedIndex;
            settings.GroupsCount = (int)udGroups.Value;
            settings.TableGroupsCount = (int)udTableGroups.Value;
            settings.Save();
            //----------------------------------------
            if (ShellModeChanged)
            {
                frmMain.MustWinLogOff = !settings.SystemShell;
                SetShellMode(settings.SystemShell);
            }
        }
    }
}
