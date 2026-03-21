using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Communication.Response
{
    public class ResponseGameJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ResponseCategoryJson? Category { get; set; }

        public ResponsePlatformJson? Platform { get; set; }


    }
}
