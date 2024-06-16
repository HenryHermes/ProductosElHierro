namespace ApisElHierroJWT.Models
{
    public class Usuarios
    {
        public int IDUsuarios { get; set; }

        public string NombreUsuario { get; set; }

        public string Contra { get; set; }

        public string Email { get; set; }

        public int Rol { get; set; }

        public bool Activo { get; set; }
    }
}
