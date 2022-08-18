using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] private int TargetID;
    [SerializeField] private GameObject Win;
    public GameObject Input1;
    public GameObject Input2;
    public GameObject ItemManager;
    public GameObject ResultPrefab;
    public Transform SlotResult;
    public Transform SlotStart1;
    public Transform SlotStart2;
    public int ItemIuput;
    public Image UIInput1;
    public Image UIInput2;
    public string ItemRecipe;
    public GameObject uiCorrect;
    public GameObject uiInCorrect;


    public void CraftItem(GameObject inputItem)
    {
        ItemIuput++;

        if (ItemIuput == 1)
        {
            Input1 = inputItem;
        }
        else if (ItemIuput == 2)
        {
            Input2 = inputItem;
            ItemRecipe += Input1.GetComponent<DragDrop>().ItemID;
            ItemRecipe += Input2.GetComponent<DragDrop>().ItemID;
            bool isMiss = false;

            int CountRecipe = ItemManager.GetComponent<Item>().MyItem.Count;
            for (int i = 0; i < CountRecipe; i++)
            {
                if (ItemRecipe == ItemManager.GetComponent<Item>().MyItem[i].RecipeID.ToString())
                {
                    GameObject ItemResult = Instantiate(ResultPrefab, SlotResult.transform);
                    ItemResult.GetComponent<Image>().sprite = ItemManager.GetComponent<Item>().MyItem[i].ItemIcon;
                    ItemResult.GetComponent<DragDrop>().ItemID = ItemManager.GetComponent<Item>().MyItem[i].ItemID;
                    uiCorrect.SetActive(true);
                    ScoreManager.instance.AddScore();
                    Input1.SetActive(false);
                    Input2.SetActive(false);
                    StartCoroutine(Wait2());
                    IsItemRecipe();
                    if (ItemResult.GetComponent<DragDrop>().ItemID == TargetID)
                    {
                        FindObjectOfType<AudioManager>().Play("success");
                        StartCoroutine(WaitForWin());
                        ScoreManager.instance.TotalScore();
                    }
                }
                else
                {
                    
                    isMiss = false;
                }
            }
            
            MatchManager.instance.CountMatch();

            if (!isMiss)
            {
                FindObjectOfType<AudioManager>().Play("in-correct");
                IsItemRecipe();
                StartCoroutine(Wait());
                StartCoroutine(Wait2());
            }
        }
    }

    void IsItemRecipe()
    {
        ItemIuput = 0;
        if (ItemRecipe != null)
        {
            ItemRecipe = "";
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        uiInCorrect.SetActive(true);
        SnapToStart();
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(2f);
        uiInCorrect.SetActive(false);
        uiCorrect.SetActive(false);
    }
    
    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Play("win");
        Win.SetActive(true);
    }

    public void SnapToStart()
    {
        Input1.GetComponent<RectTransform>().position = SlotStart1.position;
        Input2.GetComponent<RectTransform>().position = SlotStart2.position;
    }

}
