using ApisElHierroJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApisElHierroJWT.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("TraerUsers")]
        public dynamic TraerUser(FormularioDeBusquedaUsuario form)
        {
            bool succeess = false;
            string message = "Error";
            List<Usuarios> usuarios = new List<Usuarios>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("GetUsuarios", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDUsuarios", form.IDUsuarios);
            cmd.Parameters.AddWithValue("@NombreUsuario", form.NombreUsuario);
            cmd.Parameters.AddWithValue("@Contra", form.Contra);
            cmd.Parameters.AddWithValue("@Email", form.Email);
            cmd.Parameters.AddWithValue("@Rol", form.Rol);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            try
            {
                succeess = true;
                message = "OK";
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
                message = message + " " + e.Message;
            }

            sqlConnection.Close();

            return new
            {
                success = succeess,
                message = message,
                result =  usuarios
                
            };

        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("InsertarUsers")]
        public dynamic InsertarUsers(Usuarios form)
        {
            bool succeess = false;
            string message = "Error";
            bool BuenNombre = true;
            bool BuenEmail = false;
            bool BuenaContra = false;
            int Mayusculas = 0;
            int Minusculas = 0;
            int numeros = 0;
            int caracteresEspeciales = 0;
            for (int i = 0; i < form.NombreUsuario.Length; i++)
            {
                if (form.NombreUsuario[i] == '@')
                {
                    BuenNombre = false;
                }
            }
            for (int i = 0; i < form.Email.Length; i++)
            {
                if (form.Email[i] == '@')
                {
                    BuenEmail = true;
                }
            }
            for (int i = 0; i < form.Contra.Length; i++)
            {
                if (form.Contra[i] > 64 && form.Contra[i] < 91)
                {
                    Mayusculas++;
                }
                else if (form.Contra[i] > 96 && form.Contra[i] < 123)
                {
                    Minusculas++;
                }
                else if (form.Contra[i] > 47 && form.Contra[i] < 58)
                {
                    numeros++;
                }
                else if (form.Contra[i] > 32 && form.Contra[i] < 128)
                {
                    caracteresEspeciales++;
                }

                if (Mayusculas > 0 && Minusculas > 0 && numeros > 0 && caracteresEspeciales > 0)
                {
                    BuenaContra = true;
                }
            }
            if (BuenaContra == true && BuenEmail == true && BuenNombre == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("InsertUsuarios", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUsuarios", form.IDUsuarios);
                    cmd.Parameters.AddWithValue("@NombreUsuario", form.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Contra", BCrypt.Net.BCrypt.HashPassword(form.Contra));
                    cmd.Parameters.AddWithValue("@Email", form.Email);
                    cmd.Parameters.AddWithValue("@Rol", form.Rol);
                    cmd.ExecuteNonQuery();
                    cmdTransaction.Commit();
                    message = "OK";
                    succeess = true;
                }
                catch (Exception e)
                {
                    cmdTransaction.Rollback();
                    message = message + " " + e.Message;
                }

                sqlConnection.Close();
            }


            return new
            {
                success = succeess,
                message = message,
                result = ""
            };

        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("UpdateUser")]
        public dynamic UpdateUser(Usuarios form)
        {
            bool succeess = false;
            string message = "Error";
            bool BuenNombre = true;
            bool BuenEmail = false;
            bool BuenaContra = false;
            int Mayusculas = 0;
            int Minusculas = 0;
            int numeros = 0;
            int caracteresEspeciales = 0;
            for (int i = 0; i < form.NombreUsuario.Length; i++)
            {
                if (form.NombreUsuario[i] == '@')
                {
                    BuenNombre = false;
                }
            }
            for (int i = 0; i < form.Email.Length; i++)
            {
                if (form.Email[i] == '@')
                {
                    BuenEmail = true;
                }
            }
            for (int i = 0; i < form.Contra.Length; i++)
            {
                if (form.Contra[i] > 64 && form.Contra[i] < 91)
                {
                    Mayusculas++;
                }
                else if (form.Contra[i] > 96 && form.Contra[i] < 123)
                {
                    Minusculas++;
                }
                else if (form.Contra[i] > 47 && form.Contra[i] < 58)
                {
                    numeros++;
                }
                else if (form.Contra[i] > 32 && form.Contra[i] < 128)
                {
                    caracteresEspeciales++;
                }

                if (Mayusculas > 0 && Minusculas > 0 && numeros > 0 && caracteresEspeciales > 0)
                {
                    BuenaContra = true;
                }
            }
            if (BuenaContra == true && BuenEmail == true && BuenNombre == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UpdateUsuarios", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUsuarios", form.IDUsuarios);
                    cmd.Parameters.AddWithValue("@NombreUsuario", form.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Contra", BCrypt.Net.BCrypt.HashPassword(form.Contra));
                    cmd.Parameters.AddWithValue("@Email", form.Email);
                    cmd.Parameters.AddWithValue("@Rol", form.Rol);
                    cmd.ExecuteNonQuery();
                    cmdTransaction.Commit();
                    message = "OK";
                    succeess = true;
                }
                catch (Exception e)
                {
                    cmdTransaction.Rollback();
                    message = message + " " + e.Message;
                }

                sqlConnection.Close();
            }

            return new
            {
                success = succeess,
                message = message,
                result = ""
            };

        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("DeleteUser")]

        public dynamic deleteProductos(int ID)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("DeactivateUser", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDUsuarios", ID);
                cmd.ExecuteNonQuery();
                cmdTransaction.Commit();
                message = "OK";
                succeess = true;
            }
            catch (Exception e)
            {
                cmdTransaction.Rollback();
                message = message + " " + e.Message;
            }

            sqlConnection.Close();

            return new
            {
                success = succeess,
                message = message,
                result = ""
            };

        }
    }
}
