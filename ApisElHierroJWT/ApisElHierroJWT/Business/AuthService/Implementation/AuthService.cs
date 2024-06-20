using ApisElHierroJWT.Models;
using ApisElHierroJWT.Business.AuthService.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;

namespace ApisElHierroJWT.Business.AuthService.Implementation
{
    using BCrypt.Net;

    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration _configuration;
        public AuthService( IConfiguration configuration)
        {
            
            _configuration = configuration;
        }
        public List<Usuarios> TraerUser(string form)
        {
           
            List<Usuarios> usuarios = new List<Usuarios>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("GetUsuarios", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreUsuario", form);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            try
            {
                
                while (sqlDataReader.Read())
                {
                    Usuarios usuario = new Usuarios
                    {
                        IDUsuarios = int.Parse(sqlDataReader["IDUsuarios"].ToString()),
                        NombreUsuario = sqlDataReader["NombreUsuario"].ToString(),
                        Contra = sqlDataReader["Contra"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        Rol = int.Parse(sqlDataReader["Rol"].ToString()),
                        Activo = bool.Parse(sqlDataReader["Activo"].ToString())
                    };

                    usuarios.Add(usuario);
                }
            }
            catch (Exception e)
            {
                
            }

            sqlConnection.Close();

            return usuarios;

        }

        public async Task<TokenReturn> Login(string email, string password)
        {

            TokenReturn tokenReturn = null;
            Usuarios user = null;
            List<Usuarios> usuarios = TraerUser(email);
            bool verified = false;

            foreach (Usuarios usuario in usuarios)
            {
                if (BCrypt.Verify(password, usuario.Contra))
                {
                    verified = true;
                    user = usuario;
                    tokenReturn = new TokenReturn();
                    tokenReturn.rol = usuario.Rol;
                }
            }

            if (!verified)
            {
                return tokenReturn;
            }
            

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NombreUsuario),
                    new Claim(ClaimTypes.Role, user.Rol.ToString())
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenReturn.token = tokenHandler.WriteToken(token);

            return tokenReturn;
        }

       
    }
}
