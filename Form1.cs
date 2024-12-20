using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;

namespace midi2obs
{
    public partial class Form1 : Form
    {
        IInputDevice inputDevice;

        OBSWebsocket obsSocket;

        string pass = "testpass";

        private delegate void SafeCallDelegateBool(bool value);
        private delegate void SafeCallDelegateText(string text);

        public Form1()
        {
            Console.WriteLine("Program started");
            InitializeComponent();

            inputDevice = InputDevice.GetByIndex(0);
            inputDevice.EventReceived += OnMidiEventReceived;
            inputDevice.StartEventsListening();

            obsSocket = new OBSWebsocket();

            obsSocket.Connected += onConnect;
            obsSocket.Disconnected += onDisconnect;
        }

        private void onConnect(object sender, EventArgs e)
        {
            Console.WriteLine("Websocket connected");
            UpdateConnectCheckbox(true);
        }

        private void onDisconnect(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {
            Console.WriteLine("Websocket disconnected");
            UpdateConnectCheckbox(false);
        }

        private void UpdateConnectCheckbox(bool value)
        {
            if (ConnectedCheckbox.InvokeRequired)
            {
                var d = new SafeCallDelegateBool(UpdateConnectCheckbox);
                connectButton.Invoke(d, new object[] { value });
            }
            else
            {
                ConnectedCheckbox.Checked = value;
            }
        }

        private void UpdateNoteFeedback(string text)
        {
            if(NoteFeedback.InvokeRequired)
            {
                SafeCallDelegateText del = new SafeCallDelegateText(UpdateNoteFeedback);
                NoteFeedback.Invoke(del, new object[] { text });
            }
            else
            {
                NoteFeedback.Text = text;
            }
        }

        private void OnMidiEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            string eventContent = e.Event.ToString();

            if (eventContent.Contains("Timing")) return;
            if (eventContent.Contains("Aftertouch")) return;

            string[] splitString = eventContent.Split('(');
            string cleanedString = splitString[1].Remove(splitString[1].Length - 1);
            string[] values = cleanedString.Split(',');
            string[] cleanedValues = new string[values.Length];
            
            for(int i = 0; i < values.Length; i++)
            {
                cleanedValues[i] = values[i].Trim();
            }

            int note = int.Parse(cleanedValues[0]);
            int velocity = int.Parse(cleanedValues[1]);

            if (velocity == 0) return;

            UpdateNoteFeedback(note.ToString());
        }

        private void OnEventSent(object sender, MidiEventSentEventArgs e)
        {
            MidiDevice midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }

        private void connectButton_Click(object sender, EventArgs e)
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
                        BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        });
                    }
                });
            }
            else
            {
                obsSocket.Disconnect();
            }
        }

        private void ConnectedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConnectCheckbox(obsSocket.IsConnected);
        }
    }
}
