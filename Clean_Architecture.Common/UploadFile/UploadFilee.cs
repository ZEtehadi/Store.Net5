using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Common.UploadFile
{

    public class UploadFilee
    {
        private readonly IHostingEnvironment _environment;
        public UploadFilee(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public  UploadDto UploadFileMethod(IFormFile file,string src)
        {
            
            if (file != null)
            {
                string folder = $@"images\"+src;
                var UploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

                if (!Directory.Exists(UploadsRootFolder))//if dont have this folder
                {
                    Directory.CreateDirectory(UploadsRootFolder);//then Create folder
                }
                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string FileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(UploadsRootFolder, FileName);
                using (var FileSteam = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(FileSteam);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + FileName,
                    Status = true,
                };
            }
            return null;
        }


        public UploadDto DeleteAndUploadFile(IFormFile file, string src,string ImageName)
        {
            string folder = $@"images\" + src;
            var UploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

            if (!Directory.Exists(UploadsRootFolder))
            {
                Directory.CreateDirectory(UploadsRootFolder);
            }

            // Delete all existing files in the folder
            string[] existingFiles = Directory.GetFiles(UploadsRootFolder);
            foreach (string existingFile in existingFiles)
            {
                System.IO.File.Delete(existingFile);
            }

            if (file == null || file.Length == 0)
            {
                return new UploadDto()
                {
                    Status = false,
                    FileNameAddress = "",
                };
            }

            string fileName = $"{ImageName}.jpg"; // or any other desired file name
            var filePath = Path.Combine(UploadsRootFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return new UploadDto()
            {
                FileNameAddress = folder + fileName,
                Status = true,
            };
        }
    }
}
/// <summary>
/// ////
/// </summary>
