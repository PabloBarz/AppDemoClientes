
using BOL;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmMain : Form
    {
        private readonly ClienteBol _clienteBol;
        private readonly ProveedorBol _proveedorBol;

        public FrmMain(
            ClienteBol clienteBol,
            ProveedorBol proveedorBol)
        {
            InitializeComponent();

            _clienteBol = clienteBol;
            _proveedorBol = proveedorBol;

            // Vincular los botones con sus eventos
            btnClientes.Click += btnClientes_Click;
            btnProveedores.Click += btnProveedores_Click;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                using var formulario = new frmCliente(_clienteBol);
                formulario.ShowDialog(this);
            }
            finally
            {
                this.Show();
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                using var formulario = new frmProveedor(_proveedorBol);
                formulario.ShowDialog(this);
            }
            finally
            {
                this.Show();
            }
        }
    }
}
