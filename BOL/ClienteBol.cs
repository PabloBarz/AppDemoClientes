using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BOL
{
    /// <summary>
    /// La UI solo puede conversar con esta clase 
    /// </summary>
    public class ClienteBol
    {
        // Se asume la inyección o inicialización de la capa de acceso a datos (DAL)
        private readonly ClienteDal _dal;
        public ClienteBol(ClienteDal dal)
        {
            _dal = dal;
        }

        public Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default)
            => _dal.ListarAsync(buscar?.Trim(), ct);

        /// <summary>
        /// Buscador de clientes por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default)
            => _dal.ObtenerAsync(id, ct);

        public async Task GuardarAsync(Cliente cliente, CancellationToken ct = default)
        {
            // Validación
            Normalizar(cliente);        // Quitar los espacios en blanco (sanitizar)
            Validar(cliente);

            // UI nos envía un ID = 0 (NUEVO)
            // UI nos envía un ID = 7 (ACTUALIZAR)
            if (cliente.Id == 0) await _dal.CrearAsync(cliente, ct);
            else await _dal.ActualizarAsync(cliente, ct);
            
        }

        /// <summary>
        /// Elimina un cliente por su ID a través de la capa de datos.
        /// </summary>
        /// <param name="id">ID del cliente a eliminar.</param>
        /// <param name="ct">Token de cancelación opcional.</param>
        /// <returns></returns>
        public async Task EliminarAsync(int id, CancellationToken ct = default)
        {
            // Validación de regla de negocio previa a la llamada del servicio
            if (id <= 0)
            {
                throw new ReglaNegocioException("El ID del cliente proporcionado no es válido para su eliminación.");
            }

            // Delegación directa a la capa de acceso a datos (DAL)
            await _dal.EliminarAsync(id, ct);
        }


        private static void Normalizar(Cliente cliente)
        {
            cliente.Apellidos = cliente.Apellidos.Trim();
            cliente.Nombres = cliente.Nombres.Trim();
            cliente.Dni = cliente.Dni.Trim();
            cliente.Telefono = cliente.Telefono.Trim();
            cliente.Direccion = cliente.Direccion.Trim();
            cliente.Correo = cliente.Correo.Trim();
        }

        private static void Validar(Cliente cliente)
        {
            if (cliente.Apellidos.Length == 0) { throw new ReglaNegocioException("Ingresa los apellidos", nameof(cliente.Apellidos)); }
            if (cliente.Nombres.Length == 0) { throw new ReglaNegocioException("Ingrese su nombre", nameof(cliente.Nombres)); }
            if (cliente.Dni.Length != 8) { throw new ReglaNegocioException("Escriba correctamente DNI", nameof(cliente.Dni)); }

            // Corregido: Se eliminaron las comillas dobles internas adicionales
            if (!Regex.IsMatch(cliente.Dni, @"^\d{8}$")) { throw new ReglaNegocioException("El DNI debe tener 8 dígitos", nameof(cliente.Dni)); }

            if (cliente.Telefono.Length != 9) { throw new ReglaNegocioException("El teléfono debe tener 9 dígitos", nameof(cliente.Telefono)); }
        }
    }
}