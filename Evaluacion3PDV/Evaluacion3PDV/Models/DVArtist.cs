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
        public long ContentId { get; set; } // Cambiado a long
        public string? ArtistName { get; set; }
        public string? Url { get; set; }
        public string? LastNameFirst { get; set; }
        public string? BirthDayAsString { get; set; }
        public string? DeathDayAsString { get; set; }
        public string? Image { get; set; }
        public string? WikipediaUrl { get; set; }
    }
}
