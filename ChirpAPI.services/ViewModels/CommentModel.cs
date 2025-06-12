using ChirpAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.ViewModels
{
    internal class CommentModel
    {
        public int ChirpId { get; set; }

        public int? ParentId { get; set; }

        public string Text { get; set; } = null!;

        public virtual Chirp? Chirp { get; set; } = null!;

    }
}
