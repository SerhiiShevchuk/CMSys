using System;

namespace CMSys.Common
{
    public static class Error
    {
        public static ArgumentException Argument()
        {
            return new ArgumentException();
        }

        public static ArgumentException Argument(string message)
        {
            return new ArgumentException(message);
        }

        public static ArgumentException Argument(string paramName, string message)
        {
            return new ArgumentException(message, paramName);
        }

        public static ArgumentNullException ArgumentNull()
        {
            return new ArgumentNullException();
        }

        public static ArgumentNullException ArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }
    }
}