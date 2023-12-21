using Adressbok.ClassLibrary.Interfaces;
using Adressbok.ClassLibrary.Services;

namespace Adressbok.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveContentToFileShould_SaveContentToFile_ThenReturnTrue()
    {
        // Arrange
        IFileService _fileService = new FileService();
        string content = "Test Content";
        string filePath = @"C:\Programmering\EC\CSHARP-COURSE\Adressbok\Test.txt";

        // Act
        bool result = _fileService.SaveContentToFile(content, filePath);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void SaveContentToFileShould_ReturnFalse_IfFilePathDoesntExist()
    {
        // Arrange
        IFileService _fileService = new FileService();
        string content = "Test Content";
        string filePath = @"C:\Programmering\asdqwe123/123qweasd/Test.txt";

        // Act
        bool result = _fileService.SaveContentToFile(content, filePath);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetContentFromFileShould_ReadAndGetContent_ThenReturnStringContent()
    {
        // Arrange
        IFileService _fileService = new FileService();
        string filePath = @"C:\Programmering\EC\CSHARP-COURSE\Adressbok\Test.txt";

        // Act
        string result = _fileService.GetContentFromFile(filePath);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetContentFromFileShould_ReturnNull_IfFileDoesNotExist()
    {
        // Arrange
        IFileService _fileService = new FileService();
        string filePath = @"C:\Programmering\asdqwe456/456qweasd/Test.txt";

        // Act
        string result = _fileService.GetContentFromFile(filePath);

        // Assert
        Assert.Null(result);
    }
}
