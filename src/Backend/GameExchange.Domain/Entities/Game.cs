using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Entities
{
    public class Game : EntityBase
    {
        private string Name { get; set; } = string.Empty;
        private long CategoryId { get; set; }
        public long PlataformId { get; set; }

    }
}
