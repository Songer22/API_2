using Microsoft.AspNetCore.Mvc;
using ProyectoMiApiRest.Emtities;
using ProyectoMiApiRest.Repository;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoMiApiRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        /*private readonly ILogger<PersonaController> _logger;*/

        private RepPersona repPersona;
        private object resultado;

        /* public PersonaController(ILogger<PersonaController> logger)
         {
             _logger = logger;
         }
        */

        public class array
        {
            public string? status { get; set; }

            public string? date { get; set; }

            public string? message { get; set; }
            public object data { get; set; }


        }



        public PersonaController()
        {
            repPersona = new RepPersona();
        }

        [HttpGet("Mostrarpersona")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModPersona))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult MostrarPersona()
        {
            List<ModPersona>? modPerson = new();
            modPerson = repPersona.MostrarPersona();

            if (modPerson != null)
            {

                DateTime fecha = DateTime.Now;
                string dateFormatted = fecha.ToString("yyyy-MM-dd");

                var array = new array
                {
                    status = "200",
                    date = dateFormatted,
                    //date = DateTime.Now.ToString("dd-mm-yyyy"),
                    message = "Estos son los datos de la base de datos",
                    data = modPerson

                };

                //string jsonString = JsonSerializer.Serialize(array);

                return Ok(array);
                //return "status";
            }
            else
            {
                return BadRequest("No se encontraron datos");
            }
        }
    }
}