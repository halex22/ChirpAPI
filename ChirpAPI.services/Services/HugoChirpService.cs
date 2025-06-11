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

        

        public Task<ChirpModel> CreateChirp(ChirpModel chirp)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChirpModel>> GetAllChirps()
        {
             return await _context.Chirps.Select(x => new ChirpModel
            {
                Id = x.Id,
                Text = x.Text,
                ExtUrl = x.ExtUrl,
                Lat = x.Lat,
                Lng = x.Lng,

            }).ToListAsync();
        }

        public async Task<ChirpModel?> GetChirpById(int id)
        {
            var result = await _context.Chirps.FindAsync(id);
            return result == null ? null : ConvertChirp(result);
        }

        public async Task<List<ChirpModel>> GetChirpsByFilter(ChirpFilter filter)
        {
            IQueryable<Chirp> query = _context.Chirps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);
                // return await _context.Chirps.ToListAsync();
            }
            var result = await query.Select(x => ConvertChirp(x)).ToListAsync();

            return result;

        }

        async Task PutChirp(int id, Chirp chirp)
        {
            _context.Entry(chirp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChirpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ChirpExists(int chirpId) => _context.Chirps.Any(c => c.Id == chirpId);

        private ChirpModel ConvertChirp(Chirp rawChirp)
        {
            return new ChirpModel
            {
                Id = rawChirp.Id,
                Text = rawChirp.Text,
                ExtUrl = rawChirp.ExtUrl,
                Lat = rawChirp.Lat,
                Lng = rawChirp.Lng,
            };
        }
    }
}
