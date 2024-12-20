using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;

namespace midi2obs
{
    class MidiHandler
    {
        Form1 parentForm;
        IInputDevice inputDevice;

        public MidiHandler(Form1 inParentForm)
        {
            parentForm = inParentForm;

            inputDevice = InputDevice.GetByIndex(0);
            inputDevice.EventReceived += OnMidiEventReceived;
            inputDevice.StartEventsListening();
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

            for (int i = 0; i < values.Length; i++)
            {
                cleanedValues[i] = values[i].Trim();
            }

            int note = int.Parse(cleanedValues[0]);
            int velocity = int.Parse(cleanedValues[1]);

            if (velocity == 0) return;

            parentForm.UpdateNoteFeedback(note.ToString());
        }
    }
}
