using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : Singleton<SaveManager>
{
    [Serializable]
    public class Data
    {
        public float PlayerHealth;
        public Dictionary<ResourceType, int> Inventory;
    }
    
    private const string Path = "Assets/data.json";

    private Data _data = null;
    
    protected override void SingleAwake()
    {
        _data = new Data();
    }
    
    public void Save()
    {
        _data.PlayerHealth = Player.Instance.Health;
        _data.Inventory = Inventory.Instance.Resources;
        
        string jsonString = JsonConvert.SerializeObject(_data);
        File.WriteAllText(Path, jsonString);
    }

    public void Load()
    {
        if (File.Exists(Path))
        {
            string jsonString = File.ReadAllText(Path);
            _data = JsonConvert.DeserializeObject<Data>(jsonString);

            if (_data.PlayerHealth <= 0)
            {
                return;
            }
            
            Player.Instance.SetStartHealth(_data.PlayerHealth);

            Inventory.Instance.Resources.Clear();
            foreach (KeyValuePair<ResourceType, int> pair in _data.Inventory)
            {
                Inventory.Instance.SetStartResources(pair.Key, pair.Value);
            }
        }
    }
}