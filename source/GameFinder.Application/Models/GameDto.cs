using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Models
{
    public class GameDto
    {

        public int GameId { get; private set; }
        public string SportName { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime PrecictedEnd { get; private set; }
        public virtual Court Court { get; private set; }
    }
}
