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

        string pass = "testpass";

        private delegate void SafeCallDelegateBool(bool value);
        private delegate void SafeCallDelegateText(string text);

        OBSHandler obsHandler;

        public Form1()
        {
            Console.WriteLine("Program started");
            InitializeComponent();

            obsHandler = new OBSHandler(this);

            inputDevice = InputDevice.GetByIndex(0);
            inputDevice.EventReceived += OnMidiEventReceived;
            inputDevice.StartEventsListening();
        }        

        public void UpdateConnectCheckbox(bool value)
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
            obsHandler.Connect(pass);
        }

        private void ConnectedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConnectCheckbox(obsHandler.obsSocket.IsConnected);
        }
    }
}
