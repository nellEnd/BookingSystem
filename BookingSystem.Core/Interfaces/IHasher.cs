using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Interfaces
{
    public interface IHasher
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string correctHash);
        bool SlowEquals(byte[] a, byte[] b);
    }
}
