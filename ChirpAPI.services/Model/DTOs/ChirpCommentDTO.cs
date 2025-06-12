using ChirpAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Model.DTOs
{
    public class ChirpCommentDTO
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public DateTime CreationDate { get; set; }




    }
}
