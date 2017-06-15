using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMRCodePlugger
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            debugClear();
        }

        void debugPrint(String inp)
        {
            Console.WriteLine(inp);
            this.tbDebug.Text += inp + "\n";
        }

        void debugClear()
        {
            this.tbDebug.Text = "";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Codeplug c = Codeplug.FromFile("uhf.rdt");

            foreach(Codeplug.Contact con in c.Contacts)
            {
                if (con.Type == Codeplug.Contact.ContactType.Group || con.Type == Codeplug.Contact.ContactType.Private)
                {
                    con.Id += 1000;
                    //debugPrint(con.Name + " : " + con.Id);
                    Console.WriteLine(" " + con.Name + " - " + con.Id.ToString());
                }
            }

            c.save("testOut4.rdt");

            Console.WriteLine("READING BACK IN DATA ---");

            c = Codeplug.FromFile("testOut4.rdt");

            foreach (Codeplug.Contact con in c.Contacts)
            {
                if (con.Type == Codeplug.Contact.ContactType.Group || con.Type == Codeplug.Contact.ContactType.Private)
                {
                    //debugPrint(con.Name + " : " + con.Id);
                    Console.WriteLine(" " + con.Name + " - " + con.Id.ToString());
                }
            }

            /*

            debugPrint("Downloading repeater data..");
            Repeaters.Repeater[] rptrs = Repeaters.Get_Repeaters();

            int i = 0;
            foreach(Repeaters.Repeater rptr in rptrs)
            {
                i++;
                if (i > 10) break;
                debugPrint(rptr.city + ", " + rptr.state);

            }*/
        }




    }
}
