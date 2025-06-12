using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Model.DTOs
{
    public class ChirpUpdateDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string? ExtUrl { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
