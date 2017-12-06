namespace LendYourHome.Services.Files
{
    using System;
    using System.IO;

    public class PictureService : IPictureService
    {
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
    }
}
