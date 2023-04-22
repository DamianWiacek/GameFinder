﻿using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Games.Commands
{
    public record GetAllGamesFromCourtCommand(int courtId) : IRequest<List<Game>>;
    public class GetAllGamesFromCourtCommandHandler : IRequestHandler<GetAllGamesFromCourtCommand, List<Game>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesFromCourtCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<Game>> Handle(GetAllGamesFromCourtCommand request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetAllGamesFromCourt(request.courtId);
            if (games == null) throw new ArgumentNullException("There is no games on this court!");
            return games;
        }
    }
}
