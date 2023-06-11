﻿using System;

namespace GameStore.BLL.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException()
        {
        }

        public KeyNotFoundException(string message)
            : base(message)
        {
        }

        public KeyNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
