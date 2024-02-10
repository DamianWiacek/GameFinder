using GameFinder.Domain.Exceptions;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Courts.Commands
{
    public record DeleteCourtCommand(int courtId) : IRequest<bool>;

    public class DeleteCourtCommandHandler : IRequestHandler<DeleteCourtCommand, bool>
    {
        private readonly ICourtRepository _courtRepository;

        public DeleteCourtCommandHandler(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }
        public async Task<bool> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            var courtToDelete = await _courtRepository.GetCourtById(request.courtId);
            if (courtToDelete == null) 
                throw new ArgumentNullException("There is no court with given id!");
            if (courtToDelete.HasGames()) 
                throw new GamesOnCourtException();
            var result = await _courtRepository.DeleteSingleAsync(courtToDelete);
            return result;
        }
    }
}
