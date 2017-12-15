using System;
using System.IO;

using ServiceStack;
using ServiceStack.Web;

namespace OCR_API.ServiceModel
{
    [Route("/OCR")]
    public class OCRRequest : IReturn<OCRResponse>
    {
        public string Base64UploadFile { get; set; }
    }

    public class OCRResponse
    {
        public string jsonResponse { get; set; }
    }
}
