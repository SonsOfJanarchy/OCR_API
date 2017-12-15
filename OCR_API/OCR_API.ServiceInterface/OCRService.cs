using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ServiceStack;
using OCR_API.ServiceModel;

namespace OCR_API.ServiceInterface
{
    public class OCRService : Service
    {
        public object Any(OCRRequest request)
        {
            OCRResponse myResponse = new OCRResponse();

            if (string.IsNullOrWhiteSpace(request.Base64UploadFile))
            {
                myResponse.jsonResponse = "There was no file attached or no file name was specified.";
                return myResponse;
            }

            string base64ImgStr = request.Base64UploadFile.Split(',')[1];
            byte[] tmpBytes = Convert.FromBase64String(base64ImgStr);
            string fileName = DateTime.Now.Ticks.ToString();
            File.WriteAllBytes(@"C:\temp\OCR\" + fileName, tmpBytes);

            

            myResponse.jsonResponse = "This is the return text.  You've reached the end of the internet...";
            return myResponse;
        }
    }
}