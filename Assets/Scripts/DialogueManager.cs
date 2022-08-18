using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
   	public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nextText;
    public Button next;
   
    public Animator animator;
   
   	private Queue<string> sentences;
   
   	// Use this for initialization
   	void Start () {
   		sentences = new Queue<string>();
   	}
   
   	public void StartDialogue (Dialogue dialogue)
   	{
   		animator.SetBool("IsOpen", true);
   
   		nameText.text = dialogue.name;
   
   		sentences.Clear();
   
   		foreach (string sentence in dialogue.sentences)
   		{
   			sentences.Enqueue(sentence);
   		}
   
   		DisplayNextSentence();
   	}
   
   	public void DisplayNextSentence ()
   	{
   		if (sentences.Count == 0)
   		{
   			EndDialogue();
   			return;
   		}
   
   		string sentence = sentences.Dequeue();
   		StopAllCoroutines();
   		StartCoroutine(TypeSentence(sentence));
   	}
   
   	IEnumerator TypeSentence (string sentence)
   	{
   		dialogueText.text = "";
   		foreach (char letter in sentence.ToCharArray())
   		{
   			dialogueText.text += letter;
   			yield return null;
   		}
   	}
   
   	void EndDialogue()
   	{
	    animator.SetBool("IsOpen",false);
	    nextText.gameObject.SetActive(true);
	    next.gameObject.SetActive(true);
    }
   
   
}
