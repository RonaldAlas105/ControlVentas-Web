namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla DetalleCompra en la base de datos SQL
     /// </summary>
    public class DetallesCompra
    {
        public long Id { get; set; }
        public int IdProducto { get; set; }
        public long IdCompra { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public byte Estado { get; set; }
        //Propiedades Virtuales
        /// <summary>
        /// Propiedad virtual de la tabla DetalleCompra
        /// </summary>
        public virtual Producto Producto { get; set; }
        public virtual Compra Compra { get; set; }
    }
}
