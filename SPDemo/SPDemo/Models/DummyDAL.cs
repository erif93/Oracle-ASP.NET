﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPDemo.Models
{
    public class DummyDAL
    {
        static List<FileDTO> files = new List<FileDTO>();

        public static void SaveFileInDB(FileDTO dto)
        {
            files.Add(dto);
        }

        public static FileDTO GetFileByUniqueID(String uniqueName)
        {
            return files.Where(x => x.FileUniqueName == uniqueName).FirstOrDefault();
        }

        public static List<FileDTO> GetAllFiles()
        {
            return files;
        }
    }

    public class FileDTO
    {
        public String FileUniqueName { get; set; }
        public String FileActualName { get; set; }
        public String ContentType { get; set; }
        public String FileExt { get; set; }
    }
}