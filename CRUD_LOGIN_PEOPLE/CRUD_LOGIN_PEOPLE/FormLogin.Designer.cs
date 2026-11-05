

using System.Drawing;
using System.Windows.Forms;

namespace CRUD_LOGIN_PEOPLE
{
	partial class FormLogin
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

		private void InitializeComponent()
		{
			lblUsername = new Label();
			txtUsername = new TextBox();
			txtPassword = new TextBox();
			lblPassowrd = new Label();
			btnLogin = new Button();
			panel1 = new Panel();
			loginLabel = new Label();
			panel1.SuspendLayout();
			SuspendLayout();

			// lblUsername
			lblUsername.AutoSize = true;
			lblUsername.ForeColor = Color.FromArgb(15, 26, 32);
			lblUsername.Location = new Point(127, 31);
			lblUsername.Margin = new Padding(4, 0, 4, 0);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new Size(72, 19);
			lblUsername.TabIndex = 1;
			lblUsername.Text = "Usuario";

			// txtUsername
			txtUsername.BackColor = Color.FromArgb(252, 250, 249);
			txtUsername.Location = new Point(83, 56);
			txtUsername.Margin = new Padding(4, 3, 4, 3);
			txtUsername.Name = "txtUsername";
			txtUsername.Size = new Size(168, 27);
			txtUsername.TabIndex = 2;

			// txtPassword
			txtPassword.BackColor = Color.FromArgb(252, 250, 249);
			txtPassword.Location = new Point(83, 121);
			txtPassword.Margin = new Padding(4, 3, 4, 3);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.Size = new Size(168, 27);
			txtPassword.TabIndex = 3;

			// lblPassowrd
			lblPassowrd.AutoSize = true;
			lblPassowrd.ForeColor = Color.FromArgb(15, 26, 32);
			lblPassowrd.Location = new Point(118, 97);
			lblPassowrd.Margin = new Padding(4, 0, 4, 0);
			lblPassowrd.Name = "lblPassowrd";
			lblPassowrd.Size = new Size(99, 19);
			lblPassowrd.TabIndex = 4;
			lblPassowrd.Text = "Contraseña";

			// btnLogin
			btnLogin.BackColor = Color.FromArgb(39, 39, 39);
			btnLogin.ForeColor = Color.White;
			btnLogin.Location = new Point(83, 162);
			btnLogin.Margin = new Padding(4, 3, 4, 3);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(167, 33);
			btnLogin.TabIndex = 5;
			btnLogin.Text = "Iniciar sesión";
			btnLogin.UseVisualStyleBackColor = false;
			btnLogin.Click += btnLogin_Click;

			// panel1
			panel1.BackColor = Color.FromArgb(158, 173, 200);
			panel1.Controls.Add(txtUsername);
			panel1.Controls.Add(btnLogin);
			panel1.Controls.Add(lblUsername);
			panel1.Controls.Add(txtPassword);
			panel1.Controls.Add(lblPassowrd);
			panel1.Location = new Point(242, 114);
			panel1.Margin = new Padding(4, 3, 4, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(338, 238);
			panel1.TabIndex = 6;
			panel1.Paint += panel1_Paint;

			// loginLabel
			loginLabel.AutoSize = true;
			loginLabel.Font = new Font("JetBrains Mono Medium", 16F, FontStyle.Bold);
			loginLabel.Location = new Point(286, 65);
			loginLabel.Name = "loginLabel";
			loginLabel.Size = new Size(253, 36);
			loginLabel.TabIndex = 7;
			loginLabel.Text = "Iniciar sesión";
			loginLabel.Click += loginLabel_Click;

			// FormLogin
			AutoScaleDimensions = new SizeF(9F, 19F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(225, 232, 224);
			ClientSize = new Size(902, 430);
			Controls.Add(loginLabel);
			Controls.Add(panel1);
			Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			ForeColor = Color.FromArgb(15, 26, 32);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FormLogin";
			Text = "Acceso sistema de empleados";
			Load += FormLogin_Load;

			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblUsername;
		private TextBox txtUsername;
		private TextBox txtPassword;
		private Label lblPassowrd;
		private Button btnLogin;
		private Panel panel1;
		private Label loginLabel;
	}
}
