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
        public IActionResult GetAnimals()
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Animals";

            var reader = command.ExecuteReader();

            List<Animal> animals = new List<Animal>();
            while (reader.Read())
            {
                animals.Add(new Animal()
                {
                    IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Desctiprion = reader.GetString(reader.GetOrdinal("Description")),
                    Category = reader.GetString(reader.GetOrdinal("Category")),
                    Area = reader.GetString(reader.GetOrdinal("Area")),
                });
            }
            
            return Ok(animals);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] AddAnimal addAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Animals (Name, Description) VALUES (@animalName, @description)";
            command.Parameters.AddWithValue("@animalName", addAnimal.Name);
            command.Parameters.AddWithValue("@description", addAnimal.Description ?? string.Empty);
            command.ExecuteNonQuery();

            return Created("", null);
        }

        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, [FromBody] AddAnimal updatedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Animals SET Name = @animalName, Description = @description WHERE IdAnimal = @idAnimal";
            command.Parameters.AddWithValue("@idAnimal", idAnimal);
            command.Parameters.AddWithValue("@animalName", updatedAnimal.Name);
            command.Parameters.AddWithValue("@description", updatedAnimal.Description ?? string.Empty);
            command.ExecuteNonQuery();
    
            return Ok();
        }
        
        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Animals WHERE IdAnimal = @idAnimal";
            command.Parameters.AddWithValue("@idAnimal", idAnimal);
            command.ExecuteNonQuery();
    
            return NoContent();
        }
    }
        