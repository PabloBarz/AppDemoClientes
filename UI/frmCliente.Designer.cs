namespace UI
{
    partial class frmCliente
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
            txtBuscar = new TextBox();
            lbBuscar = new Label();
            dgvDatos = new DataGridView();
            btnGuardar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtApellidos = new TextBox();
            txtNombres = new TextBox();
            txtCorreo = new TextBox();
            txtDni = new TextBox();
            txtDireccion = new TextBox();
            txtTelefono = new TextBox();
            btnNuevo = new Button();
            btnEliminar = new Button();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(141, 50);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(314, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // lbBuscar
            // 
            lbBuscar.AutoSize = true;
            lbBuscar.Location = new Point(54, 53);
            lbBuscar.Name = "lbBuscar";
            lbBuscar.Size = new Size(45, 15);
            lbBuscar.TabIndex = 0;
            lbBuscar.Text = "Buscar:";
            // 
            // dgvDatos
            // 
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.AllowUserToOrderColumns = true;
            dgvDatos.AllowUserToResizeColumns = false;
            dgvDatos.AllowUserToResizeRows = false;
            dgvDatos.BackgroundColor = SystemColors.ButtonHighlight;
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatos.Location = new Point(54, 120);
            dgvDatos.MultiSelect = false;
            dgvDatos.Name = "dgvDatos";
            dgvDatos.ReadOnly = true;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Size = new Size(772, 286);
            dgvDatos.TabIndex = 18;
            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(160, 611);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 439);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 3;
            label1.Text = "Apellidos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(296, 439);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 5;
            label2.Text = "Nombres";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(527, 439);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 7;
            label3.Text = "Correo:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(54, 516);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 9;
            label4.Text = "DNI:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(296, 516);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 11;
            label5.Text = "Telefono:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(527, 516);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 13;
            label6.Text = "Dirección:";
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(54, 469);
            txtApellidos.MaxLength = 50;
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(181, 23);
            txtApellidos.TabIndex = 4;
            txtApellidos.Enter += txtApellidos_Enter;
            txtApellidos.Leave += txtApellidos_Leave;
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(296, 469);
            txtNombres.MaxLength = 50;
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(181, 23);
            txtNombres.TabIndex = 6;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(536, 469);
            txtCorreo.MaxLength = 150;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(181, 23);
            txtCorreo.TabIndex = 8;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(54, 544);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(181, 23);
            txtDni.TabIndex = 10;
            txtDni.KeyPress += txtDni_KeyPress;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(536, 544);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(181, 23);
            txtDireccion.TabIndex = 14;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(296, 544);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(181, 23);
            txtTelefono.TabIndex = 12;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(59, 611);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(75, 23);
            btnNuevo.TabIndex = 19;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(277, 611);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 20;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(490, 53);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // frmCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 670);
            Controls.Add(btnEliminar);
            Controls.Add(btnNuevo);
            Controls.Add(txtDireccion);
            Controls.Add(txtCorreo);
            Controls.Add(txtDni);
            Controls.Add(txtTelefono);
            Controls.Add(txtNombres);
            Controls.Add(txtApellidos);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGuardar);
            Controls.Add(dgvDatos);
            Controls.Add(lbBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Name = "frmCliente";
            Text = "frmCliente";
            Load += frmCliente_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtBuscar;
        private Label lbBuscar;
        private DataGridView dgvDatos;
        private Button button3;
        private Button button4;
        private Button btnGuardar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtApellidos;
        private TextBox txtNombres;
        private TextBox txtCorreo;
        private TextBox txtDni;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnBuscar;
    }
}