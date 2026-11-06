
using Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace DAL
{
    /// <summary>
    /// Acceso a datos de proveedores mediante API REST.
    /// </summary>
    public class ProveedorDal : IProveedorDal
    {
        private const string RUTA = "https://localhost:7292/api/proveedores";

        private readonly HttpClient _http;

        // Constructor
        public ProveedorDal(HttpClient http)
        {
            _http = http;
        }

        // GET: Listar proveedores con filtro opcional
        public async Task<List<Proveedor>> ListarAsync(
            string? buscar = null,
            CancellationToken ct = default)
        {
            var url = string.IsNullOrWhiteSpace(buscar)
                ? RUTA
                : $"{RUTA}?buscar={Uri.EscapeDataString(buscar)}";

            using var resp = await _http.GetAsync(url, ct);

            await AsegurarExitoAsync(resp, ct);

            return await resp.Content
                .ReadFromJsonAsync<List<Proveedor>>(ct) ?? new();
        }

        // GET: Obtener proveedor por ID
        public async Task<Proveedor?> ObtenerAsync(
            int id,
            CancellationToken ct = default)
        {
            using var resp = await _http.GetAsync($"{RUTA}/{id}", ct);

            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            await AsegurarExitoAsync(resp, ct);

            return await resp.Content
                .ReadFromJsonAsync<Proveedor>(ct);
        }

        // POST: Registrar proveedor
        public async Task<Proveedor?> CrearAsync(
            Proveedor proveedor,
            CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync(
                RUTA, proveedor, ct);

            await AsegurarExitoAsync(resp, ct);

            return await resp.Content
                .ReadFromJsonAsync<Proveedor>(ct);
        }

        // PUT: Actualizar proveedor
        public async Task ActualizarAsync(
            Proveedor proveedor,
            CancellationToken ct = default)
        {
            using var resp = await _http.PutAsJsonAsync(
                $"{RUTA}/{proveedor.Id}", proveedor, ct);

            await AsegurarExitoAsync(resp, ct);
        }

        // DELETE: Eliminar proveedor
        public async Task EliminarAsync(
            int id,
            CancellationToken ct = default)
        {
            using var resp = await _http.DeleteAsync(
                $"{RUTA}/{id}", ct);

            await AsegurarExitoAsync(resp, ct);
        }

        // Manejo de errores HTTP
        private static async Task AsegurarExitoAsync(
            HttpResponseMessage resp,
            CancellationToken ct)
        {
            if (resp.IsSuccessStatusCode)
            {
                return;
            }

            var cuerpo = await resp.Content.ReadAsStringAsync(ct);

            var detalle = string.IsNullOrWhiteSpace(cuerpo)
                ? resp.ReasonPhrase
                : cuerpo;

            throw new ApiException(
                $"El servidor respondió {resp.StatusCode}: {detalle}");
        }
    }
}
