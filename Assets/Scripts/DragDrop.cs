using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
{

    //public GameObject Canvas;
    [SerializeField] private GameObject canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public int ItemID;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas");
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Vector3 Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mousepos.z = 10;
        this.transform.position = Mousepos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}