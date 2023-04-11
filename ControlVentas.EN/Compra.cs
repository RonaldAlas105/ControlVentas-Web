using System;
using System.Collections.Generic;

namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Compra en la base de datos SQL
     /// </summary>
    public class Compra
    {
        public long Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public byte Estatus { get; set; }
        public byte Estado { get; set; }

        // Propiedades virtuales

        /// <summary>
        /// Propiedad virtual de la tabla Compra
        /// </summary>
        public virtual Usuario Usuario { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        // Propiedad para almacenar los detalles de venta en memoria
        public List<DetallesCompra> Detalles { get; set; }

        // Enums
        public enum EnumEstatus
        {
            CREADO = 1,
            CERRADO = 2,
            ANULADO = 3,
        }


        // PropiedadAuxiliar
        private string estatus;
        /// <summary>
        /// Contiene los datos del enum del tipo Estatus de la clase Compra , la cual tiene tres posiciones , ANULADO, CERRADO Y CREADO.
        /// </summary>
        public string EstatusStr
        {
            get
            {
                if (Estatus == 3)
                {
                    return "ANULADO";
                }
                else if (Estatus == 2)
                {
                    return "CERRADO";
                }
                else
                {
                    return "CREADO";
                }
            }
        }

    }
}