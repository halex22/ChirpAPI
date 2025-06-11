using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChirpAPI.Model;
using ChirpAPI.services.ViewModels;

namespace ChirpAPI.services.Services.Interfaces
{
    public interface IChirpsService
    {
        //Task<List<Chirp>> GetAllChirps();
        Task<List<ChirpModel>> GetChirpsByFilter(ChirpFilter filter);
        Task<List<ChirpModel>> GetAllChirps();
        Task<ChirpModel?> GetChirpById(int id);
        Task<ChirpModel> CreateChirp(ChirpModel chirp);
        
        //Task<bool> UpdateChirpAsync(Chirp chirp);
        //Task<bool> DeleteChirpAsync(int id);
    }
}
