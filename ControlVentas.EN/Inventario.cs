using System;

namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Inventario en la base de datos SQL
     /// </summary>
    public class Inventario
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaHoraIngreso { get; set; }
        public int Cantidad { get; set; }
        public byte Estado { get; set; }
       
        
        //Propiedades Virtuales
        /// <summary>
        /// Propiedad virtual de la tabla Inventario
        /// </summary>
        public virtual Producto Producto { get; set; }
    }
}
