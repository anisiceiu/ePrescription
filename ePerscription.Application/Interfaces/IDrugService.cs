using ePerscription.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.Interfaces
{
    public interface IDrugService
    {
        Task<IEnumerable<DrugDto>> GetAllDrugsAsync();
        Task<DrugDto> GetDrugByIdAsync(int id);
        Task AddDrugAsync(DrugDto DrugDto);
        Task UpdateDrugAsync(DrugDto DrugDto);
        Task DeleteDrugAsync(int id);
    }
}
