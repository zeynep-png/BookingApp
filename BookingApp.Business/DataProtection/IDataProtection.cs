using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.DataProtection
{
    // Interface defining data protection methods for encryption and decryption
    public interface IDataProtection
    {
        // Method to encrypt a given plain text string
        string Protect(string text);

        // Method to decrypt a given protected (encrypted) string
        string UnProtect(string protectedText);
    }
}
