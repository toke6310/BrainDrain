using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public List<ItemInfo> MyItem;
    public GameObject ItemPrefab;
}

[System.Serializable]

public class ItemInfo
{

    public string ItemName;
    public Sprite ItemIcon;
    public int ItemID;
    public int RecipeID;

    public ItemInfo(string itemName, Sprite itemIcon, int itemID, int recipeID)
    {
        ItemName = itemName;
        ItemIcon = itemIcon;
        ItemID = itemID;
        RecipeID = recipeID;
    }

}