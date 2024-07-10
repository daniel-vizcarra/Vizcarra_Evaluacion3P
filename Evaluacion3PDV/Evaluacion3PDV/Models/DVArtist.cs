using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion3PDV.Models
{
    public class DVArtist
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDay { get; set; }
        public string DeathDay { get; set; }
        public string ImageUrl { get; set; }
        public string WikipediaUrl { get; set; }
    }
}
