using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;
using System.Linq;

namespace ServiMun.Services
{

    public interface IPadronContribuyenteService
    {
        Task<Result<PadronContribuyente>> AddPadronContribuyente(PadronContribuyente padronContribuyente);
        Task<Result<PadronContribuyente>> DeletePadronContribuyente(int idContribuyente, int idTributoMunicipal);
        Task<Result<PadronContribuyente>> UpdatePadronContribuyente(PadronContribuyenteGetDTO padronGetDTO);
        Task<Result<PadronContribuyente>> GetPadronContribuyentePadronById(int idContribuyente, int idTributoMunicipal);
        Task<IEnumerable<PadronContribuyente>> GetAllPadronContribuyenteByIdTributo(int idTributoMunicipal);
        Task<Result<PadronContribuyente>> GetPadronContribuyentePadronByNumeroPadron(int numeroPadron);
    }

    public class PadronContribuyenteService : IPadronContribuyenteService
    {
        private readonly IPadronContribuyenteRepository _padronContribuyente;
        private readonly IPadronBoletaRepository _padronBoleta;

        public PadronContribuyenteService(IPadronContribuyenteRepository padronContribuyente, IPadronBoletaRepository padronBoleta)
        {
            _padronContribuyente = padronContribuyente;
            _padronBoleta = padronBoleta;
        }

        public async Task<Result<PadronContribuyente>> AddPadronContribuyente(PadronContribuyente padronContribuyente)
        {
            var resultado = await _padronContribuyente.AddItem(padronContribuyente);
            return resultado;
        }

        public async Task<Result<PadronContribuyente>> DeletePadronContribuyente(int idContribuyente, int idTributoMunicipal)
        {
            var resultado = await _padronContribuyente.DeleteItem(idContribuyente, idTributoMunicipal);
            return resultado;
        }

        public async Task<Result<PadronContribuyente>> UpdatePadronContribuyente(PadronContribuyenteGetDTO padronDTO)
        {
            // Verificar si el NumeroPadron existe en PadronBoleta
            var padronExists = await _padronBoleta.GetAllByNumeroPadron(padronDTO.NumeroPadron);
            if (padronExists.Count()>0)
            {
                return Result<PadronContribuyente>.Failure("Padron Contribuyente no identificado");
            }

            // Eliminar el registro existente
            var existingPadron = await _padronContribuyente.GetById(padronDTO.IdContribuyente, padronDTO.IdTributoMunicipal);
            if (existingPadron != null)
            {
                await _padronContribuyente.DeleteItem(padronDTO.IdContribuyente, padronDTO.IdContribuyente);
            }

            // Conversion de DTO a Model
            PadronContribuyente pad = new PadronContribuyente()
            {
                IdContribuyente = padronDTO.IdContribuyente,
                IdTributoMunicipal = padronDTO.IdTributoMunicipal,
                NumeroPadron = padronDTO.NumeroPadron,
                Estado = padronDTO.Estado
            };

            // Agregar el nuevo registro
            await _padronContribuyente.AddItem(pad);
            return Result<PadronContribuyente>.Success(pad);
        }

        public async Task<Result<PadronContribuyente>> GetPadronContribuyentePadronById(int idContribuyente, int idTributoMunicipal)
        {
            var resultado = await _padronContribuyente.GetById(idContribuyente, idTributoMunicipal);
            return resultado;
        }

        public async Task<IEnumerable<PadronContribuyente>> GetAllPadronContribuyenteByIdTributo(int idTributoMunicipal)
        {
            var resultado = await _padronContribuyente.GetAllByIdTributo(idTributoMunicipal);
            return resultado;
        }

        public async Task<Result<PadronContribuyente>> GetPadronContribuyentePadronByNumeroPadron(int numeroPadron) 
        { 
            var resultado = await _padronContribuyente.GetByNumeroPadron(numeroPadron);
            return resultado;
        }
    }

}
