namespace LendYourHome.Common.Utilties
{
    using System.IO;

    public class FileStrem
    {
        public static string GetFilePath(string relativepath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), relativepath);
        }
    }
}
