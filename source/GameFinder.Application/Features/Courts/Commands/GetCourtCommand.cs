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
    public record GetCourtCommand(int courtId) : IRequest<Court>;
    public class GetCourtCommandCommandHandler : IRequestHandler<GetCourtCommand, Court>
    {
        private readonly ICourtRepository _courtRepository;

        public GetCourtCommandCommandHandler(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<Court> Handle(GetCourtCommand request, CancellationToken cancellationToken)
            => await _courtRepository.GetCourtById(request.courtId);
        
    }
}
