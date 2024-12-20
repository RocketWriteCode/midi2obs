using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

namespace midi2obs
{
    class OBSHandler
    {
        public OBSWebsocket obsSocket;
        Form1 parentForm;

        public OBSHandler(Form1 inParentForm)
        {
            parentForm = inParentForm;

            obsSocket = new OBSWebsocket();
            obsSocket.Connected += onConnect;
            obsSocket.Disconnected += onDisconnect;
        }

        private void onConnect(object sender, EventArgs e)
        {
            Console.WriteLine("Websocket connected");
            parentForm.UpdateConnectCheckbox(true);
        }

        private void onDisconnect(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            Console.WriteLine("Websocket disconnected");
            parentForm.UpdateConnectCheckbox(false);
        }

        public void Connect(string pass)
        {
            if (!obsSocket.IsConnected)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        obsSocket.ConnectAsync("ws://192.168.212.41:4455", pass);
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
            else
            {
                obsSocket.Disconnect();
            }
        }
    }
}
