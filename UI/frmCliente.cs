using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BOL;
using DAL;
using Entidades;

namespace UI
{
    public partial class frmCliente : Form
    {
        private readonly ClienteBol _bol;
        private int _idActual;

        //Definir constantes
        Color coloractivo = Color.LightYellow;
        Color colorBase = Color.White;

        private bool _cargandogrid; //Evita la seleccion mientras se renderiza los datos
        //Constructor
        public frmCliente(ClienteBol bol)
        {
            InitializeComponent();
            _bol = bol;

            //Enlace a un evento
            Load += async (_, _) => await CargarAsync();
        }

        private async Task CargarListAsync()
        {
            var lista = await _bol.ListarAsync(txtBuscar.Text);
            _cargandogrid = true;
            dgvDatos.DataSource = lista;
            dgvDatos.ClearSelection();
            _cargandogrid = false;
            configurarGrid();
            LimpiarFormulario();
        }

        private async Task EjecutarAsync(Func<Task> action)
        {
            Control? foco = null; //ReglaNegocioException -> Error -> CONTROL
            try
            {
                UseWaitCursor = true;
                Enabled = true;
                await action();
            }
            catch (ReglaNegocioException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //ReglaNegocio solo nos indica el nombre del campo: apellidos, nombres, dni, etc. (txtApellidos, txtNombres, txtDni)
                foco = ex.Campo switch
                {
                    nameof(Cliente.Apellidos) => txtApellidos,
                    nameof(Cliente.Nombres) => txtNombres,
                    nameof(Cliente.Dni) => txtDni,
                    nameof(Cliente.Telefono) => txtTelefono,
                    nameof(Cliente.Correo) => txtCorreo,
                    _ => null
                };
            }
            catch (ApiException ex)
            {
                MessageBox.Show(ex.Message, "Error en el servidor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("El servidor está demasiado ocupado", "Tiempo agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "No hay comunicación server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }

            foco?.Focus();
        }


        private Task CargarAsync() => EjecutarAsync(CargarListAsync);

        private void frmCliente_Load(object sender, EventArgs e)
        {
            //Se ejecuta despues del contructor
        }

        private async Task GuardarAsync()
        {
            var cliente = new Cliente
            {
                Id = _idActual,
                Apellidos = txtApellidos.Text,
                Nombres = txtNombres.Text,
                Dni = txtDni.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,
                Correo = txtCorreo.Text
            };

            await EjecutarAsync(async () =>
            {
                await _bol.GuardarAsync(cliente);
                await CargarListAsync();

                MessageBox.Show("Cliente guardado", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void LimpiarFormulario()
        {
            _idActual = 0;
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDni.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCorreo.Clear();
            dgvDatos.ClearSelection();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            await GuardarAsync();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloDigito(sender, e);
        }

        /// <summary>
        /// Bloque cualquier caracter ingresado excepto numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SoloDigito(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void configurarGrid()
        {
            // Configuración de columnas (Anchos mejorados) 
            dgvDatos.Columns[0].Visible = false;
            dgvDatos.Columns[1].Width = 180; 
            dgvDatos.Columns[2].Width = 180;
            dgvDatos.Columns[5].Width = 200;
            dgvDatos.Columns[6].Width = 200;

            // Estilo General del Grid 
            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDatos.GridColor = Color.FromArgb(230, 230, 230); 
            dgvDatos.RowHeadersVisible = false; 
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dgvDatos.MultiSelect = false;

            // Estilo de los Encabezados (Headers) 
            dgvDatos.EnableHeadersVisualStyles = false; 
            dgvDatos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 66, 91);
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDatos.ColumnHeadersHeight = 35;

            // Estilo de las Filas de Datos 
            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvDatos.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            dgvDatos.RowTemplate.Height = 30; 

            // Filas Alternas (Tu línea original mejorada) 
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250); 

            // Estilo de Fila Seleccionada 
            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 240); 
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void txtApellidos_Enter(object sender, EventArgs e)
        {
            txtApellidos.BackColor = coloractivo;
        }

        private void txtApellidos_Leave(object sender, EventArgs e)
        {
            txtApellidos.BackColor = colorBase;
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            //Este evento se ejecuta cuando cambia una fila activa 
            MostrarFilasSeleccionado();
        }

        private void MostrarFilasSeleccionado()
        {
            if (_cargandogrid || dgvDatos.SelectedRows.Count == 0) return;
            if (dgvDatos.SelectedRows[0].DataBoundItem is not Cliente c) return; //API


            _idActual = c.Id;
            txtApellidos.Text = c.Apellidos;
            txtNombres.Text = c.Nombres;
            txtDireccion.Text = c.Direccion;
            txtDni.Text = c.Dni;
            txtTelefono.Text = c.Telefono;
            txtCorreo.Text = c.Correo;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarAsync();
        }

        private async Task EliminarAsync()
        {
            if (_idActual == 0) return;

            string datosCliente = $"{txtApellidos} {txtNombres.Text}";
            DialogResult rpta = MessageBox.Show("¿Desea eliminar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rpta == DialogResult.No) return;

            var id = _idActual;

            //1. Eliminar > 2. Actualizar Grid
            await EjecutarAsync(async () =>
            {
                await _bol.EliminarAsync(id);
                await CargarListAsync();
            });

            LimpiarFormulario();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            //Keypress (presiona, libera) vs Keydown (botón presionado)
            //Identificar el ENTER
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarAsync();
        }
    }
}
