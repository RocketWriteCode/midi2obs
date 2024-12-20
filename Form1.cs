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
        string pass = "testpass";

        private delegate void SafeCallDelegateBool(bool value);
        private delegate void SafeCallDelegateText(string text);

        OBSHandler obsHandler;
        MidiHandler midiHandler;

        public Form1()
        {
            Console.WriteLine("Program started");
            InitializeComponent();

            obsHandler = new OBSHandler(this);
            midiHandler = new MidiHandler(this);
            
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

        public void UpdateNoteFeedback(string text)
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
