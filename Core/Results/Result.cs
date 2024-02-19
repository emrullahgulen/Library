using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class Result : IResult
    {
        public Result(bool IsSuccess, string message):this(IsSuccess)
        {
            Message = message;  
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get;  }

        public string Message {get; }
    }
}
