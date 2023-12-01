using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class ResponseApi<T> 
    {
        public bool Succes {  get; set; }
        public T? Value { get; set; }
        public string? Message { get; set; }
    }
}
