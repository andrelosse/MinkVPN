// Code by: @andrelosse

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using MinkVPN.Core;
using MinkVPN.MVVM.Model;
using System.Windows.Controls;
using MinkVPN.MVVM.View;

namespace MinkVPN.MVVM.ViewModel
{
    internal class ProtectionViewModel : ObservObj
    {

        static string folder = $"{Directory.GetCurrentDirectory()}/VPNServer";
        static string PBKPath = $"{folder}/VPNServer.pbk";

        private string _connecStatus;
        private string _disconnecStatus;

        public ObservableCollection<ServerModel> VpnServers { set; get; } = new ObservableCollection<ServerModel>();

        public string ConnecStatus {
            get { return _connecStatus; }
            set { _connecStatus = value;
                OnPropertyChanged(); 
            }
        }
        public string DisconnecStatus {
            get { return _disconnecStatus; }
            set{ _disconnecStatus = value;
                OnPropertyChanged();
            }
        }

        public RelayCommands ConnecCommand { get; }
        public RelayCommands DisconnectCommand { get; }
        public ServerModel ServerSellectedOBJ { get; set; }

        public ProtectionViewModel()
        {

            GetServers();

            ConnecStatus = "Connect";
            DisconnecStatus = "Disconnect";

            ConnecCommand = new RelayCommands(o => {

                ServerFile();

                Task.Run(() =>
                {

                    
                    ConnecStatus = "Connecting...";
                    var connecProcess = new Process();
                    connecProcess.StartInfo.FileName = "cmd.exe";
                    connecProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    if (ServerSellectedOBJ != null)
                    {
                        connecProcess.StartInfo.ArgumentList.Add($@"/c rasdial ServerInfo {ServerSellectedOBJ.UserName} {ServerSellectedOBJ.Password} /phonebook:./VPNServer/VPNServer.pbk");
                        connecProcess.StartInfo.UseShellExecute = false;
                        connecProcess.StartInfo.CreateNoWindow = true;

                        connecProcess.Start();
                        connecProcess.WaitForExit();
                    }

                    switch (connecProcess.ExitCode)
                    {
                        case 0:
                            ConnecStatus = "Connected";
                            break;
                        case 691:
                            MessageBox.Show("Wrong username/password");
                            break;
                        default:
                            MessageBox.Show($"Error -> {connecProcess.ExitCode}");
                            break;
                    }
                });
            });

            DisconnectCommand = new RelayCommands(o =>
            {
                Task.Run(() =>
                {
                    DisconnecStatus = "Disconnecting...";
                    var disconnecProcess = new Process();
                    disconnecProcess.StartInfo.FileName = "cmd.exe";
                    disconnecProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    disconnecProcess.StartInfo.ArgumentList.Add(@"/c rasdial /d");
                    disconnecProcess.StartInfo.UseShellExecute = false;
                    disconnecProcess.StartInfo.CreateNoWindow = true;

                    disconnecProcess.Start();
                    disconnecProcess.WaitForExit();

                    File.Delete(PBKPath);

                    DisconnecStatus = "Disconnect";
                });

                ConnecStatus = "Connect";
            });
        }

        public void ServerFile()
        {

            try {
                if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }

                if (ServerSellectedOBJ == null)
                {
                    MessageBox.Show("Conection failed"); return;
                }

                var stringbuilds = new StringBuilder();
                stringbuilds.AppendLine("[ServerInfo]");
                stringbuilds.AppendLine("MEDIA=rastapi");
                stringbuilds.AppendLine("Port=VPN2-0");
                stringbuilds.AppendLine("Device=WAN Miniport (IKEv2)");
                stringbuilds.AppendLine("DEVICE=vpn");
                stringbuilds.AppendLine(@$"PhoneNumber={ServerSellectedOBJ.Address}");

                File.WriteAllText(PBKPath, stringbuilds.ToString());

            } catch (Exception e){ MessageBox.Show(e.ToString()); return; }
            
        }

        public void GetServers()
        {
            ServerModel Server001 =
            new ServerModel(iD: "001", userName: "vpnbook", password: "w48bf5a",
                address: "us1.vpnbook.com", countryOp: "USA");

            ServerModel Server002 =
                new ServerModel(iD: "002", userName: "vpnbook", password: "w48bf5a",
                    address: "ca222.vpnbook.com", countryOp: "Canada");

            ServerModel Server003 =
                new ServerModel(iD: "003", userName: "vpnbook", password: "w48bf5a",
                    address: "fr8.vpnbook.com", countryOp: "France");

            VpnServers.Add(Server001);
            VpnServers.Add(Server002);
            VpnServers.Add(Server003);
        }
    }
}
