﻿using System.ComponentModel.DataAnnotations;

namespace ServiMun.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Sintetico { get; set; }
        public bool Estado { get; set; } // True: Activo, False: Inactivo

        // Relacion inversa -  PadronCntribuyente
        public ICollection<ServicioCliente> ServicioClientes { get; set; }

        // Relación inversa - Movimientos
        public ICollection<Movimiento> Movimientos { get; set; }
    }

}
