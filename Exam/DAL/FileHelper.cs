namespace DAL;

public static class FileHelper
{
    public static string BasePath = Environment
                                        .GetFolderPath(Environment.SpecialFolder.UserProfile)
                                    + Path.DirectorySeparatorChar + "Restaurant" + Path.DirectorySeparatorChar;
}
