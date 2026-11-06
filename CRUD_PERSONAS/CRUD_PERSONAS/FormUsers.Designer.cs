using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_PERSONAS
{
    partial class FormUsers
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
            panel1 = new Panel();
            label1 = new Label();
            cmbShift = new ComboBox();
            lblNewPassword = new Label();
            lblNewUser = new Label();
            lblTurno = new Label();
            btnSaveUser = new Button();
            txtNewPass = new TextBox();
            txtNewUser = new TextBox();
            panel2 = new Panel();
            label3 = new Label();
            dgvUsers = new DataGridView();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(158, 173, 200);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbShift);
            panel1.Controls.Add(lblNewPassword);
            panel1.Controls.Add(lblNewUser);
            panel1.Controls.Add(lblTurno);
            panel1.Controls.Add(btnSaveUser);
            panel1.Controls.Add(txtNewPass);
            panel1.Controls.Add(txtNewUser);
            panel1.Location = new Point(35, 93);
            panel1.Name = "panel1";
            panel1.Size = new Size(443, 354);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 28);
            label1.Name = "label1";
            label1.Size = new Size(237, 30);
            label1.TabIndex = 6;
            label1.Text = "Datos del cajero";
            // 
            // cmbShift
            // 
            cmbShift.FormattingEnabled = true;
            cmbShift.Location = new Point(102, 172);
            cmbShift.Name = "cmbShift";
            cmbShift.Size = new Size(328, 27);
            cmbShift.TabIndex = 4;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("JetBrains Mono", 12F);
            lblNewPassword.Location = new Point(12, 126);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(144, 26);
            lblNewPassword.TabIndex = 5;
            lblNewPassword.Text = "Contraseña:";
            // 
            // lblNewUser
            // 
            lblNewUser.AutoSize = true;
            lblNewUser.Font = new Font("JetBrains Mono", 12F);
            lblNewUser.Location = new Point(12, 74);
            lblNewUser.Name = "lblNewUser";
            lblNewUser.Size = new Size(108, 26);
            lblNewUser.TabIndex = 4;
            lblNewUser.Text = "Cajero: ";
            // 
            // lblTurno
            // 
            lblTurno.AutoSize = true;
            lblTurno.Font = new Font("JetBrains Mono", 12F);
            lblTurno.Location = new Point(12, 173);
            lblTurno.Name = "lblTurno";
            lblTurno.Size = new Size(84, 26);
            lblTurno.TabIndex = 3;
            lblTurno.Text = "Turno:";
            // 
            // btnSaveUser
            // 
            btnSaveUser.BackColor = Color.FromArgb(39, 39, 39);
            btnSaveUser.Font = new Font("JetBrains Mono", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveUser.ForeColor = Color.White;
            btnSaveUser.Location = new Point(12, 223);
            btnSaveUser.Name = "btnSaveUser";
            btnSaveUser.Size = new Size(410, 49);
            btnSaveUser.TabIndex = 1;
            btnSaveUser.Text = "Guardar Cajero";
            btnSaveUser.UseVisualStyleBackColor = false;
            btnSaveUser.Click += btnSaveUser_Click;
            // 
            // txtNewPass
            // 
            txtNewPass.Location = new Point(156, 128);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '*';
            txtNewPass.Size = new Size(274, 27);
            txtNewPass.TabIndex = 2;
            // 
            // txtNewUser
            // 
            txtNewUser.Location = new Point(121, 75);
            txtNewUser.Name = "txtNewUser";
            txtNewUser.Size = new Size(309, 27);
            txtNewUser.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(158, 173, 200);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(dgvUsers);
            panel2.Location = new Point(505, 76);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 387);
            panel2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(18, 17);
            label3.Name = "label3";
            label3.Size = new Size(279, 30);
            label3.TabIndex = 7;
            label3.Text = "Cajeros registrados";
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.BackgroundColor = Color.FromArgb(252, 250, 249);
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(20, 63);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(762, 292);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(35, 9);
            label2.Name = "label2";
            label2.Size = new Size(493, 37);
            label2.TabIndex = 4;
            label2.Text = "Registro de cajeros y turnos";
            // 
            // FormUsers
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(225, 232, 224);
            ClientSize = new Size(1336, 474);
            Controls.Add(label2);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "FormUsers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de Cajeros";
            Load += FormUsers_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private TextBox txtNewPass;
        private TextBox txtNewUser;
        private Button btnSaveUser;
        private Label lblNewPassword;
        private Label lblNewUser;
        private Label lblTurno;
        private DataGridView dgvUsers;
        private ComboBox cmbShift;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}