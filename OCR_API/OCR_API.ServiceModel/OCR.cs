using System;
using System.IO;

using ServiceStack;
using ServiceStack.Web;

namespace OCR_API.ServiceModel
{
    [Route("/OCR")]
    public class OCR : IRequiresRequestStream, IReturn<OCRResponse>
    {
        public string FileName { get; set; }

        public Stream RequestStream { get; set; }
    }

    public class OCRResponse
    {
        public string jsonResponse { get; set; }
    }
}
