using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ApiException: Exception
    {
        //Constructor (metodo inicializador)
        //Anulando el constructor predeteminado vacio => public clase(){}
        public ApiException(string message) : base(message) { }
    }
}
