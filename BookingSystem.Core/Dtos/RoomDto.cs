using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Dtos
{
    public class RoomDto
    {
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string? BookingString { get; set; }
    }
}
