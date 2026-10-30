using Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace DAL
{
    /// <summary>
    /// Acceso a datos de Clientes. En esta arquitectura el acceso a la BD es por API REST
    /// </summary>
    public class ClienteDal : IClienteDal
    {
        private const string RUTA = "https://localhost:7292/api/clientes";

        private readonly HttpClient _http;

        //Constructor 
        public ClienteDal(HttpClient http)
        {
            _http = http;
        }


        public async Task<Cliente?> CrearAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync(RUTA, cliente, ct);
            await AsegurarExitoAsync(resp, ct);
            return await resp.Content.ReadFromJsonAsync<Cliente>(ct); //Al registrar, API Rest devuelve { ... }
        }

        public async Task EliminarAsync(int id, CancellationToken ct = default)
        {
            using var resp = await _http.DeleteAsync($"{RUTA}/{id}", ct);
            await AsegurarExitoAsync(resp, ct);
        }
        public async Task ActualizarAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PutAsJsonAsync($"{RUTA}/{cliente.Id}", cliente, ct);
            await AsegurarExitoAsync(resp, ct);
        }




        /// <summary>
        /// Retorna una lista de clientes utilizando un filtro de manera opcional
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns
        public async Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default)
        {
            //¿Se nos envío datos en la variable buscar?
            var url = string.IsNullOrWhiteSpace(buscar) ? RUTA : $"{RUTA}?buscar={Uri.EscapeDataString(buscar)}";

            using var resp = await _http.GetAsync(url, ct);
            await AsegurarExitoAsync(resp, ct);

            return await resp.Content.ReadFromJsonAsync<List<Cliente>>(ct) ?? new();
        }

        public async Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default)
        {
            using var resp = await _http.GetAsync($"{RUTA}/{id}", ct);
            if(resp.StatusCode == System.Net.HttpStatusCode.NotFound) { return null; }

            await AsegurarExitoAsync(resp, ct);

            return await resp.Content.ReadFromJsonAsync<Cliente>(ct) ?? new();
        }

        private static async Task AsegurarExitoAsync(HttpResponseMessage resp, CancellationToken ct)
        {
            if(resp.IsSuccessStatusCode) return;

            //¿Y si algo no funcionó?...
            var cuerpo = await resp.Content.ReadAsStringAsync();
            var detalle = string.IsNullOrWhiteSpace(cuerpo) ? resp.ReasonPhrase : cuerpo;

            //Exception
            throw new ApiException($"El servidor respondio {resp.StatusCode}");
        }
    }
}
