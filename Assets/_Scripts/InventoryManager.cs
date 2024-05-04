using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Transform inventoryPanel;
    public GridLayoutGroup gridLayoutGroup;

    public Dictionary<string, bool> inventoryItems = new Dictionary<string, bool>();
    public Dictionary<string, GameObject> itemPrefabs = new Dictionary<string, GameObject>();

    public const string Lantern = "lantern";
    public const string Book = "book";
    public const string Amulet = "amulet";
    public const string BookPages = "bookPages";
    public const string Note = "note";

    public GameObject lanternPrefab;
    public GameObject bookPrefab;
    public GameObject amuletPrefab;
    public GameObject bookPagesPrefab;
    public GameObject notePrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        inventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel").transform;

        InitializeInevntoryItems();
    }

    private void InitializeInevntoryItems()
    {
        inventoryItems.Add(Lantern, false);
        inventoryItems.Add(Book, false);
        inventoryItems.Add(Amulet, false);
        inventoryItems.Add(BookPages, false);
        inventoryItems.Add(Note, false);
    }


    public void AddItem(string itemName, bool hasItem)
    {
        if (!inventoryItems.ContainsKey(itemName))
        {
            Debug.LogError("Item " + itemName + "is not recognized");
            return;
        }
     
      if(hasItem && !inventoryItems[itemName])
         {
             if(hasItem && !inventoryItems[itemName])
             {
                 GameObject inventoryItemPrefab = GetPrefabForItem(itemName);
                 if (inventoryItemPrefab != null)
                 {
                     GameObject inventoryItem = Instantiate(inventoryItemPrefab, inventoryPanel);
                     inventoryItem.GetComponentInChildren<Text>().text = itemName;
                 }
             }

             inventoryItems[itemName] = true;

             Debug.Log("Added " + itemName + " to inventory.");
         }

         else
         {
             Debug.Log(itemName + " has already been added to inventory");
         } 
    }

    private GameObject GetPrefabForItem(string itemName)
    {
        switch (itemName)
        {
            case Lantern:
                return lanternPrefab;
            case Book:
                return bookPrefab;
            case Amulet: 
                return amuletPrefab;
            case BookPages: 
                return bookPagesPrefab;
            case Note: 
                return notePrefab;
            default:
                Debug.LogError("Prefab for item " + itemName + " not found");
                return null;
        }
    }

    public bool HasItem(string itemName)
    {
        if (!inventoryItems.ContainsKey(itemName))
        {
            Debug.LogError("Item" + itemName + " is not recognized.");
            return false;
        }

        return inventoryItems.ContainsKey(itemName) && inventoryItems[itemName];
    }
}
