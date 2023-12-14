using Adressbok.Interfaces;
using Adressbok.Services;

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
}
