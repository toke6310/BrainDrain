using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trigger : MonoBehaviour, IDropHandler
{
    public GameObject ItemManager;
    public GameObject CraftSystem;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            this.GetComponent<Image>().sprite = ItemManager.GetComponent<Item>().MyItem[other.GetComponent<DragDrop>().ItemID].ItemIcon;
            CraftSystem.GetComponent<CraftSystem>().CraftItem(other.gameObject);
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            this.GetComponent<Image>().sprite = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
    }
    
}
