using ChirpAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Model.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int ChirpId { get; set; }

        public int? ParentId { get; set; }

        public string Text { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        //public virtual Chirp Chirp { get; set; } = null!;

        //public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

        //public virtual Comment? Parent { get; set; }
    }
}
