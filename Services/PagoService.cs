using ServiMun.Models;
using ServiMun.Shared;

namespace ServiMun.Services
{
    public interface IPagoService 
    {
        Task<Result<Movimiento>> PagarBoleta(int numeroPadron, int periodo);
        Task<Result<Movimiento>> ReversarPagoServicio(int numeroServicio, int periodo);
        Task<Result<Movimiento>> PagarServicio(int numeroServicio, int periodo);
        Task<Result<Movimiento>> ReversarPagoBoleta(int numeroPadron, int periodo);
        Task<Result<Movimiento>> GetMovimiento(int idMovimiento);
        Task<Result<IEnumerable<Movimiento>>> GetMovimientoByTipoId(string tipo, int id, DateTime fecha);
        Task<Result<IEnumerable<Movimiento>>> GetMovimientoTotalByTipoId(string tipo, int id, DateTime fecha);
    }
    public class PagoService : IPagoService
    {
        private readonly IPadronBoletaService _padronBoletaService;
        private readonly IMovimientoService _movimientoService;
        private readonly IComprobanteService _comprobanteService;
        private readonly IPadronContribuyenteService _padronContribuyenteService;

        private readonly IServicioClienteService _servicioClienteService;
        private readonly IServicioBoletaService _servicioBoletaService;

        public PagoService(IPadronBoletaService padron,
            IMovimientoService movimiento,
            IComprobanteService comprobante,
            IPadronContribuyenteService padronContribuyente,

            IServicioBoletaService servicioBoleta,
            IServicioClienteService servicioCliente
            )
        {
            _comprobanteService = comprobante;
            _padronContribuyenteService = padronContribuyente;
            _movimientoService = movimiento;
            _padronBoletaService = padron;

            _servicioBoletaService = servicioBoleta;
            _servicioClienteService = servicioCliente;
        }
        public async Task<Result<Movimiento>> PagarServicio(int numeroServicio, int periodo)
        {
            var servicio = await _servicioBoletaService.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio, periodo);

            if (!servicio._succes)
            {
                return Result<Movimiento>.Failure("Servicio Boleta no identificado");
            }

            var cliente = await _servicioClienteService.getServicioClienteByNumeroServicio(numeroServicio);
            if (!cliente._succes)
            {
                return Result<Movimiento>.Failure("Cliente no identificado");
            }

            // Pago

            var resultPago = await _servicioBoletaService.PagoServicioBoleta(servicio._value.IdBoletaServicio);

            if (!resultPago._succes)
            {
                return Result<Movimiento>.Failure("Error en el pago:" + resultPago._errorMessage);
            }

            //Genero el movimiento

            DateTime fechaReal = DateTime.Now;
            DateTime fechaMovimiento = fechaReal.Date;

            Decimal importe = 0;

            if (fechaMovimiento <= resultPago._value.Vencimiento)
            {
                importe = resultPago._value.Importe;
            }
            else
            {
                importe = resultPago._value.Importe2;
            }

            //Obtiene el numero de comprobante
            var numeracion = await _comprobanteService.UpdateComprobante(1);
            int numeroComprobante = numeracion._value.UltimoNumeroComprobante;

            //Creo el movimiento
            Movimiento nuevoMovimiento = new Movimiento
            {
                NumeroComprobante = numeroComprobante,
                Tipo = "Servicio",
                Numero = resultPago._value.NumeroServicio,
                FechaReal = fechaReal,
                FechaMovimiento = fechaMovimiento,
                Periodo = resultPago._value.Periodo,
                Importe = importe,
                Contrasiento = "",
                IdServicio = cliente._value.IdServicio
            };

            // Registro del movimiento
            var resultado = await _movimientoService.AddMovimiento(nuevoMovimiento);

            return resultado;

        }
        public async Task<Result<Movimiento>> ReversarPagoServicio(int numeroServicio, int periodo)
        {
            var servicio = await _servicioBoletaService.GetServicioBoletaPorNumeroServicioPeriodo(numeroServicio, periodo);

            if (!servicio._succes)
            {
                return Result<Movimiento>.Failure("Servicio Boleta no identificado");
            }

            if (!servicio._value.Pagado)
            {
                return Result<Movimiento>.Failure("Servicio impago, no se puede reversar el pago");
            }

            var movimientoOriginal = await _movimientoService.GetMovimientoByTipoNumeroPeriodo("Servicio", numeroServicio, periodo);
            if (!movimientoOriginal._succes || movimientoOriginal._value == null)
            {
                return Result<Movimiento>.Failure("Error en la identificacion del movimiento: " + movimientoOriginal._errorMessage);
            }

            // Reversar el movimiento
            servicio._value.Pagado = false;
            var reversa = await _servicioBoletaService.UpdateServicioBoleta2(servicio._value.IdBoletaServicio, servicio._value);

            if (!reversa._succes)
            {
                return Result<Movimiento>.Failure("Error en la reversa del pago en Servicio Boleta: " + reversa._errorMessage);
            }

            // Actualizar Movimiento
            movimientoOriginal._value.Contrasiento = "C";
            var actualizacionMovimiento = await _movimientoService.UpdateMovimiento(movimientoOriginal._value.IdMovimiento, movimientoOriginal._value);
            if (!actualizacionMovimiento._succes)
            {
                return Result<Movimiento>.Failure("Error en la actualizacion del movimiento Original: " + actualizacionMovimiento._errorMessage);
            }

            //Registrar NuevoMovimiento
            DateTime fecha = DateTime.Now;
            DateTime fechaMovimiento = fecha;
            Movimiento nuevoMovimiento = new Movimiento
            {
                NumeroComprobante = movimientoOriginal._value.NumeroComprobante,
                Tipo = "Servicio",
                Numero = movimientoOriginal._value.Numero,
                FechaReal = fecha,
                FechaMovimiento = fecha.Date,
                Periodo = movimientoOriginal._value.Periodo,
                Importe = movimientoOriginal._value.Importe,
                Contrasiento = "M",
                IdServicio = movimientoOriginal._value.IdServicio,
                IdTributo = movimientoOriginal._value.IdTributo
            };
            var resultado = await _movimientoService.AddMovimiento(nuevoMovimiento);

            return resultado;

        }
        public async Task<Result<Movimiento>> PagarBoleta(int numeroPadron, int periodo)
        {
            var padron = await _padronBoletaService.GetPadronBoletaByNumeroPadronNumeroPeriodo(numeroPadron, periodo);

            if (!padron._succes)
            {
                return Result<Movimiento>.Failure("Padron Boleta no identificado");
            }

            var contribuyente = await _padronContribuyenteService.GetPadronContribuyentePadronByNumeroPadron(padron._value.NumeroPadron);
            if (!contribuyente._succes)
            {
                return Result<Movimiento>.Failure("Contribuyente no identificado");
            }

            // Pago

            var resultPago = await _padronBoletaService.PagoPadronBoleta(padron._value.IdBoleta);

            if (!resultPago._succes)
            {
                return Result<Movimiento>.Failure("Error en el pago:" + resultPago._errorMessage);
            }

            //Genero el movimiento

            DateTime fechaReal = DateTime.Now;
            DateTime fechaMovimiento = fechaReal.Date;

            Decimal importe = 0;

            if (fechaMovimiento <= resultPago._value.Vencimiento)
            {
                importe = resultPago._value.Importe;
            }
            else
            {
                importe = resultPago._value.Importe2;
            }

            //Obtiene el numero de comprobante
            var numeracion = await _comprobanteService.UpdateComprobante(1);
            int numeroComprobante = numeracion._value.UltimoNumeroComprobante;

            //Creo el movimiento
            Movimiento nuevoMovimiento = new Movimiento
            {
                NumeroComprobante = numeroComprobante,
                Tipo = "Tributo",
                Numero = resultPago._value.NumeroPadron,
                FechaReal = fechaReal,
                FechaMovimiento = fechaMovimiento,
                Periodo = resultPago._value.Periodo,
                Importe = importe,
                Contrasiento = "",
                IdTributo = contribuyente._value.IdTributoMunicipal
            };

            // Registro del movimiento
            var resultado = await _movimientoService.AddMovimiento(nuevoMovimiento);

            return resultado;

        }
        public async Task<Result<Movimiento>> ReversarPagoBoleta(int numeroPadron, int periodo)
        {
            var padron = await _padronBoletaService.GetPadronBoletaByNumeroPadronNumeroPeriodo(numeroPadron, periodo);

            if (!padron._succes)
            {
                return Result<Movimiento>.Failure("Padron Boleta no identificado");
            }

            if (!padron._value.Pagado)
            {
                return Result<Movimiento>.Failure("Boleta impaga, no se puede reversar el pago");
            }

            var movimientoOriginal = await _movimientoService.GetMovimientoByTipoNumeroPeriodo("Tributo",numeroPadron,periodo);
            if (!movimientoOriginal._succes || movimientoOriginal._value==null)
            {
                return Result<Movimiento>.Failure("Error en la identificacion del movimiento: " + movimientoOriginal._errorMessage);
            }

            // Reversar el movimiento
            padron._value.Pagado = false;
            var reversa = await _padronBoletaService.UpdatePadronBoleta(padron._value.IdBoleta, padron._value);

            if (!reversa._succes)
            {
                return Result<Movimiento>.Failure("Error en la reversa del pago en padron boleta: " +  reversa._errorMessage);
            }

            // Actualizar Movimiento
            movimientoOriginal._value.Contrasiento = "C";
            var actualizacionMovimiento = await _movimientoService.UpdateMovimiento(movimientoOriginal._value.IdMovimiento, movimientoOriginal._value);
            if (!actualizacionMovimiento._succes)
            {
                return Result<Movimiento>.Failure("Error en la actualizacion del movimiento Original: " + actualizacionMovimiento._errorMessage);
            }

            //Registrar NuevoMovimiento
            DateTime fecha = DateTime.Now;
            DateTime fechaMovimiento = fecha;
            Movimiento nuevoMovimiento = new Movimiento
            {
                NumeroComprobante = movimientoOriginal._value.NumeroComprobante,
                Tipo = "Tributo",
                Numero = movimientoOriginal._value.Numero,
                FechaReal = fecha,
                FechaMovimiento = fecha.Date,
                Periodo = movimientoOriginal._value.Periodo,
                Importe = movimientoOriginal._value.Importe,
                Contrasiento = "M",
                IdServicio = movimientoOriginal._value.IdServicio,
                IdTributo = movimientoOriginal._value.IdTributo
            };
            var resultado = await _movimientoService.AddMovimiento(nuevoMovimiento);

            return resultado;

        }
        public async Task<Result<Movimiento>> GetMovimiento(int idMovimiento)
        {
            var resultado = await _movimientoService.GetMovimientoById(idMovimiento);
            return resultado;
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetMovimientoByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _movimientoService.GetAllMovimientoByTipoId(tipo, id, fecha);
            return resultado;
        }
        public async Task<Result<IEnumerable<Movimiento>>> GetMovimientoTotalByTipoId(string tipo, int id, DateTime fecha)
        {
            var resultado = await _movimientoService.GetAllMovimientoTotalByTipoId(tipo, id, fecha);
            return resultado;
        }
    }
}
