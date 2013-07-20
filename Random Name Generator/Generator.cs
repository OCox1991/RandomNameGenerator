using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Random_Name_Generator
{
    class Generator
    {
        private Random r;
        private List<string> startparts;
        private List<string> endparts;
        public Generator(bool showDialog)
        {
            string filename = null;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "XML Files (*.xml)|*.xml";
            open.Title = "Select XML file to read name parts from:";
            if (open.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                filename = open.FileName;
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening file", "File Error",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            init(filename, showDialog);
        }

        public Generator(string filename, bool showDialog)
        {
            init(filename, showDialog);
        }

        private void init(string filename, bool showDialog)
        {
            r = new Random();
            if (filename != null)
            {
                Parser p = new Parser(filename);
                string name = "UNASSIGNED";
                startparts = p.getStartParts();
                endparts = p.getEndParts();
                if (showDialog)
                {
                    DialogResult d = DialogResult.Yes;
                    while (d == DialogResult.Yes)
                    {
                        name = getName(true);
                        d = MessageBox.Show("OPERATION: " + name.ToUpper() + "\r \r Generate new?", "Name Generated:", MessageBoxButtons.YesNo);
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public string getName(bool addSpace)
        {
            string name = getRandomEntry<string>(startparts);
            if (addSpace)
            {
                name += " ";
            }
            name += getRandomEntry<string>(endparts);
            return name;
        }

        internal T getRandomEntry<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            else
            {
                return list[r.Next(list.Count)];
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Generator g = new Generator(true);
        }
    }
}
