
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

using BOL;
using DAL;
using Entidades;

namespace UI
{
    public partial class frmProveedor : Form
    {
        private readonly ProveedorBol _bol;
        private int _idActual;

        private bool _cargandoGrid;

        private readonly Color colorActivo = Color.LightYellow;
        private readonly Color colorBase = Color.White;

        public frmProveedor(ProveedorBol bol)
        {
            InitializeComponent();
            _bol = bol;

            Load += async (_, _) => await CargarAsync();

            btnBuscar.Click += btnBuscar_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnNuevo.Click += btnNuevo_Click;

            txtBuscar.KeyDown += txtBuscar_KeyDown;
            txtRuc.KeyPress += SoloDigito;
            txtTelefono.KeyPress += SoloDigito;

            txtRazonSocial.Enter += (_, _) =>
                txtRazonSocial.BackColor = colorActivo;

            txtRazonSocial.Leave += (_, _) =>
                txtRazonSocial.BackColor = colorBase;

            dgvDatos.SelectionChanged += dgvDatos_SelectionChanged;

            txtRazonSocial.MaxLength = 120;
            txtRuc.MaxLength = 11;
            txtTelefono.MaxLength = 9;
            txtCorreo.MaxLength = 150;
            txtDireccion.MaxLength = 150;
        }

        private async Task CargarListAsync()
        {
            var lista = await _bol.ListarAsync(txtBuscar.Text);

            _cargandoGrid = true;

            try
            {
                dgvDatos.DataSource = lista;
                ConfigurarGrid();
                dgvDatos.ClearSelection();
            }
            finally
            {
                _cargandoGrid = false;
            }

            LimpiarFormulario();
        }

        private async Task EjecutarAsync(Func<Task> action)
        {
            Control? foco = null;

            try
            {
                UseWaitCursor = true;
                Enabled = false;

                await action();
            }
            catch (ReglaNegocioException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                foco = ex.Campo switch
                {
                    nameof(Proveedor.RazonSocial) => txtRazonSocial,
                    nameof(Proveedor.Ruc) => txtRuc,
                    nameof(Proveedor.Telefono) => txtTelefono,
                    nameof(Proveedor.Correo) => txtCorreo,
                    nameof(Proveedor.Direccion) => txtDireccion,
                    _ => null
                };
            }
            catch (ApiException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error en el servidor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "La solicitud excedió el tiempo de espera.",
                    "Tiempo agotado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }

            foco?.Focus();
        }

        private Task CargarAsync()
            => EjecutarAsync(CargarListAsync);

        private async Task GuardarAsync()
        {
            var proveedor = new Proveedor
            {
                Id = _idActual,
                RazonSocial = txtRazonSocial.Text,
                Ruc = txtRuc.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text
            };

            await EjecutarAsync(async () =>
            {
                await _bol.GuardarAsync(proveedor);
                await CargarListAsync();

                MessageBox.Show(
                    "Proveedor guardado correctamente.",
                    "Proveedores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            });
        }

        private void LimpiarFormulario()
        {
            _idActual = 0;

            txtRazonSocial.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            dgvDatos.ClearSelection();
        }

        private void ConfigurarGrid()
        {
            if (dgvDatos.Columns.Count == 0) return;

            dgvDatos.Columns[nameof(Proveedor.Id)]!.Visible = false;

            dgvDatos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvDatos.GridColor = Color.FromArgb(230, 230, 230);
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDatos.MultiSelect = false;
            dgvDatos.ReadOnly = true;

            dgvDatos.EnableHeadersVisualStyles = false;
            dgvDatos.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(45, 66, 91);

            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvDatos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvDatos.ColumnHeadersHeight = 35;

            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor =
                Color.FromArgb(64, 64, 64);

            dgvDatos.DefaultCellStyle.Font =
                new Font("Segoe UI", 9.5f);

            dgvDatos.RowTemplate.Height = 30;

            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            dgvDatos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(200, 220, 240);

            dgvDatos.DefaultCellStyle.SelectionForeColor =
                Color.Black;
        }

        private void MostrarFilaSeleccionada()
        {
            if (_cargandoGrid || dgvDatos.SelectedRows.Count == 0)
                return;

            if (dgvDatos.SelectedRows[0].DataBoundItem is not Proveedor p)
                return;

            _idActual = p.Id;

            txtRazonSocial.Text = p.RazonSocial;
            txtRuc.Text = p.Ruc;
            txtTelefono.Text = p.Telefono;
            txtCorreo.Text = p.Correo;
            txtDireccion.Text = p.Direccion;
        }

        private async Task EliminarAsync()
        {
            if (_idActual == 0) return;

            var respuesta = MessageBox.Show(
                $"¿Desea eliminar al proveedor {txtRazonSocial.Text}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            int id = _idActual;

            await EjecutarAsync(async () =>
            {
                await _bol.EliminarAsync(id);
                await CargarListAsync();
            });
        }

        private static void SoloDigito(
            object? sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void btnGuardar_Click(
            object? sender, EventArgs e)
        {
            await GuardarAsync();
        }

        private async void btnEliminar_Click(
            object? sender, EventArgs e)
        {
            await EliminarAsync();
        }

        private void btnNuevo_Click(
            object? sender, EventArgs e)
        {
            LimpiarFormulario();
            txtRazonSocial.Focus();
        }

        private async void btnBuscar_Click(
            object? sender, EventArgs e)
        {
            await CargarAsync();
        }

        private void txtBuscar_KeyDown(
            object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;
            btnBuscar.PerformClick();
        }

        private void dgvDatos_SelectionChanged(
            object? sender, EventArgs e)
        {
            MostrarFilaSeleccionada();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
