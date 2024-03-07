namespace HamRadioStudy.Utilities;

public static class ResourceExtensions
{
    /// <summary>
    /// Get the content of a resource file as a string
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    public static string GetResource(this string name)
    {
        var assembly = typeof(ResourceExtensions).Assembly;
        using var stream = assembly.GetManifestResourceStream(name)
            ?? throw new InvalidDataException($"Resource {name} not found");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Get the content of a resource file as a list of strings, one for each line
    /// skipping empty lines
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetResourceLines(this string name) =>
        name.GetResource()
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => !string.IsNullOrWhiteSpace(line));
}
