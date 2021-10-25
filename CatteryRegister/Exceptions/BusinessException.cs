using System;

namespace CatteryRegister.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string code) : base(code)
        {
            
        }
        public string Code { get; set; }
    }
}