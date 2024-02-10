using GameFinder.Application.Models;
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
    public record CreateCourtCommand(NewCourtDto newCourtDto) : IRequest<int>;


    public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, int>
    {
        private readonly ICourtRepository _courtRepository;

        public CreateCourtCommandHandler(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<int> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            var address = Address.New(
                request.newCourtDto.City,
                request.newCourtDto.Street,
                request.newCourtDto.PostalCode);

            var newCourt = Court.New(request.newCourtDto.CourtType, address);


            return (await _courtRepository.AddSingleAsync(newCourt)).CourtId;
        }
    }

}
