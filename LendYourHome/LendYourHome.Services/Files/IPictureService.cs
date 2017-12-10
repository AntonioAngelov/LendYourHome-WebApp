namespace LendYourHome.Services.Files
{
    using System.Collections.Generic;

    public interface IPictureService
    {
        string GetFilePath(string relativepath);

        string GetBase64(string imageRelativePath);

        byte[] GetFileData(string imagePath);

        string GetHomePicturesFullPath(int homeId);

        void EditHomePictures(int homeId, List<string> picturesUrls);
    }
}
