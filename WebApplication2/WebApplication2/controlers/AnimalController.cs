using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebApplication2.controlers;


    [ApiController]
    [Route("api/{controller}")]
    public class AnimalController : ControllerBase {
        [HttpGet]
        public IActionResult GetAnimal()
        {
            SqlConnection connection = new SqlConnection("");
            return Ok();
        }


        }