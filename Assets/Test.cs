using Inventarys.Data;
using Inventarys.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _add;
    [SerializeField] private Button _remove;

    [SerializeField] private List<string> _ids;

    [SerializeField] private string ID;
    [SerializeField] private bool Statck;
    [SerializeField] private int MaxSize;
    [SerializeField] private int Amount;

    private Inventory _inventory;


    private void Start()
    {
        var data = new InvetoryData()
        {
            Size = 7,
            Slots = GetRandoms()
        };


        _inventory = new Inventory(data);

       ShowInventory();

    }

    private List<InventorySlotData> GetRandoms()
    {
        return new List<InventorySlotData>()
            {
            new InventorySlotData(_ids[Random.Range(1,_ids.Count)],Random.Range(0,30)),
            new InventorySlotData(_ids[Random.Range(1,_ids.Count)],Random.Range(0,30)),
            new InventorySlotData(_ids[Random.Range(1,_ids.Count)],Random.Range(0,30)),
            new InventorySlotData(_ids[Random.Range(1,_ids.Count)],Random.Range(0,30)),
            new InventorySlotData(_ids[Random.Range(1,_ids.Count)],Random.Range(0,30)),
            };
    }

    private void OnEnable()
    {
        _add.onClick.AddListener(OnAddClick);
    }

    private void OnDisable()
    {
        _add.onClick.RemoveAllListeners();
    }

    private void OnAddClick()
    {
        InventoryItem item = new InventoryItem(ID, Statck, MaxSize);
        Inventorys.Structures.AddItemsResult result = _inventory.AddItem(item, Amount);

        Debug.Log(result);
    }

    [ContextMenu(nameof(ShowInventory))]
    private void ShowInventory()
    {
        foreach (var item in _inventory.InventorySlots)
        {
            Debug.Log(item.ItemId.GetValue() + "       " + item.Amount.GetValue().ToString());
        }
    }

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