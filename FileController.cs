using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiHost
{
    public class FileController : ApiController
    {
        [HttpGet]
        public string GetFileName()
        {
            return "Test";
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveScriptFile()
        {
            var uploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles");
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(Path.Combine(uploadPath, "Upload"));
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                    }

                    string fileName = fileData.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }

                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    File.Move(fileData.LocalFileName, Path.Combine(uploadPath, fileName));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
