namespace ControlVentas.EN
{
    /// <summary>
    /// Representa los datos de la tabla Cliente en la base de datos SQL
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Devuelve un tipo de datos int del campo Id de la tabla cliente
        /// </summary>
        public int Id { get; set; }

      /// <summary>
      /// Devuelve un tipo de datos string del campo NombreCompleto de la tabla cliente
      /// </summary>
        public string NombreCompleto { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos string del campo DUI de la tabla cliente
        /// </summary>
        public string DUI { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos string del campo Telefono de la tabla cliente
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos string del campo Direccion de la tabla cliente
        /// </summary>
        public string Direccion { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos byte del campo Estado de la tabla cliente
        /// </summary>
        public byte Estado { get; set; }
    }
}
