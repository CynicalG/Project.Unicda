using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_PERSONAS
{
    partial class FormSelectCashier
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblCajero = new Label();
            cmbCashiers = new ComboBox();
            btnStartShift = new Button();
            SuspendLayout();
            // 
            // lblCajero
            // 
            lblCajero.AutoSize = true;
            lblCajero.Font = new Font("JetBrains Mono", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCajero.Location = new Point(220, 96);
            lblCajero.Name = "lblCajero";
            lblCajero.Size = new Size(264, 26);
            lblCajero.TabIndex = 0;
            lblCajero.Text = "Seleccione un cajero:";
            // 
            // cmbCashiers
            // 
            cmbCashiers.BackColor = Color.FromArgb(252, 250, 249);
            cmbCashiers.FormattingEnabled = true;
            cmbCashiers.Location = new Point(220, 128);
            cmbCashiers.Name = "cmbCashiers";
            cmbCashiers.Size = new Size(254, 27);
            cmbCashiers.TabIndex = 1;
            // 
            // btnStartShift
            // 
            btnStartShift.BackColor = Color.FromArgb(39, 39, 39);
            btnStartShift.ForeColor = Color.White;
            btnStartShift.Location = new Point(220, 161);
            btnStartShift.Name = "btnStartShift";
            btnStartShift.Size = new Size(254, 33);
            btnStartShift.TabIndex = 2;
            btnStartShift.Text = "Iniciar turno";
            btnStartShift.UseVisualStyleBackColor = false;
            btnStartShift.Click += btnStartShift_Click;
            // 
            // FormSelectCashier
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(158, 173, 200);
            ClientSize = new Size(695, 313);
            Controls.Add(btnStartShift);
            Controls.Add(cmbCashiers);
            Controls.Add(lblCajero);
            Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(39, 39, 39);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormSelectCashier";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Seleccionar Cajero";
            Load += FormSelectCashier_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCajero;
        private ComboBox cmbCashiers;
        private Button btnStartShift;
    }
}