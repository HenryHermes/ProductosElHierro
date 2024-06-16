using ApisElHierroJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApisElHierroJWT.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Products")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        [HttpPost]
        [Route("TraerProductos")]

        public dynamic TraerProductos(FormularioBusquedaProducto form)
        {
            bool succeess = false;
            string message = "Error";

            List<Productos> Productos = new List<Productos>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("GetProductos", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDProducto", form.IDProducto);
            cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", form.Descripcion);
            cmd.Parameters.AddWithValue("@Precio1", form.Precio1);
            cmd.Parameters.AddWithValue("@Precio2", form.Precio2);
            cmd.Parameters.AddWithValue("@Stock1", form.Stock1);
            cmd.Parameters.AddWithValue("@Stock2", form.Stock2);
            cmd.Parameters.AddWithValue("@FechaDeCreacion1", form.FechaDeCreacion1);
            cmd.Parameters.AddWithValue("@FechaDeAct1", form.FechaDeAct1);
            cmd.Parameters.AddWithValue("@FechaDeCreacion2", form.FechaDeCreacion2);
            cmd.Parameters.AddWithValue("@FechaDeAct2", form.FechaDeAct2);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            try
            {
                succeess = true;
                message = "OK";
                while (sqlDataReader.Read())
                {
                    Productos producto = new Productos
                    {
                        IDProducto = int.Parse(sqlDataReader["IDProducto"].ToString()),
                        Nombre = sqlDataReader["Nombre"].ToString(),
                        Descripcion = sqlDataReader["Descripcion"].ToString(),
                        Precio = double.Parse(sqlDataReader["Precio"].ToString()),
                        Stock = int.Parse(sqlDataReader["Stock"].ToString()),
                        FechaDeCreacion = DateTime.Parse(sqlDataReader["FechaDeCreacion"].ToString()),
                        FechaDeAct = DateTime.Parse(sqlDataReader["FechaDeAct"].ToString()),
                        Activo = bool.Parse(sqlDataReader["Activo"].ToString())
                    };

                    Productos.Add(producto);
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
                result = new
                {
                    ProductosEncontrados = Productos
                }
            };

        }

        [HttpPost]
        [Route("InsertarProductos")]
        public dynamic InsertarProductos(Productos form)
        {
            bool succeess = false;
            string message = "Error";
            if (form.Nombre.Length > 0 && form.Descripcion.Length > 0 && form.Precio > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("InsertProductos", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProducto", form.IDProducto);
                    cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", form.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", form.Precio);
                    cmd.Parameters.AddWithValue("@Stock", form.Stock);
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

        [HttpPost]
        [Route("UpdateProductos")]
        public dynamic UpdateProductos(Productos form)
        {
            bool succeess = false;
            string message = "Error";
            if (form.Nombre.Length > 0 && form.Descripcion.Length > 0 && form.Precio > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UpdateProductos", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProducto", form.IDProducto);
                    cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", form.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", form.Precio);
                    cmd.Parameters.AddWithValue("@Stock", form.Stock);
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

        [HttpPost]
        [Route("DeleteProductos")]

        public dynamic deleteProductos(int ID)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("DeactivateProduct", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDProducto", ID);
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
