using Newtonsoft.Json;
using OCR_API.ServiceModel;
using PV_Doc_Template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using ServiceStack;
using tessnet2;


namespace OCR_API.ServiceInterface
{
    public class OCRService : Service
    {
        [EnableCors]
        public object Any(OCRRequest request)
        {
            OCRResponse myResponse = new OCRResponse();
            
            if (request.Base64UploadFile == null || string.IsNullOrWhiteSpace(request.Base64UploadFile.ToString()))
            {
                myResponse.jsonResponse = "There was no file attached or no file name was specified.";
                return myResponse;
            }

            try
            {
                // get the image data and save to local storage
                string base64ImgStr = request.Base64UploadFile.ToString().Split(',')[1];
                byte[] tmpBytes = Convert.FromBase64String(base64ImgStr);
                Image image = (Bitmap)((new ImageConverter()).ConvertFrom(tmpBytes));

                // now do the OCR junk here
                var ocr = new Tesseract();
                ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.,/");
                ocr.Init(@"C:\source\innovation\Content\tessdata", "eng", false);

                var resizedImage = (Bitmap)Resize(image, (7000), (7000), false);
                resizedImage.SetResolution(300, 300);

                var result = ocr.DoOCR(resizedImage, Rectangle.Empty);

                OCRRawDataModel dataItems = new OCRRawDataModel();
                dataItems.DataList = new List<OCRRawDataModel.RawDataItem>();
                foreach (Word word in result)
                {
                    var item = new OCRRawDataModel.RawDataItem();
                    item.Value = word.Text;
                    item.Confidence = (int)word.Confidence;
                    item.LineIndex = word.LineIndex;
                    dataItems.DataList.Add(item);
                }

                var mapper = new IdentificationCardMapper();
                var mappedObjects = mapper.MapDriversLicenseData(dataItems);
                myResponse.jsonResponse = JsonConvert.SerializeObject(mappedObjects);

                // cleanup
                image.Dispose();
                resizedImage.Dispose();
                ocr.Dispose();
            }
            catch (Exception ex)
            {
                // i don't know what to do here...
                myResponse.jsonResponse = ex.Message;
            }
            
            return myResponse;
        }

        public static Bitmap Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }
    }
}