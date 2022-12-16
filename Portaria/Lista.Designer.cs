namespace Portaria
{
    partial class Lista
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.presencaTableAdapter1 = new Portaria.BDCADASTRODataSetTableAdapters.PRESENCATableAdapter();
            this.bdcadastroDataSet1 = new Portaria.BDCADASTRODataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BDCADASTRODataSet = new Portaria.BDCADASTRODataSet();
            this.PRESENCABindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PRESENCATableAdapter = new Portaria.BDCADASTRODataSetTableAdapters.PRESENCATableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.bdcadastroDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDCADASTRODataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRESENCABindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox3.Location = new System.Drawing.Point(12, 29);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(508, 20);
            this.textBox3.TabIndex = 55;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(547, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(98, 20);
            this.dateTimePicker1.TabIndex = 56;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CustomFormat = "MMMM - yyyy";
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(673, 29);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(115, 20);
            this.dateTimePicker3.TabIndex = 65;
            this.dateTimePicker3.ValueChanged += new System.EventHandler(this.dateTimePicker3_ValueChanged);
            this.dateTimePicker3.EnabledChanged += new System.EventHandler(this.dateTimePicker3_EnabledChanged);
            this.dateTimePicker3.Enter += new System.EventHandler(this.dateTimePicker3_Enter);
            this.dateTimePicker3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker3_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(544, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(670, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Mês";
            // 
            // presencaTableAdapter1
            // 
            this.presencaTableAdapter1.ClearBeforeFill = true;
            // 
            // bdcadastroDataSet1
            // 
            this.bdcadastroDataSet1.DataSetName = "BDCADASTRODataSet";
            this.bdcadastroDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PRESENCABindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Portaria.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(15, 75);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(773, 518);
            this.reportViewer1.TabIndex = 70;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load_1);
            // 
            // BDCADASTRODataSet
            // 
            this.BDCADASTRODataSet.DataSetName = "BDCADASTRODataSet";
            this.BDCADASTRODataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PRESENCABindingSource
            // 
            this.PRESENCABindingSource.DataMember = "PRESENCA";
            this.PRESENCABindingSource.DataSource = this.BDCADASTRODataSet;
            // 
            // PRESENCATableAdapter
            // 
            this.PRESENCATableAdapter.ClearBeforeFill = true;
            // 
            // Lista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 658);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.dateTimePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Lista";
            this.Text = "Lista";
            this.Load += new System.EventHandler(this.Lista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdcadastroDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BDCADASTRODataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRESENCABindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private BDCADASTRODataSetTableAdapters.PRESENCATableAdapter presencaTableAdapter1;
        private BDCADASTRODataSet bdcadastroDataSet1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PRESENCABindingSource;
        private BDCADASTRODataSet BDCADASTRODataSet;
        private BDCADASTRODataSetTableAdapters.PRESENCATableAdapter PRESENCATableAdapter;
    }
}