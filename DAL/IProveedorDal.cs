
using System;
using System.Collections.Generic;
using System.Text;

using Entidades;

namespace DAL
{
    public interface IProveedorDal
    {
        // Listar proveedores y aplicar filtro de búsqueda
        Task<List<Proveedor>> ListarAsync(string? buscar = null, CancellationToken ct = default);

        // Obtener proveedor por ID
        Task<Proveedor?> ObtenerAsync(int id, CancellationToken ct = default);

        // Registrar proveedor
        Task<Proveedor?> CrearAsync(Proveedor proveedor, CancellationToken ct = default);

        // Actualizar proveedor
        Task ActualizarAsync(Proveedor proveedor, CancellationToken ct = default);

        // Eliminar proveedor
        Task EliminarAsync(int id, CancellationToken ct = default);
    }
}
