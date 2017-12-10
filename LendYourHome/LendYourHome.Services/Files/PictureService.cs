namespace LendYourHome.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;
    using Data.Models;

    public class PictureService : IPictureService
    {
        private readonly LendYourHomeDbContext db;

        public PictureService(LendYourHomeDbContext db)
        {
            this.db = db;
        }


        public string GetFilePath(string relativepath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativepath);
        }

        public byte[] GetFileData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }

        public string GetBase64(string imageRelativePath)
        {
            string path = this.GetFilePath(imageRelativePath);
            byte[] imagebyte = this.GetFileData(path);
            var base64 = Convert.ToBase64String(imagebyte);

            return base64;
        }

        public string GetHomePicturesFullPath(int homeId)
        {
            var pathTokens = this.db
                .Pictures
                .Where(p => p.HomeId == homeId)
                .FirstOrDefault()
                .Url
                .Split('/')
                .Take(3);

            return this.GetFilePath(string.Join("/", pathTokens));
        }

        public void EditHomePictures(int homeId, List<string> picturesUrls)
        {
            //remove old pictures
            this.db.Pictures.RemoveRange(
                this.db.Pictures.Where(p => p.HomeId == homeId));

            //save new ones
            foreach (var url in picturesUrls)
            {
                this.db.Pictures.Add(new Picture
                {
                    Url = url,
                    HomeId = homeId
                });
            }

            this.db.SaveChanges();
        }
    }
}
