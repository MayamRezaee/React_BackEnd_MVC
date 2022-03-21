using System.Collections.Generic;
using System.Linq;
using DataBaseEntities.Data;
using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataBaseEntities.Controllers
{
    public class ApiController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("api/person")]
        public IActionResult AddPerson([FromBody] PersonCreationViewModel person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            City city = _context.Cities.Where(a => a.CityName == person.CityName).Include(a => a.People).FirstOrDefault();

            IEnumerable<Language> languages = _context.Languages.Where(b => person.LanguageName.Contains(b.LanguageName));
            Country country = _context.Countries.FirstOrDefault(k => k.CountryName == person.CountryName);


            Person p = new Person
            {
                Name = person.FullName,
                PhoneNumber = person.PhoneNum,
                City = city,
                Country = country
            };

            _context.People.Add(p);
            _context.SaveChanges();

            Person insertedPerson = _context.People.FirstOrDefault(a => a.Name == p.Name);

            List<LanguagePerson> languagePerson = new List<LanguagePerson>();
            foreach (var l in languages)
            {
                languagePerson.Add(new LanguagePerson()
                {
                    Language = l,
                    Person = insertedPerson
                });
            }

            _context.LanguagePerson.AddRange(languagePerson);
            city.People.Add(p);

            _context.Cities.Update(city);
            _context.SaveChanges();


            return Ok("Created successfully!");
        }


        [HttpGet("api/person/all")]
        public IActionResult ListPeople()
        {
            var cityLanguageCountry = _context.People
                .Include(a => a.City)
                .Include(k => k.Country)
                .Include(a => a.LanguagePerson)
                .ToList();


            return Ok(cityLanguageCountry);
        }


        [HttpDelete("api/person/{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            var personToDelete = _context.People.Find(id);
            _context.People.Remove(personToDelete);
            _context.SaveChanges();

            return Ok("deleted successfully");
        }

        [HttpGet("api/country/cities/{countryName}")]
        public IActionResult GetCitisByCountryName([FromRoute] string countryName)
        {
            Country chosenCountry = _context.Countries.Where(a => a.CountryName == countryName).Include(b => b.Cities).FirstOrDefault();
            if (chosenCountry == null) return NoContent(); //status : 202
            IEnumerable<string> cityNames = chosenCountry.Cities.Select(a => a.CityName);
            return Ok(cityNames);
        }

        [HttpGet("api/country")]
        public IActionResult GetCountries()
        {
            var country = _context.Countries.ToList();
            return Ok(country);
        }


        [HttpGet("api/langs")]
        public IActionResult GetLangs()
        {
            var langs = _context.Languages.Select(a => new LanguageViewModel() { Name = a.LanguageName }).ToList();


            return Ok(langs);
        }


    }
}
