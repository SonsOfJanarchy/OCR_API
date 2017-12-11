using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack;

namespace OCR_API.ServiceModel
{
    [Route("/OCR")]
    public class OCR : IReturn<OCRResponse>
    {
        public string inputValue { get; set; }
        public string inputValueTwo { get; set; }
    }

    public class OCRResponse
    {
        public string ReturnMe { get; set; }
    }
}
