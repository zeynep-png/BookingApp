using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Types
{
    // Class representing a service message for operations that do not return data
    public class ServiceMessage
    {
        // Indicates whether the operation succeeded
        public bool IsSucceed { get; set; }

        // Contains a message related to the operation's success or failure
        public string Message { get; set; }
    }

    // Generic class representing a service message that can also return data of type T
    public class ServiceMessage<T>
    {
        // Indicates whether the operation succeeded
        public bool IsSucceed { get; set; }

        // Contains a message related to the operation's success or failure
        public string Message { get; set; }

        // Holds the data returned from the operation, can be null if there is no data
        public T? Data { get; set; }
    }
}