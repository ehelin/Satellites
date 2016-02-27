using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Shared.dto.Satellite
{
    public class Position
    {
        [JsonProperty]
        public int X { get; set; }

        [JsonProperty]
        public int Y { get; set; }

        public Position(int pX, int pY)
        {
            X = pX;
            Y = pY;
        }
    }
}
