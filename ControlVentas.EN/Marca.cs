namespace ControlVentas.EN
{    /// <summary>
     /// Representa los datos de la tabla Marca en la base de datos SQL
     /// </summary>
    public class Marca
    {        /// <summary>
             /// Devuelve un tipo de datos int del campo Id de la tabla Marca
             /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Devuelve un tipo de datos string del campo Nombre de la tabla Marca
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos byte del campo Estado de la tabla Marca
        /// </summary>
        public byte Estado { get; set; }
    }
}
