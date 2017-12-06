namespace LendYourHome.Services.Files
{
    public interface IPictureService
    {
        string GetFilePath(string relativepath);

        string GetBase64(string imageRelativePath);

        byte[] GetFileData(string imagePath);
    }
}
