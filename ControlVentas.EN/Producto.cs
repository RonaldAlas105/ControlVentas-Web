using System;

namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Producto en la base de datos SQL
     /// </summary>
    public class Producto
    {


        public int Id { get; set; }
        public byte IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] Img { get; set; }
        public byte Estado { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreMarca { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime FechaHoraIngreso { get; set; }
        public int Cantidad { get; set; }



        //Propiedades Virtuales
        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }

        public virtual Inventario Inventario { get; set; }
    }
}
