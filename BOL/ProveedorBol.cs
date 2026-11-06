
using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BOL
{
    /// <summary>
    /// La UI se comunica con esta clase para gestionar proveedores.
    /// </summary>
    public class ProveedorBol
    {
        private readonly ProveedorDal _dal;

        // Constructor
        public ProveedorBol(ProveedorDal dal)
        {
            _dal = dal;
        }

        // Listar proveedores con búsqueda opcional
        public Task<List<Proveedor>> ListarAsync(
            string? buscar = null,
            CancellationToken ct = default)
            => _dal.ListarAsync(buscar?.Trim(), ct);

        // Obtener proveedor por ID
        public Task<Proveedor?> ObtenerAsync(
            int id,
            CancellationToken ct = default)
            => _dal.ObtenerAsync(id, ct);

        // Registrar o actualizar proveedor
        public async Task GuardarAsync(
            Proveedor proveedor,
            CancellationToken ct = default)
        {
            Normalizar(proveedor);
            Validar(proveedor);

            // ID = 0: registrar nuevo proveedor
            // ID > 0: actualizar proveedor existente
            if (proveedor.Id == 0)
            {
                await _dal.CrearAsync(proveedor, ct);
            }
            else
            {
                await _dal.ActualizarAsync(proveedor, ct);
            }
        }

        // Eliminar proveedor
        public async Task EliminarAsync(
            int id,
            CancellationToken ct = default)
        {
            if (id <= 0)
            {
                throw new ReglaNegocioException(
                    "El ID del proveedor no es válido para su eliminación.");
            }

            await _dal.EliminarAsync(id, ct);
        }

        // Eliminar espacios innecesarios
        private static void Normalizar(Proveedor proveedor)
        {
            proveedor.RazonSocial = proveedor.RazonSocial.Trim();
            proveedor.Ruc = proveedor.Ruc.Trim();
            proveedor.Telefono = proveedor.Telefono.Trim();
            proveedor.Correo = proveedor.Correo.Trim();
            proveedor.Direccion = proveedor.Direccion.Trim();
        }

        // Validaciones de negocio
        private static void Validar(Proveedor proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.RazonSocial))
            {
                throw new ReglaNegocioException(
                    "Ingrese la razón social del proveedor.",
                    nameof(proveedor.RazonSocial));
            }

            if (proveedor.RazonSocial.Length > 120)
            {
                throw new ReglaNegocioException(
                    "La razón social no puede superar los 120 caracteres.",
                    nameof(proveedor.RazonSocial));
            }

            if (!Regex.IsMatch(proveedor.Ruc, @"^[0-9]{11}$"))
            {
                throw new ReglaNegocioException(
                    "El RUC debe contener exactamente 11 dígitos numéricos.",
                    nameof(proveedor.Ruc));
            }

            if (!Regex.IsMatch(proveedor.Telefono, @"^[0-9]{9}$"))
            {
                throw new ReglaNegocioException(
                    "El teléfono debe contener exactamente 9 dígitos numéricos.",
                    nameof(proveedor.Telefono));
            }

            if (string.IsNullOrWhiteSpace(proveedor.Correo) ||
                proveedor.Correo.Length > 150)
            {
                throw new ReglaNegocioException(
                    "Ingrese un correo válido de máximo 150 caracteres.",
                    nameof(proveedor.Correo));
            }

            if (string.IsNullOrWhiteSpace(proveedor.Direccion) ||
                proveedor.Direccion.Length > 150)
            {
                throw new ReglaNegocioException(
                    "Ingrese una dirección de máximo 150 caracteres.",
                    nameof(proveedor.Direccion));
            }
        }
    }
}
