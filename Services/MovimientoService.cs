using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{

    public interface IMovimientoService
    {
        Task<Result<Movimiento>> AddMovimiento(Movimiento movimiento);
        Task<Result<Movimiento>> UpdateMovimiento(int idMovimiento, Movimiento movimiento);
        Task<Result<Movimiento>> GetMovimientoById(int id);
        Task<IEnumerable<Movimiento>> GetAllMovimientoByFecha(DateTime fechaMovimiento);
        Task<Result<Movimiento>> GetMovimientoByTipoNumeroPeriodo(string tipo, int numero, int periodo);
        Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoByTipoId(string tipo, int id, DateTime fecha);
        Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoTotalByTipoId(string tipo, int id, DateTime fecha);

    }
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository) 
        { 
            _movimientoRepository = movimientoRepository; 
        }
        public async Task<Result<Movimiento>> AddMovimiento(Movimiento movimiento)
        { 
            var resultado = await _movimientoRepository.AddMovimiento(movimiento);
            return resultado;
        }
        public async Task<Result<Movimiento>> UpdateMovimiento(int idMovimiento, Movimiento movimiento)
        {
            var encontrado = await _movimientoRepository.GetMovimientoById(idMovimiento);

            if (encontrado._succes)
            {
                encontrado._value.FechaReal = movimiento.FechaReal;
                encontrado._value.FechaMovimiento = movimiento.FechaMovimiento;
                encontrado._value.Importe = movimiento.Importe;
                encontrado._value.NumeroComprobante = movimiento.NumeroComprobante;
                encontrado._value.Numero = movimiento.Numero;
                encontrado._value.Contrasiento = movimiento.Contrasiento;
                encontrado._value.IdServicio = movimiento.IdServicio;
                encontrado._value.IdTributo = movimiento.IdTributo;

                var resultado = await _movimientoRepository.UpdateMovimiento(encontrado._value);
                return resultado;
            }

            return encontrado;
        }
        public async Task<Result<Movimiento>> GetMovimientoById(int idMovimiento) 
        { 
            var resultado = await _movimientoRepository.GetMovimientoById(idMovimiento);
            return resultado;
        }
        public async Task<IEnumerable<Movimiento>> GetAllMovimientoByFecha(DateTime fechaMovimiento)
        {
            var resultados = await _movimientoRepository.GetAllMovimientoByFecha(fechaMovimiento);
            return resultados;
        }
        public async Task<Result<Movimiento>> GetMovimientoByTipoNumeroPeriodo(string tipo, int numero, int periodo)
        {
            var resultado = await _movimientoRepository.GetMovimientoByTipoNumeroPeriodo(tipo, numero, periodo);
            return resultado;
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _movimientoRepository.GetAllMovimientoByTipoId(tipo, id, fecha);
            return resultado;
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetAllMovimientoTotalByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _movimientoRepository.GetAllMovimientoTotalByTipoId(tipo, id, fecha);
            return resultado;
        }


    }
}
