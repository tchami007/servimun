using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;
using System.Runtime.InteropServices;

namespace ServiMun.Services
{
    public interface IPadronBoletaService
    {
        Task<Result<PadronBoleta>> AddPadronBoleta(PadronBoleta padronBoleta);
        Task<Result<PadronBoleta>> DeletePadronBoleta(int id);
        Task<Result<PadronBoleta>> UpdatePadronBoleta(int id, PadronBoleta padronBoleta);
        Task<Result<PadronBoleta>> PagoPadronBoleta(int idBoleta);
        Task<Result<PadronBoleta>> GetPadronBoletaById(int id);
        Task<IEnumerable<PadronBoleta>> GetAllPadronBoletaByNumeroPadron(int numeroPadron);
        Task<Result<PadronBoleta>> GetPadronBoletaByNumeroPadronNumeroPeriodo(int numeroPadron, int numeroPeriodo);
        Task<Result<IEnumerable<PadronBoleta>>> Generar(int numeroPadron, int periodoInicial, int cantidad);
    }


    public class PadronBoletaService : IPadronBoletaService
    {
        private readonly  IPadronBoletaRepository _padronBoletaRepository;
        private Random rand = new Random();

        public PadronBoletaService(IPadronBoletaRepository padronBoletaRepository)
        {
            _padronBoletaRepository = padronBoletaRepository;
        }

        public async Task<Result<PadronBoleta>> AddPadronBoleta(PadronBoleta padronBoleta)
        {
            var resultado = await _padronBoletaRepository.AddItem(padronBoleta);
            return resultado;
        }

        public async Task<Result<PadronBoleta>> DeletePadronBoleta(int id)
        {
            var resultado = await _padronBoletaRepository.DeleteItem(id);
            return resultado;
        }

        public async Task<Result<PadronBoleta>> UpdatePadronBoleta(int id, PadronBoleta padronBoleta)
        {
            var encontrado = await _padronBoletaRepository.GetById(id);
            if (!encontrado._succes)
            {
                return Result<PadronBoleta>.Failure("Padron Boleta no identificado");
            }

            encontrado._value.NumeroPadron = padronBoleta.NumeroPadron;
            encontrado._value.Periodo = padronBoleta.Periodo;
            encontrado._value.Importe = padronBoleta.Importe;
            encontrado._value.Vencimiento = padronBoleta.Vencimiento;
            encontrado._value.Pagado = padronBoleta.Pagado;
            encontrado._value.Vencimiento2 = padronBoleta.Vencimiento2;
            encontrado._value.Importe2 = padronBoleta.Importe2;

            var resultado = await _padronBoletaRepository.UpdateItem(encontrado._value);

            return resultado;
        }
       
        public async Task<Result<PadronBoleta>> PagoPadronBoleta(int id)
        {
            var resultado = await _padronBoletaRepository.PagoItem(id);
            return resultado;
        }
        
        public async Task<Result<PadronBoleta>> GetPadronBoletaById(int id)
        {
            var resultado = await _padronBoletaRepository.GetById(id);

            return resultado;
        }

        public async Task<IEnumerable<PadronBoleta>> GetAllPadronBoletaByNumeroPadron(int numeroPadron)
        {
            var resultado = await _padronBoletaRepository.GetAllByNumeroPadron(numeroPadron);
            return resultado;
        }

        public async Task<Result<PadronBoleta>> GetPadronBoletaByNumeroPadronNumeroPeriodo(int numeroPadron, int numeroPeriodo)
        {
            var resultado = await _padronBoletaRepository.GetByNumeroPadronPeriodo(numeroPadron, numeroPeriodo);
            return Result<PadronBoleta>.Success(resultado);
        }

        public async Task<Result<IEnumerable<PadronBoleta>>> Generar(int numeroPadron, int periodoInicial, int cantidad)
        {
            var resultado = await _padronBoletaRepository.GenerarPadronBoleta(numeroPadron, periodoInicial, cantidad);
            return resultado;
        }

    }

}
