using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UOG_BUS_MANAGEMENT_AND_SCHEDULING_SYSTEM
{
    public partial class FlashScreen : Form
    {
     
        int count = 0;
        public FlashScreen()
        {
            InitializeComponent();
            
            
        }

        private void FlashScreen_Load(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            count++;
            panelslide.Width += 1;
            if(panelslide.Width > 281)
            {
                panelslide.Width = 0;

            }
            if(count == 1000)
            {
                timer1.Stop();
                LoginDriver lo = new LoginDriver();
                lo.Show();
                this.Hide();
                timer1.Stop();
            }
        }
    }
}
