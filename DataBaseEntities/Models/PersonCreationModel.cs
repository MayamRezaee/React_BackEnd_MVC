using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models
{
    public class PersonCreationModel
    {
       
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CityName { get; set; }
        public int PhoneNum { get; set; }

        public string[] LanguageName { get; set; }

        public string CountryName { get; set; }

    }
}