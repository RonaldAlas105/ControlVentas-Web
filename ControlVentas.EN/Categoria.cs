namespace ControlVentas.EN
{
    /// <summary>
    /// Representa los datos de la tabla Categoria en la base de datos SQL
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Devuelve un tipo de datos int del campo Id de la tabla Categoria
        /// </summary>
        public byte Id { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos string del campo Nombre de la tabla Categoria
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Devuelve un tipo de datos byte del campo Estado de la tabla Categoria
        /// </summary>
        public byte Estado { get; set; }

    }
}
