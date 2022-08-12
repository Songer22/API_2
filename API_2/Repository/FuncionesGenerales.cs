
using System.Data;
using System.Data.SqlClient;

namespace ProyectoMiApiRest.Repository
{
    public class FuncionesGenerales
    {
        //manejar la tabla de tipo privada
        private readonly IConfiguration _configuration;

        public FuncionesGenerales()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                _configuration = builder.Build();

        }

        //METODO PARA EJECUTAR CONSULTA
        public DataTable CargarDatos(string sp, Dictionary<string, string>? parametros= null)
        {
            using SqlConnection con = new(_configuration.GetConnectionString("conexionBD").ToString());
            con.Open();
            SqlCommand cmd = new(sp, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parametros != null)
            {
                foreach (KeyValuePair<string, string> item in parametros)
                {
                    cmd.Parameters.AddWithValue(Convert.ToString(item.Key), Convert.ToString( item.Value));
                }
            }

            DataTable dt = new();
            try
            {
                new SqlDataAdapter(cmd).Fill(dt);
            }
            catch(Exception)
            {
                throw;
            }
            con.Close();
            return dt;
        }
    }
}
