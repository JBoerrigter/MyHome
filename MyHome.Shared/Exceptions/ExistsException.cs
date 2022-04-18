using System;

namespace MyHome.Shared.Exceptions
{
    public class ExistsException : Exception
    {
        public ExistsException(string type)
            : base($"{type} already exists") { }
    }
}
