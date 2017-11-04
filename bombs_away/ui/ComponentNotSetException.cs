using System;

namespace bombs_away.ui
{
    class ComponentNotSetException : Exception
    {
        public ComponentNotSetException() : base ("Component not set")
        {
        }

        public ComponentNotSetException(string message)
            : base(message)
        {
        }

        public ComponentNotSetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
