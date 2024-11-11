using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IServicioBoletaService
    {
        Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoletaDTO servicioBoleta);
        Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta);
        Task<Result<ServicioBoleta>> UpdateServicioBoleta(int idServicioBoleta, ServicioBoletaDTO sertvicioBoletaDTO);
        Task<Result<ServicioBoleta>> UpdateServicioBoleta2(int idServicioBoleta, ServicioBoleta sertvicioBoleta);
        Task<Result<ServicioBoleta>> PagoServicioBoleta(int idServicioBoleta);
        Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta);
        Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio);
        Task<Result<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo);
        Task<Result<IEnumerable<ServicioBoleta>>> Generar(int numeroServicio, int periodo, int cantidad);
    }
    public class ServicioBoletaService : IServicioBoletaService
    {
        private readonly IServicioBoletaRepository _servicioBoleta;
        private Random rand = new Random();
        public ServicioBoletaService(IServicioBoletaRepository servicioBoleta)
        {
            _servicioBoleta = servicioBoleta;
        }
        public async Task<Result<ServicioBoleta>> AddServicioBoleta(ServicioBoletaDTO servicioBoleta)
        {
            int numeroServicio = servicioBoleta.NumeroServicio;
            int periodo = servicioBoleta.Periodo;

            // Busqueda
            var encontrado = await _servicioBoleta.GetServicioBoletaPorId(servicioBoleta.IdBoletaServicio);
            if (encontrado != null) { return Result<ServicioBoleta>.Failure($"El servicio ya existe. Operacion  cancelada"); }

            // Nuevo elemento
            ServicioBoleta nuevaBoleta = new ServicioBoleta
            {
                NumeroServicio = numeroServicio,
                Periodo = periodo,
                Importe = servicioBoleta.Importe,
                Importe2 = servicioBoleta.Importe2,
                Pagado = servicioBoleta.Pagado,
                Vencimiento = servicioBoleta.Vencimiento,
                Vencimiento2 = servicioBoleta.Vencimiento2,
            };

            // Registro

            var resultado = await _servicioBoleta.AddServicioBoleta(nuevaBoleta);
            return resultado;
        }

        public async Task<Result<ServicioBoleta>> DeleteServicioBoleta(int idServicioBoleta)
        {
            var resultado = await _servicioBoleta.DeleteServicioBoleta(idServicioBoleta);
            return resultado;
        }

        public async Task<Result<IEnumerable<ServicioBoleta>>> Generar(int numeroServicio, int periodoInicial, int cantidad)
        {
            int per = periodoInicial;

            int anio = int.Parse(periodoInicial.ToString().Substring(0, 4));

            int mes = int.Parse(periodoInicial.ToString().Substring(4, 2));

            if (per < 202401 || per > 202601)
            {
                return Result<IEnumerable<ServicioBoleta>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. Operacion Cancelada");
            }

            if (anio < 2024 || anio > 2026)
            {
                return Result<IEnumerable<ServicioBoleta>>.Failure("Error: periodo Inicial debe ser mayor 202401 y menor a 202601. OperacioCancelada");
            }

            if (mes + cantidad - 1 > 12)
            {
                return Result<IEnumerable<ServicioBoleta>>.Failure("Error: periodo + cantidad supera mes 12. OperacioCancelada");
            }

            // Ciclo de generacion
            for (int i = 0; i <= cantidad - 1; i++)
            {
                var encontrado = await _servicioBoleta.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio ,periodoInicial);
                if (encontrado._value == null)
                {
                    // Armado de importes
                    decimal importe = rand.Next(1000, 9000);
                    decimal importe2 = importe + (importe * 10 / 100);

                    // Armado de vencimientos
                    DateTime vencimiento = new DateTime();
                    DateTime vencimiento2 = new DateTime();

                    vencimiento = DateTime.Parse(anio.ToString() + "-" + (mes + i).ToString() + "-10");
                    vencimiento2 = vencimiento.AddDays(10);

                    // Creacion de boleta
                    ServicioBoleta nuevoServicio = new ServicioBoleta
                    {
                        NumeroServicio = numeroServicio,
                        Periodo = periodoInicial,
                        Importe = importe,
                        Importe2 = importe2,
                        Vencimiento = vencimiento,
                        Vencimiento2 = vencimiento2,
                        Pagado = false
                    };

                    // Registro de nuevo padron
                    await _servicioBoleta.AddServicioBoleta(nuevoServicio);
                }

                // Proximo periodo
                periodoInicial = periodoInicial + 1;

            }

            // Recuperar todo el servicio generado
            var resultado = await _servicioBoleta.GetServicioBoletaPorNumeroServicio(numeroServicio);

            return Result<IEnumerable<ServicioBoleta>>.Success(resultado);
        }

        public async Task<Result<ServicioBoleta>> GetServicioBoletaPorId(int idServicioBoleta)
        {
            var resultado = await _servicioBoleta.GetServicioBoletaPorId(idServicioBoleta);
            return resultado;
        }

        public async Task<IEnumerable<ServicioBoleta>> GetServicioBoletaPorNumeroServicio(int numeroServicio)
        {
            var resultado = await _servicioBoleta.GetServicioBoletaPorNumeroServicio(numeroServicio);
            return resultado;
        }

        public async Task<Result<ServicioBoleta>> GetServicioBoletaPorNumeroServicioPeriodo(int numeroServicio, int periodo)
        {
            var resultado = await _servicioBoleta.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio , periodo);
            return resultado;
        }

        public async Task<Result<ServicioBoleta>> PagoServicioBoleta(int idServicioBoleta)
        {
            var encontrado = await _servicioBoleta.GetServicioBoletaPorId(idServicioBoleta);
            if (!encontrado._succes)
            {
                return Result<ServicioBoleta>.Failure($"El sevicio boleta no se encuentra. Operacion Cancelada: {idServicioBoleta}");
            }

            if (encontrado._value.Pagado)
            {
                return Result<ServicioBoleta>.Failure($"El sevicio boleta ya se encuentra pagado. Operacion Cancelada: {idServicioBoleta}");
            }

            encontrado._value.Pagado = true;

            var resultado = await _servicioBoleta.PagoServicioBoleta(encontrado._value);

            return resultado;

        }

        public async Task<Result<ServicioBoleta>> UpdateServicioBoleta(int idServicioBoleta, ServicioBoletaDTO sertvicioBoletaDTO)
        {
            if (idServicioBoleta != sertvicioBoletaDTO.IdBoletaServicio)
            {
                return Result<ServicioBoleta>.Failure("Los parametros Id y Dto no se corresponde. Operacion Cancelada");
            }

            var encontrado = await _servicioBoleta.GetServicioBoletaPorId(idServicioBoleta);

            if (!encontrado._succes)
            {
                return Result<ServicioBoleta>.Failure("El servicio boleta no se encuentra. Operacion Cancelada");
            }

            encontrado._value.NumeroServicio = sertvicioBoletaDTO.NumeroServicio;
            encontrado._value.Importe = sertvicioBoletaDTO.Importe;
            encontrado._value.Importe2 = sertvicioBoletaDTO.Importe2;
            encontrado._value.Vencimiento = sertvicioBoletaDTO.Vencimiento;
            encontrado._value.Vencimiento2 = sertvicioBoletaDTO.Vencimiento2;
            encontrado._value.Pagado = sertvicioBoletaDTO.Pagado;

            var resultado = await _servicioBoleta.UpdateServicioBoleta(encontrado._value);

            return resultado;
        }
        public async Task<Result<ServicioBoleta>> UpdateServicioBoleta2(int idServicioBoleta, ServicioBoleta sertvicioBoleta)
        {
            if (idServicioBoleta != sertvicioBoleta.IdBoletaServicio)
            {
                return Result<ServicioBoleta>.Failure("Los parametros Id y Dto no se corresponde. Operacion Cancelada");
            }

            var encontrado = await _servicioBoleta.GetServicioBoletaPorId(idServicioBoleta);

            if (!encontrado._succes)
            {
                return Result<ServicioBoleta>.Failure("El servicio boleta no se encuentra. Operacion Cancelada");
            }

            encontrado._value.NumeroServicio = sertvicioBoleta.NumeroServicio;
            encontrado._value.Importe = sertvicioBoleta.Importe;
            encontrado._value.Importe2 = sertvicioBoleta.Importe2;
            encontrado._value.Vencimiento = sertvicioBoleta.Vencimiento;
            encontrado._value.Vencimiento2 = sertvicioBoleta.Vencimiento2;
            encontrado._value.Pagado = sertvicioBoleta.Pagado;

            var resultado = await _servicioBoleta.UpdateServicioBoleta(encontrado._value);

            return resultado;
        }
    }
}
