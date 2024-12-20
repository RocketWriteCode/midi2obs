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
        private delegate void SafeCallDelegateVoid();

        OBSHandler obsHandler;
        MidiHandler midiHandler;

        List<midiEventBinding> bindings = new List<midiEventBinding>();

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

        private void addBindingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindings.Add(new midiEventBinding("new binding", 0, "effect", "parameters"));
            UpdateBindingListView();
        }

        private void UpdateBindingListView()
        {
            if (BindingList.InvokeRequired)
            {
                SafeCallDelegateVoid del = new SafeCallDelegateVoid(UpdateBindingListView);
                BindingList.Invoke(del, new object[] {});
            }
            else
            {
                BindingList.Items.Clear();
                ListViewItem[] newContent = new ListViewItem[bindings.Count];
                for(int i = 0; i < bindings.Count; i++)
                {
                    newContent[i] = new ListViewItem(bindings[i].name);
                }
                BindingList.Items.AddRange(newContent);
            }
        }

        private void removeBindingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BindingList.SelectedItems.Count <= 0) return;
            foreach(ListViewItem item in BindingList.SelectedItems)
            {
                List<midiEventBinding> toDelete = new List<midiEventBinding>();
                foreach(midiEventBinding bind in bindings)
                {
                    if (bind.name == item.Text)
                    {
                        toDelete.Add(bind);
                    }
                }
                foreach(midiEventBinding bind in toDelete)
                {
                    bindings.Remove(bind);
                }
            }
            UpdateBindingListView();
        }
    }
}
