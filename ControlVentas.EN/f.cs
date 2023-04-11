using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlVentas.EN
{
   public class DetalleVenta
    {
        public long id { get; set; }
        public long CodigoFactura  { get; set; }
        public int Idproducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public byte Estado { get; set; }
        //Propiedades Virtuales
        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
