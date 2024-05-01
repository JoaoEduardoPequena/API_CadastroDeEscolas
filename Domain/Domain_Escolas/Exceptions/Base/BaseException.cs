using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Escolas.Exceptions.Base
{
    public  class BaseException: Exception
    {
        public BaseException(string title, string? message)
            : base(message)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
