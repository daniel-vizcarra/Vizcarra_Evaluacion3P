using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion3PDV.Models
{
    public class DVPainting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageUrl { get; set; }
        public int ArtistContentId { get; set; }
    }
}
