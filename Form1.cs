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

namespace midi2obs
{
    public partial class Form1 : Form
    {
        IInputDevice inputDevice;
        public Form1()
        {
            InitializeComponent();

            inputDevice = InputDevice.GetByIndex(0);
            inputDevice.EventReceived += OnEventReceived;
            inputDevice.StartEventsListening();

            /*
            foreach(InputDevice inputDevice in InputDevice.GetAll())
            {
                inputDevice.EventReceived += OnEventReceived;
                Console.WriteLine(inputDevice.Name);
            }
            */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // ...

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            MidiDevice midiDevice = (MidiDevice)sender;

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

            Console.WriteLine($"Event: {note}, {velocity}");
        }

        private void OnEventSent(object sender, MidiEventSentEventArgs e)
        {
            MidiDevice midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event sent to '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }
    }
}
