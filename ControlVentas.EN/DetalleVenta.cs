namespace ControlVentas.EN
{   /// <summary>
     /// Representa los datos de la tabla DetaVenta en la base de datos SQL
     /// </summary>
    public class DetalleVenta
    {
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public byte Estado { get; set; }

        // Propiedades virtuales

        /// <summary>
        /// Propiedad virutal de la tabla DetalleVenta
        /// </summary>
        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
