using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Courts.Commands
{
    public record GetAllCourtsCommand : IRequest<List<Court>>;
    public class GetAllCourtsCommandHandler : IRequestHandler<GetAllCourtsCommand, List<Court>>
    {
        private readonly ICourtRepository _courtRepository;

        public GetAllCourtsCommandHandler(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<List<Court>> Handle(GetAllCourtsCommand request, CancellationToken cancellationToken) 
          => (await _courtRepository.GetAllAsync()).ToList();
        
    }
}
