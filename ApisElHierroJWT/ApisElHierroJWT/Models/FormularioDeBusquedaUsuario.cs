namespace ApisElHierroJWT.Models
{
    public class FormularioDeBusquedaUsuario
    {
        public int IDUsuarios { get; set; } = 0;

        public string NombreUsuario { get; set; } = "";

        public string Contra { get; set; } = "";

        public string Email { get; set; } = "";

        public int Rol { get; set; } = 0;
    }
}
