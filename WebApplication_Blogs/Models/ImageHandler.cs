using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Blogs.Models
{
    public class ImageHandler
    {
        public static string AddImage(string rootPath,IFormFile ImageFile)
        {
            string ImageFileName = null;
            try
            {
                if (!Directory.Exists(rootPath + "\\images\\"))
                {
                    Directory.CreateDirectory(rootPath + "\\images\\");
                }
                ImageFileName = new Random().Next(10000) + "_" + ImageFile.FileName;
                using (FileStream fileStream = File.Create(
                    rootPath + "\\images\\" + ImageFileName))
                {
                    ImageFile.CopyTo(fileStream);
                    fileStream.Flush();
                    return ImageFileName;
                };
            }
            catch (Exception ex) { 
            }
            return ImageFileName;
        }

        public static bool DeleteIamge(string rootPath,string ImageFileName)
        {
            string ImgPath = rootPath + "\\images\\"+ ImageFileName;
            if (File.Exists(ImgPath))
            {
                File.Delete(ImgPath);
                return true;
            }
            return false;
        }

        public static string UpdateIamge(string rootPath,string old_image, IFormFile ImageFile)
        {
            string ImageFileName = null;
            DeleteIamge(rootPath, old_image);
            ImageFileName = AddImage(rootPath, ImageFile);
            return ImageFileName;
        }
    }
}
