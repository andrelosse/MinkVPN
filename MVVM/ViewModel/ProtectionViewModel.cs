// Code by: @andrelosse

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MinkVPN.Core;
using MinkVPN.MVVM.Model;

namespace MinkVPN.MVVM.ViewModel
{
    internal class ProtectionViewModel : ObservObj
    {

        static string address = "us1.vpnbook.com";
        static string folder = $"{Directory.GetCurrentDirectory()}/VPNServer";
        static string PBKPath = $"{folder}/VPNServer1.pbk";

        private string _connecStatus;
        private string _disconnecStatus;

        public ObservableCollection<ServerModel> VpnServers { get; set; }

        public string ConnecStatus {
            get { return _connecStatus; }
            set { _connecStatus = value;
                OnPropertyChanged(); 
            }
        }
        public string DisconnecStatus
        {
            get { return _disconnecStatus; }
            set{ _disconnecStatus = value;
                OnPropertyChanged();
            }
        }

        public RelayCommands ConnecCommand { get; set; }
        public RelayCommands DisconnectCommand { get; set; }

        public ProtectionViewModel()
        {
            ConnecStatus = "Connect";
            DisconnecStatus = "Disconnect";

            VpnServers = new ObservableCollection<ServerModel>();

                VpnServers.Add(new ServerModel
                {
                    CountryOp = "USA",
                    ServerOp = "us1.vpnbook.com",
                    Password = "n4862iu",
                    UserName = "vpnbook",
                    ID = "VPNBook",
                });

            ConnecCommand = new RelayCommands(o => {

                Task.Run(() =>
                {
                    ConnecStatus = "Connecting...";
                    ServerConnector();
                    var connecProcess = new Process();
                    connecProcess.StartInfo.FileName = "cmd.exe";
                    connecProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    connecProcess.StartInfo.ArgumentList.Add(@"/c rasdial ServerX vpnbook n4862iu /phonebook:./VPNServer/VPNServer1.pbk");
                    connecProcess.StartInfo.UseShellExecute = false;
                    connecProcess.StartInfo.CreateNoWindow = true;

                    connecProcess.Start();
                    connecProcess.WaitForExit();

                    switch (connecProcess.ExitCode)
                    {
                        case 0:
                            Debug.WriteLine("CONNECTED DEBUG");
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

        public void ServerConnector()
        {

            try {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(PBKPath))
                {
                    MessageBox.Show("Server connection already exists"); return;
                }

                var stringbuilds = new StringBuilder();
                stringbuilds.AppendLine("[ServerX]");
                stringbuilds.AppendLine("MEDIA=rastapi");
                stringbuilds.AppendLine("Port=VPN2-0");
                stringbuilds.AppendLine("Device=WAN Miniport (IKEv2)");
                stringbuilds.AppendLine("DEVICE=vpn");
                stringbuilds.AppendLine($"PhoneNumber={address}");

                File.WriteAllText(PBKPath, stringbuilds.ToString());
            } catch (Exception e){ MessageBox.Show(e.ToString()); return; }

            
        }
    }
}
