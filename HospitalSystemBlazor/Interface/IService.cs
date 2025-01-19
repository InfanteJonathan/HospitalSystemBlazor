using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;

namespace HospitalSystemBlazor.Service.Interface
{
    public interface IService <T> where T: class
    {
        Task<Result<List<T>>> ListaEspecialidades();
        Task<Result<T>> Detalles(int id);
        Task<Result<string>> Crear(T model);
        Task<Result<string>> Editar(int id, T model);
        Task<Result<string>> Eliminar(int id);
    }
}
