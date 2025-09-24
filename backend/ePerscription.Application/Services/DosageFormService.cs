using AutoMapper;
using ePerscription.Application.Interfaces;
using ePerscription.Application.DTOs;
using ePerscription.Domain.Entities;
using ePerscription.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.Services
{
    public class DosageFormService : IDosageFormService
    {
        private readonly IMapper _mapper;
        private readonly IDosageFormRepository _dosageFormRepository;
        public DosageFormService(IDosageFormRepository dosageFormRepository, IMapper mapper)
        {
            _mapper = mapper;
            _dosageFormRepository = dosageFormRepository;
        }

        public async Task AddDosageFormAsync(DosageFormDto DosageFormDto)
        {
            await _dosageFormRepository.AddAsync(_mapper.Map<DosageForm>(DosageFormDto));
            await _dosageFormRepository.SaveChangesAsync();
        }

        public async Task DeleteDosageFormAsync(int id)
        {
            var DosageForm = await _dosageFormRepository.GetByIdAsync(id);
            _dosageFormRepository.Delete(DosageForm);
            await _dosageFormRepository.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<DosageFormDto>> GetAllDosageFormsAsync()
        {
           return _mapper.Map<IEnumerable<DosageFormDto>>(await _dosageFormRepository.GetAllAsync());
        }

       

        public async Task<DosageFormDto> GetDosageFormByIdAsync(int id)
        {
          var DosageForm =  await _dosageFormRepository.GetByIdAsync(id);

            return _mapper.Map<DosageFormDto>(DosageForm);
        }

       

        public async Task UpdateDosageFormAsync(DosageFormDto DosageFormDto)
        {
            _dosageFormRepository.Update(_mapper.Map<DosageForm>(DosageFormDto));
            await _dosageFormRepository.SaveChangesAsync();
        }
    }
}
