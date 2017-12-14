namespace LendYourHome.Services.Files
{
    using System.Collections.Generic;

    public interface IPictureService
    {
        string GetFilePath(string relativepath);

        string GetBase64(string imageRelativePath);

        byte[] GetFileData(string imagePath);

        string GetHomePicturesFullPath(int homeId);

        string GetUserProfilePictureFullPath(string userId);

        void EditHomePictures(int homeId, List<string> picturesUrls);

        string PreparePictureToDisplay(string relativePath);
    }
}
