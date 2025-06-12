using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Model;
using ChirpAPI.services.Model.DTOs;
using ChirpAPI.services.ViewModels;

namespace ChirpAPI.services.Services.Interfaces
{
    public interface IChirpsService
    {
        //Task<List<Chirp>> GetAllChirps();
        Task<List<ChirpModel>> GetChirpsByFilter(ChirpFilter filter);
        Task<List<ChirpModel>> GetAllChirps();
        Task<ChirpModel?> GetChirpById(int id);
        Task<Chirp> CreateChirp(ChirpCreateDTO chirp);
        Task UpdateChirp(int id, Chirp chirp);
        Task DeleteChirp(int id);

        //Task<bool> UpdateChirpAsync(Chirp chirp);
        //Task<bool> DeleteChirpAsync(int id);
    }
}
