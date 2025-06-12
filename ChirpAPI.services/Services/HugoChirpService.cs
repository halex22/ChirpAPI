using ChirpAPI.Model;
using ChirpAPI.services.Model.DTOs;
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

        

        public async Task<Chirp> CreateChirp(ChirpCreateDTO chirp)
        {
            var newChirp = new Chirp
            {
                Text = chirp.Text,
                ExtUrl = chirp.ExtUrl,
                Lat = chirp.Lat,
                Lng = chirp.Lng
            };
            _context.Chirps.Add(newChirp);
            await _context.SaveChangesAsync();
            return newChirp;
        }

        public async Task DeleteChirp(int id)
        {
            // first check 
            //Chirp? entity = await _context.Chirps
            //    .Include(chirp => chirp.Comments)
            //    .Where(comment => comment.Id == id)
            //    .SingleOrDefaultAsync();

            //// second check
            //if (entity != null && entity.Comments.Any())
            //{
            //    throw new InvalidOperationException($"Chirp with ID {id} cannot be deleted because it has associated comments.");
            //}

            Chirp? chirp = await _context.Chirps.FindAsync(id);
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
            var entity = await _context.Chirps.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Chirp with ID {id} not found.");

            if (!string.IsNullOrWhiteSpace(chirp.Text)) entity.Text = chirp.Text;
            if (!string.IsNullOrWhiteSpace(chirp.ExtUrl)) entity.ExtUrl = chirp.ExtUrl;
            if (chirp.Lat != null) entity.Lat = chirp.Lat;
            if (chirp.Lng != null) entity.Lng = chirp.Lng;

            _context.Entry(entity).State = EntityState.Modified;

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

        private static ChirpModel ConvertChirp(Chirp rawChirp) // why static ????? memory leak 
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
