using Adressbok.Interfaces;
using System.Diagnostics;

namespace Adressbok.Services;

public class FileService : IFileService
{
    private readonly string _filePath = @"C:\Programmering\EC\CSHARP-COURSE\Adressbok\Contacts.json";

    /// <summary>
    /// Metod för att spara ner innehåll till ovanstående filepath i form av json.
    /// </summary>
    /// <param name="content">Innehållet som ska sparas ner till datorn.</param>
    /// <returns>Retunerar true vid lyckad sparning, annars false.</returns>
    public bool SaveContentToFile(string content)
    {
        try
        {
            using(var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    /// <summary>
    /// Hämtar upp sparat innehåll från ovanstående filepath.
    /// </summary>
    /// <returns>Om lyckat, returnerar det sparade innehållet från filepath. Om misslyckat, returnerar null.</returns>
    public string GetContentFromFile()
    {
        try
        {
            if(File.Exists(_filePath)) 
            { 
                using(var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
