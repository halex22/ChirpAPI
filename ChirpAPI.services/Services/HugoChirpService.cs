using ChirpAPI.Model;
using ChirpAPI.services.Services.Interfaces;
using ChirpAPI.services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Services
{
    public class HugoChirpService : IChirpsService
    {
        
        private readonly ChirpContext _context;
        public HugoChirpService(ChirpContext context)
        {
            _context = context;
        }


        public async Task<List<ChirpModel>> GetChirpsByFilter(ChirpFilter filter)
        {
            IQueryable<Chirp> query = _context.Chirps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);
                // return await _context.Chirps.ToListAsync();
            }
            var result = await query.Select(x => new ChirpModel
            {
                Id = x.Id,
                Text = x.Text,
                ExtUrl = x.ExtUrl,
                Lat = x.Lat,
                Lng = x.Lng,

            }).ToListAsync();

            return result;

        }
    }
}
