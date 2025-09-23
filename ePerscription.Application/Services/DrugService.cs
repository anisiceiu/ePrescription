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
    public class DrugService : IDrugService
    {
        private readonly IMapper _mapper;
        private readonly IDrugRepository  _drugRepository;
        public DrugService(IDrugRepository drugRepository,IMapper mapper)
        {
            _mapper = mapper;
            _drugRepository = drugRepository;
        }

        public async Task AddDrugAsync(DrugDto DrugDto)
        {
            await _drugRepository.AddAsync(_mapper.Map<Drug>(DrugDto));
            await _drugRepository.SaveChangesAsync();
        }

        public async Task DeleteDrugAsync(int id)
        {
            var drug = await _drugRepository.GetByIdAsync(id);
            _drugRepository.Delete(drug);
            await _drugRepository.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<DrugDto>> GetAllDrugsAsync()
        {
           return _mapper.Map<IEnumerable<DrugDto>>(await _drugRepository.GetAllAsync());
        }

       

        public async Task<DrugDto> GetDrugByIdAsync(int id)
        {
          var drug =  await _drugRepository.GetByIdAsync(id);

            return _mapper.Map<DrugDto>(drug);
        }

       

        public async Task UpdateDrugAsync(DrugDto DrugDto)
        {
            _drugRepository.Update(_mapper.Map<Drug>(DrugDto));
            await _drugRepository.SaveChangesAsync();
        }
    }
}
