using System;

namespace CommonErrorsKata.Shared
{
    public class CommonAnswer : IGradable
    {
        decimal grade = 0;

        public decimal Grade
        {
            get
            {
                return grade;
            }
            set
            {
                if (value < 0 || value > 100) throw new InvalidGradeException();
                grade = value;
            }
        }
        
    }

    public class InvalidGradeException : Exception
    {
        
    }
}
