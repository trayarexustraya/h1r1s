using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Human_Resource_Information_System
{
    public partial class page_size : Form
    {
        private rpt_payroll_summary rpt_payroll;
        private String type;
        public page_size()
        {
            InitializeComponent();
        }
        public page_size(rpt_payroll_summary form)
        {
            rpt_payroll = form;
        }

        private void page_size_Load(object sender, EventArgs e)
        {
            cbo_paper_type.DroppedDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cbo_paper_type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a paper size");
                return;
            }
            else
            {
                type = cbo_paper_type.SelectedItem.ToString();
                rpt_payroll.papersize = type;
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
