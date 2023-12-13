namespace Adressbok.Interfaces;

public interface IFileService
{
    // Se kommentarer i service.
    public bool SaveContentToFile(string content);
    public string GetContentFromFile();
}
