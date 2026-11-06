namespace UI
{
    partial class FrmMain
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
            label2 = new Label();
            btnClientes = new Button();
            btnProveedores = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(294, 48);
            label1.Name = "label1";
            label1.Size = new Size(185, 25);
            label1.TabIndex = 0;
            label1.Text = " Sistema de Gestión";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(342, 85);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 1;
            label2.Text = "Menú principal";
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.CadetBlue;
            btnClientes.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnClientes.ForeColor = SystemColors.Control;
            btnClientes.Location = new Point(147, 147);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(197, 136);
            btnClientes.TabIndex = 2;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = false;
            // 
            // btnProveedores
            // 
            btnProveedores.BackColor = Color.Goldenrod;
            btnProveedores.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnProveedores.ForeColor = SystemColors.Control;
            btnProveedores.Location = new Point(424, 147);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.Size = new Size(197, 136);
            btnProveedores.TabIndex = 3;
            btnProveedores.Text = "Proveedores";
            btnProveedores.UseVisualStyleBackColor = false;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProveedores);
            Controls.Add(btnClientes);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmMain";
            Text = "FrmMain";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnClientes;
        private Button btnProveedores;
    }
}