namespace Adressbok.ClassLibrary.Interfaces;

public interface IFileService
{
    // Se kommentarer i service.
    public bool SaveContentToFile(string content, string filePath);
    public string GetContentFromFile(string filePath);
}
