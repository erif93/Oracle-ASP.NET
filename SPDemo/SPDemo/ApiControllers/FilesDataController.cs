﻿using SPDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace SPDemo.ApiControllers
{
    public class FilesDataController : ApiController
    {
        [HttpPost]
        public void UploadFile()
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if (file != null)
                        {
                            FileDTO fileDTO = new FileDTO();

                            fileDTO.FileActualName = file.FileName;
                            fileDTO.FileExt = Path.GetExtension(file.FileName);
                            fileDTO.ContentType = file.ContentType;

                            //Generate a unique name using Guid
                            fileDTO.FileUniqueName = Guid.NewGuid().ToString();

                            //Get physical path of our folder where we want to save images
                            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

                            var fileSavePath = System.IO.Path.Combine(rootPath, fileDTO.FileUniqueName + fileDTO.FileExt);

                            //Save the uploaded file to "UploadFiles" folder
                            file.SaveAs(fileSavePath);

                            //Save File Meta data in Database
                            DummyDAL.SaveFileInDB(fileDTO);
                        }
                    }//end of foreach
                }
                catch (Exception ex)
                {

                }
            }//end of if count>0
            var name = HttpContext.Current.Request["Name"];
        }

        [HttpGet]
        public Object DownloadFile(String uniqueName)
        {
            //physical path of root folder
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

            //find file from DB against unique name
            var fileDTO = DummyDAL.GetFileByUniqueID(uniqueName);

            if (fileDTO != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = System.IO.Path.Combine(rootPath, fileDTO.FileUniqueName + fileDTO.FileExt);

                byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //string mimeType = MimeType.GetMimeType(file);
                //you may do ur hard coding here based on file extension

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileDTO.ContentType);
                response.Content.Headers.ContentDisposition.FileName = fileDTO.FileActualName;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}