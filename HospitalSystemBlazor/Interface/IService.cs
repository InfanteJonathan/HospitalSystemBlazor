using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;

namespace HospitalSystemBlazor.Service.Interface
{
    public interface IService <T> where T: class
    {
        public Result<List<T>> Listas();
        Result<T> Detalles(int id);
        Result<string> Crear(T model);
        Result<string> Editar(int id, T model);
        Result<string> Eliminar(int id);
    }
}
