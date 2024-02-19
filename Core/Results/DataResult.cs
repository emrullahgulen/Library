using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(T data, bool IsSuccess,string message):base(IsSuccess,message) 
        {
            Data = data;
        }

        public DataResult(T data, bool IsSuccess) : base(IsSuccess)
        {
            Data = data;
        }


    }
}
