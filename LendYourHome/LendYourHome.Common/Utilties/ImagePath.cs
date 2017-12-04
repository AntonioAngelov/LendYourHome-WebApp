namespace LendYourHome.Common.Utilties
{
    using System;

    public class ImagePath
    {
        public static string GetBase64(string imageRelativePath)
        {
            string path = FileStrem.GetFilePath(imageRelativePath);
            byte[] imagebyte = LoadImage.GetPictureData(path);
            var base64 = Convert.ToBase64String(imagebyte);

            return base64;
        }
    }
}
