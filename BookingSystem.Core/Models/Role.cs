using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}
