using ePerscription.Application.DTOs;
using ePerscription.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.Interfaces
{
    public interface IDosageFormService
    {
        Task<IEnumerable<DosageFormDto>> GetAllDosageFormsAsync();
        Task<DosageFormDto> GetDosageFormByIdAsync(int id);
        Task AddDosageFormAsync(DosageFormDto DosageFormDto);
        Task UpdateDosageFormAsync(DosageFormDto DosageFormDto);
        Task DeleteDosageFormAsync(int id);
    }
}
