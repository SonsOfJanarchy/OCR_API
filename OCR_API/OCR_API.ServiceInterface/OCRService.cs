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
        public object Post(OCRRequest request)
        {
            OCRResponse myResponse = new OCRResponse();

            if (string.IsNullOrWhiteSpace(request.FileName) || request.RequestStream.Length == 0)
            {
                myResponse.jsonResponse = "There was no file attached or no file name was specified.";
                return myResponse;
            }

            // we need to save off the file on the local machine...
            string localPath = ConfigurationManager.AppSettings["TempImageDir"] + request.FileName;

            if (!File.Exists(localPath))
            {
                //FileStream fs = new FileStream(localPath, FileMode.CreateNew);
                //request.RequestStream.CopyTo(fs);
                //fs.Close();
                //fs.Dispose();

                //Image saveBmp = Bitmap.FromStream(request.RequestStream);
                //saveBmp.Save(localPath, System.Drawing.Imaging.ImageFormat.Bmp);
                //saveBmp.Dispose();

                ImageConverter imgConvert = new System.Drawing.ImageConverter();
                byte[] imgBytes = null;
                using (var binReader = new BinaryReader(request.RequestStream))
                {
                    imgBytes = binReader.ReadBytes((int)request.RequestStream.Length);
                }

                Image img = (Image)imgConvert.ConvertFrom(imgBytes);
                img.Save(localPath, ImageFormat.Bmp);
                img.Dispose();
            }
            else
            {
                myResponse.jsonResponse = "The file exists!";
                return myResponse;
            }
            
            myResponse.jsonResponse = "This is the return text.  You've reached the end of the internet...";
            return myResponse;
        }
    }
}