using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{
    public partial class Notificação1 : Form
    {
        public Notificação1()
        {
            InitializeComponent();
        }
        public Notificação1(string PMemssagem, int pTipo)
        {
            InitializeComponent();
            lblMsd.Text = PMemssagem;
            if (pTipo == 1)
            {
                pMsg.BackColor = Color.FromArgb(12, 198, 6);
                pictureBox2.Visible = true;
            }
            else if (pTipo == 2)
            {
                pMsg.BackColor = Color.FromArgb(232, 0, 1);
                pictureBox1.Visible = true;
            }
           
        }
        private int conteo;
        private void timer1_Tick(object sender, EventArgs e)
        {
            conteo++;
            if (conteo == 40)
                this.Close();
        }

        private void Notificação1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void lblMsd_Click(object sender, EventArgs e)
        {

        }
    }
}
