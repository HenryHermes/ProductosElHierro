namespace ApisElHierroJWT.Models
{
    public class Productos
    {
        public int IDProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }

        public DateTime FechaDeCreacion { get; set; }

        public DateTime FechaDeAct { get; set; }

        public bool Activo { get; set; }

    }
}
