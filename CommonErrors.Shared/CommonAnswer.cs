using System;

namespace CommonErrorsKata.Shared
{
    public class CommonAnswer : IGradable
    {
        public decimal Grade { get; set; }
    }

    public class InvalidGradeException : Exception
    {
        
    }
}
