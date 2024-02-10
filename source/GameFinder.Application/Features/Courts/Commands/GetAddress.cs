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
    public record GetAddressCommand(int courtId) : IRequest<Address>;
    public class GetAddressCommandHandler : IRequestHandler<GetAddressCommand, Address>
    {
        private readonly ICourtRepository _courtRepository;

        public GetAddressCommandHandler(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<Address> Handle(GetAddressCommand request, CancellationToken cancellationToken)
            => (await _courtRepository.GetCourtById(request.courtId)).Address;
            
        
    }
}
