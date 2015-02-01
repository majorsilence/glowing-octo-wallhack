using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFinderOrganizer
{

    internal delegate void FileOrangizerEventHandler(object sender, FileOrangizerArgs e);

    internal class FileOrangizerArgs : System.EventArgs
    {
        private string _msg;
        private int _value;
        public FileOrangizerArgs(string m)
        {
            _msg = m;
            _value = 0;
        }
        public FileOrangizerArgs(int v)
        {
            _msg = "";
            _value = v;
        }

        public FileOrangizerArgs(string msg, int v)
        {
            _msg = msg;
            _value = v;
        }
        /// <summary>
        /// Event Message.
        /// </summary>
        public string Message
        {
            get { return _msg; }
        }
        public int Value
        {
            get { return _value; }
        }
    }
}
