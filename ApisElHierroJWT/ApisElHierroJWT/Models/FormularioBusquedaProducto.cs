namespace ApisElHierroJWT.Models
{
    public class FormularioBusquedaProducto
    {
        public int IDProducto { get; set; } = 0;

        public string Nombre { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public double Precio1 { get; set; } = 0;

        public double Precio2 { get; set; } = 999999999.99;

        public int Stock1 { get; set; } = 0;

        public int Stock2 { get; set; } = 999999;

        public DateTime FechaDeCreacion1 { get; set; }

        public DateTime FechaDeAct1 { get; set; }

        public DateTime FechaDeCreacion2 { get; set; }

        public DateTime FechaDeAct2 { get; set; }
    }
}
