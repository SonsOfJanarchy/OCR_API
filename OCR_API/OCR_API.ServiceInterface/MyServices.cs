using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using OCR_API.ServiceModel;

namespace OCR_API.ServiceInterface
{
    public class MyServices : Service
    {
        //public object Any(Hello request)
        //{
        //    if (request == null || request.Name == null)
        //    {
        //        return new HelloResponse { Result = "You didd not submit any parameters." };
        //    }
            
        //    return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        //}

        public object Get(Hello request)
        {
            return new HelloResponse { Result = "Hello! This is the Get method!" };
        }

        public object Post(Hello request)
        {
            return new HelloResponse { Result = "Hello! This is the Post method!" };
        }

        public object Post(OCR request)
        {
            return new OCRResponse { ReturnMe = "You hit the OCR Get method!  inputValue = " + request.inputValue + ".  inputValueTwo: " + request.inputValueTwo};
        }
    }
}