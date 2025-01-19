using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Web.Utilities;
using System.Net.Http.Json;

namespace HospitalSystemBlazor.Web.Services
{
    public class EspecialidadService
    {
        private readonly HttpClient _http;

        public EspecialidadService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EspecialidadDTO>> Especialidades()
        {
            try
            {
                var response = await _http.GetAsync("api/Especialidades");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la API: {response.StatusCode}, Detalles: {errorContent}");
                }

                return await response.Content.ReadFromJsonAsync<List<EspecialidadDTO>>()
                    ?? new List<EspecialidadDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<EspecialidadDTO> Detalle(int id)
        {
            try
            {
                var response = await _http.GetAsync($"api/Especialidades/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var mensajeError = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{mensajeError}");
                }

                return await response.Content.ReadFromJsonAsync<EspecialidadDTO>()
                    ?? new EspecialidadDTO();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MensajeOperacion> Crear(EspecialidadDTO model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Especialidades/NuevaEsp", model);
                var mensaje = await response.Content.ReadAsStringAsync();
                    
                return new MensajeOperacion 
                { 
                    Exito = response.IsSuccessStatusCode, 
                    Mensaje = mensaje
                };
            }
            catch(Exception ex)
            {
                return new MensajeOperacion { Exito = false, Mensaje = ex.Message };
            }
        }

        public async Task<MensajeOperacion> Editar(int id, EspecialidadDTO model)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"api/Especialidades/Actualizar/{id}", model);
                var mensaje = await response.Content.ReadAsStringAsync();

                return new MensajeOperacion
                {
                    Exito = response.IsSuccessStatusCode,
                    Mensaje = mensaje
                };
            }
            catch(Exception ex)
            {
                return new MensajeOperacion { Exito = false, Mensaje = ex.Message };
            }
        }

        public async Task<MensajeOperacion> Eliminar(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/Especialidades/Eliminar/{id}");
                var mensaje = await response.Content.ReadAsStringAsync();

                return new MensajeOperacion
                {
                    Exito = response.IsSuccessStatusCode,
                    Mensaje = mensaje
                };
            }
            catch(Exception ex)
            {
                return new MensajeOperacion { Exito = false, Mensaje = ex.Message };
            }
        }
    }
}
