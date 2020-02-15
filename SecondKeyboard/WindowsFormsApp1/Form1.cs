using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            Task.Factory.StartNew(() => {
                while (true)
                {
                    sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
                    Thread.Sleep(2000);
                }
            });
        }
    }
}
