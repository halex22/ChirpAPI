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

        

        public async Task CreateChirp(Chirp chirp)
        {
            _context.Chirps.Add(chirp);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChirp(int id)
        {
            var chirp = await _context.Chirps.FindAsync(id);
            if (chirp == null) throw new KeyNotFoundException($"Chirp with ID {id} not found.");

            _context.Chirps.Remove(chirp);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChirpModel>> GetAllChirps()
        {
             return await _context.Chirps.Select(x => ConvertChirp(x)).ToListAsync();
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
            }
            var result = await query.Select(x => ConvertChirp(x)).ToListAsync();

            return result;

        }

        public async Task UpdateChirp(int id, Chirp chirp)
        {
            if (!ChirpExists(id)) throw new KeyNotFoundException($"Chirp with ID {id} not found.");

            _context.Entry(chirp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
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
