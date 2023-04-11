namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Usuario en la base de datos SQL
     /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public byte IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public byte Estado { get; set; }

        //Propiedades Virtuales
        public virtual Rol Rol { get; set; }

    }
}
