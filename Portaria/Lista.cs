using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Portaria
{
    public partial class Lista : Form
    {
        public Lista()
        {
            InitializeComponent();
        }
        public Lista(string usuario)
        {
            InitializeComponent();
            ReportParameterCollection jef = new ReportParameterCollection();
            jef.Add(new ReportParameter("ReportParameter1", dateTimePicker3.Text));
            reportViewer1.LocalReport.SetParameters(jef);
            this.reportViewer1.Refresh();
        }
        private void Lista_Load(object sender, EventArgs e)
        {
            ReportParameterCollection jef = new ReportParameterCollection();
            jef.Add(new ReportParameter("ReportParameter1", dateTimePicker3.Text));
            reportViewer1.LocalReport.SetParameters(jef);
            // TODO: esta linha de código carrega dados na tabela 'BDCADASTRODataSet.PRESENCA'. Você pode movê-la ou removê-la conforme necessário.
            this.PRESENCATableAdapter.Fill(this.BDCADASTRODataSet.PRESENCA, dateTimePicker3.Text);
            this.reportViewer1.RefreshReport();
            this.reportViewer1.Refresh();
            dateTimePicker3.Focus();
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            ReportParameterCollection jef = new ReportParameterCollection();
            jef.Add(new ReportParameter("ReportParameter1", dateTimePicker3.Text));
            reportViewer1.LocalReport.SetParameters(jef);
            this.PRESENCATableAdapter.Fill(this.BDCADASTRODataSet.PRESENCA, dateTimePicker3.Text);
            this.reportViewer1.RefreshReport();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void dateTimePicker3_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dateTimePicker3_Enter(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void pRESENCABindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
