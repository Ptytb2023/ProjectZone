using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

public class Test : MonoBehaviour
{
    [SerializeField] private FilePathSo _filePath;
    [SerializeField] private int ammount;

}
public class JsonNetFileService
{
    public async Task<TModel> LoadAsync<TModel>(string filePath)
    {
        PermissionReques();

        using var reader = new StreamReader(filePath);

        string json = await reader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<TModel>(json);
    }

    public async Task SaveAsync<TModel>(TModel model, string filePath)
    {
        PermissionReques();

        using var writer = new StreamWriter(filePath);

        string json = JsonConvert.SerializeObject(model, Formatting.Indented);

        await writer.WriteAsync(json);
    }

    public TModel Load<TModel>(string filePath)
    {
        PermissionReques();

        if (File.Exists(filePath))
        {
            FileStream fileStream = File.Create(filePath);
        }

        var reader = new StreamReader(filePath);

        string json = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<TModel>(json);
    }

    private void PermissionReques()
    {
        bool isRead = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
        bool isWrite = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);


        UnityEngine.Debug.Log($"isWrite = {isWrite}, isRead = {isRead}");

        if (!(isRead && isWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

}