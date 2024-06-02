using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Common.Dto
{
    public class ResultDto
    {
        public bool IsSeccess { get; set; }
        public string Message { get; set; }

    }


    public class ResultDto<T>
    {
        public bool IsSeccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
