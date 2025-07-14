using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using AutoMapper;
using Application.IPublisher;


namespace Application.Services
{
    public class TechnologyService : ITechnologyService
    {
        private ITechnologyFactory _technologyFactory;
        private ITechnologyRepositoryEF _technologyRepository;
        private readonly IMessagePublisher _publisher;
        private readonly IMapper _mapper;
        //priate readonly IMessagePublisher _publisher;

        public TechnologyService(ITechnologyFactory technologyFactory, ITechnologyRepositoryEF technologyRepository, IMapper mapper, IMessagePublisher publisher)
        {
            _technologyFactory = technologyFactory;
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            //this._publisher = publisher;
        }
        public async Task<Result<TechnologyDTO>> AddTechnologyAsync(AddTechnologyDTO technologyDTO)
        {
            Technology t;
            try
            {
                t = _technologyFactory.CreateAsync(technologyDTO.Description).Result;
                await _technologyRepository.AddAsync(t);
                await _publisher.PublishTechnologyCreatedAsync(t.Id, t.Description);

            }
            catch (ArgumentException a)
            {
                return Result<TechnologyDTO>.Failure(Error.BadRequest(a.Message));
            }
            catch (Exception e)
            {
                return Result<TechnologyDTO>.Failure(Error.BadRequest(e.Message));
            }
            var result = _mapper.Map<Technology, TechnologyDTO>(t);
            return Result<TechnologyDTO>.Success(result);
        }
        public async Task SubmitAsync(string description)
        {
            var existingTechnology = await _technologyRepository.IsRepeated(description);
            if (existingTechnology)
            {
                throw new ArgumentException("Já existe uma tecnologia com este ID.");
            }
            var technology = await _technologyFactory.CreateAsync(description);
            await _technologyRepository.AddAsync(technology);
            await _technologyRepository.SaveChangesAsync();
        }
    }
}