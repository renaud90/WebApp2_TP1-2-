using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenementsAPI
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }
        public object Errors { get; set; }
    }
}
