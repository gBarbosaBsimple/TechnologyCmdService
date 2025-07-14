using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.IServices
{
    public interface ITechnologyService
    {
        Task<Result<TechnologyDTO>> AddTechnologyAsync(AddTechnologyDTO technologyDto);
        Task SubmitAsync(string description);
    }
}