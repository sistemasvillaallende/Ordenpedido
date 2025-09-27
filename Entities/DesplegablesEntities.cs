namespace Entities
{
    public class Secretaria
    {
        public int id_secretaria { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public bool activa { get; set; }
        public System.DateTime fecha_creacion { get; set; }
    }

    public class Direccion
    {
        public int id_direccion { get; set; }
        public string descripcion { get; set; }
        public string nro_cta { get; set; }
    }
}
