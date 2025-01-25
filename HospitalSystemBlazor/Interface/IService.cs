using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;

namespace HospitalSystemBlazor.Service.Interface
{
    public interface IService <T> where T: class
    {
        public Task<Result<List<T>>> Listas();
        public Task<Result<T>> Detalles(int id);
        public Task<Result<string>> Crear(T model);
        public Task<Result<string>> Editar(int id, T model);
        public Task<Result<string>> Eliminar(int id);
    }
}
