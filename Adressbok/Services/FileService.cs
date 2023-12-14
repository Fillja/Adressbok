using Adressbok.Interfaces;
using System.Diagnostics;

namespace Adressbok.Services;

public class FileService : IFileService
{
    //private readonly string _filePath = @"C:\Programmering\EC\CSHARP-COURSE\Adressbok\Contacts.json";

    /// <summary>
    /// Metod för att spara ner innehåll till ovanstående filepath i form av json.
    /// </summary>
    /// <param name="content">Innehållet som ska sparas ner till datorn.</param>
    /// <param name="filePath">Directory där filen finns som content ska sparas ner på.</param>
    /// <returns>Retunerar true vid lyckad sparning, annars false.</returns>
    public bool SaveContentToFile(string content, string filePath)
    {
        try
        {
            using(var sw = new StreamWriter(filePath))
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
    public string GetContentFromFile(string filePath)
    {
        try
        {
            if(File.Exists(filePath)) 
            { 
                using(var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
