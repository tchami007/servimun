using ServiMun.Models;

namespace ServiMun.Services
{
    public interface ITributoMunicipalService
    {
        Task<TributoMunicipal> AltaNuevoTributoMunicipal(TributoMunicipal tributo);
        Task<bool> BajaTributoMunicipal(int id);
        Task<TributoMunicipal> ModificacionTributoMunicipal(TributoMunicipal tributo);
        Task<TributoMunicipal> RecuperacionTributoMunicipal(int id);
        Task<IEnumerable<TributoMunicipal>> RecuperacionTodosTributoMunicipal();
    }
}
