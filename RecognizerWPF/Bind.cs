using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace BinderWPF
{
    [Serializable]
    public class Bind
    {
     
        public Keys InputKey = new Keys();

        public List<Keys> OutputKeys = new List<Keys>();

        public int Delay;

        private Bind()
        {

        }

        public Bind(Keys input, List<Keys> output, int delay)
        {
            InputKey = input;

            OutputKeys.AddRange(output);

            Delay = delay;
        }


    }
}
