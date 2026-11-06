using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_PERSONAS
{
    partial class FormReports
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
            label1 = new Label();
            lblReporte = new Label();
            btnGenerate = new Button();
            cmbReportType = new ComboBox();
            dgvReports = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(488, 39);
            label1.Name = "label1";
            label1.Size = new Size(374, 37);
            label1.TabIndex = 0;
            label1.Text = "Generador de reportes";
            // 
            // lblReporte
            // 
            lblReporte.AutoSize = true;
            lblReporte.Font = new Font("JetBrains Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReporte.Location = new Point(24, 152);
            lblReporte.Name = "lblReporte";
            lblReporte.Size = new Size(276, 26);
            lblReporte.TabIndex = 1;
            lblReporte.Text = "Seleccione el Reporte:";
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.FromArgb(39, 39, 39);
            btnGenerate.Font = new Font("JetBrains Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGenerate.ForeColor = Color.White;
            btnGenerate.Location = new Point(1070, 138);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(229, 54);
            btnGenerate.TabIndex = 2;
            btnGenerate.Text = "Generar Reporte";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // cmbReportType
            // 
            cmbReportType.FormattingEnabled = true;
            cmbReportType.ItemHeight = 19;
            cmbReportType.Items.AddRange(new object[] { "Cuadre del Día", "Ventas por Cajero", "Inventario en Stock", "Facturas Crédito/Efectivo", "Top 5 Clientes" });
            cmbReportType.Location = new Point(306, 154);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(196, 27);
            cmbReportType.TabIndex = 3;
            // 
            // dgvReports
            // 
            dgvReports.BackgroundColor = Color.FromArgb(158, 173, 200);
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Location = new Point(24, 203);
            dgvReports.Name = "dgvReports";
            dgvReports.RowHeadersWidth = 51;
            dgvReports.Size = new Size(1275, 195);
            dgvReports.TabIndex = 4;
            dgvReports.CellContentClick += dgvReports_CellContentClick;
            // 
            // FormReports
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1323, 428);
            Controls.Add(dgvReports);
            Controls.Add(cmbReportType);
            Controls.Add(btnGenerate);
            Controls.Add(lblReporte);
            Controls.Add(label1);
            Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormReports";
            Text = "Reportes Cajeros";
            Load += FormReports_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblReporte;
        private Button btnGenerate;
        private ComboBox cmbReportType;
        private DataGridView dgvReports;
    }
}