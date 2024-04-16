using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication2.models;
using WebApplication2.models.DTO;

namespace WebApplication2.controlers;


    [ApiController]
    [Route("api/{controller}")]
    public class AnimalController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnimalController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAnimal()
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Animals";

            var reader = command.ExecuteReader();

            List<Animal> animals = new List<Animal>();
            int IdAnimalOrdinal = reader.GetOrdinal("IdAnimal");
            int NameOrdinal = reader.GetOrdinal("Name");
            int DesctiprionOrdinal = reader.GetOrdinal("Desctiprion");
            int CategoryOrdinal = reader.GetOrdinal("Category");
            int AreaOrdinal = reader.GetOrdinal("Area");

            while (reader.Read())
            {
                animals.Add(new Animal()
                {
                    IdAnimal = reader.GetInt32(0),
                    Name = reader.GetString(0),
                    Desctiprion = reader.GetString(0),
                    Category = reader.GetString(0),
                    Area = reader.GetString(0),
                });
                
            }
            
            return Ok();
        }

        [HttpPost]
        public IActionResult AddAnimal(AddAnimal addAnimal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Animals VALUES (@animalName,'','','' )";
            command.Parameters.AddWithValue("@animalName", addAnimal.Name);
            command.ExecuteNonQuery();
            return Created("", null);
        }


        }
        