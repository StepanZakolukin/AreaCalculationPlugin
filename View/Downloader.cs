using System.Drawing.Text;
using System.IO.Compression;

namespace AreaCalculationPlugin.View;

public class Downloader
{
    private string tempFolder;

    public async Task AddFontFamilyToCollection(string url, PrivateFontCollection fontCollection)
    {
        tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempFolder);

        var downloadPath = Path.Combine(tempFolder, $"{Guid.NewGuid()}.zip");
        var extractPath = Path.Combine(tempFolder, $"{Guid.NewGuid()}");

        await DownloadFontFolder(url, downloadPath, extractPath);

        AddFontFilesToCollection(fontCollection, extractPath);
        DeleteFolder(tempFolder);
    }

    private void AddFontFilesToCollection(PrivateFontCollection fonts, string pathToFontFolder)
    {
        foreach (var file in Directory.GetFiles(pathToFontFolder))
            fonts.AddFontFile(file);
    }

    private void DeleteFolder(string tempDir)
    {
        if (Directory.Exists(tempDir))
            Directory.Delete(tempDir, true);
    }

    private async Task DownloadFontFolder(string url, string downloadPath, string extractPath)
    {
        try
        {
            // Шаг 1: Скачиваем архив
            using (HttpClient client = new HttpClient())
            {
                byte[] data = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(downloadPath, data);
            }

            // Шаг 2: Распаковываем архив
            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true); // Очистка папки, если она уже есть
            ZipFile.ExtractToDirectory(downloadPath, extractPath);
        }
        catch (Exception ex)
        {
            DeleteFolder(tempFolder);
            throw ex;
        }
        finally
        {
            // Очистка временных файлов (если необходимо)
            if (File.Exists(downloadPath))
                File.Delete(downloadPath);
        }
    }
}
