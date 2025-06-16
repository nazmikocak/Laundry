﻿namespace LaundrySystem.Services.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) 
        { 
        }
    }
}