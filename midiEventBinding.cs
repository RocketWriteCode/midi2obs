using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi2obs
{
    class midiEventBinding
    {
        public string name;
        public int midiNote;
        public string effect;
        public string parameters;

        public midiEventBinding(string inName, int inNote, string inEffect, string inParameters)
        {
            name = inName;
            midiNote = inNote;
            effect = inEffect;
            parameters = inParameters;
        }
    }
}
