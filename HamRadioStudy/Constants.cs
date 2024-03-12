namespace HamRadioStudy;

// See https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
public static class Constants
{
    public const string DatabaseFilename = "HamRadioStudy.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static string DefaultBackupPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "HamStudyBackups");
}
