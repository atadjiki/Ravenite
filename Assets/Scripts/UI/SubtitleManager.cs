using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    //Singleton vars
    private static SubtitleManager _instance;

    public static SubtitleManager Instance { get { return _instance; } }

    public GameObject dialogueText;
    public GameObject choiceAText;
    public GameObject choiceBText;

    public bool TypewriterEffect = false;
    public float type_speed = 0.15f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetText(Constants.Text_Type type, string text)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            StartCoroutine(Typewriter(choiceAText.GetComponent<TextMeshProUGUI>(), text));
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            StartCoroutine(Typewriter(choiceBText.GetComponent<TextMeshProUGUI>(), text));
        }
        else if(type == Constants.Text_Type.Dialogue)
        {
            StartCoroutine(Typewriter(dialogueText.GetComponent<TextMeshProUGUI>(), text));
        }
        
    }

    public void SetSegmentedText(Constants.Text_Type type, string textA, string textB)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            StartCoroutine(SegmentedTypewriter(choiceAText.GetComponent<TextMeshProUGUI>(), textA, textB));
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            StartCoroutine(SegmentedTypewriter(choiceBText.GetComponent<TextMeshProUGUI>(), textA, textB));
        }
        else if (type == Constants.Text_Type.Dialogue)
        {
            StartCoroutine(SegmentedTypewriter(dialogueText.GetComponent<TextMeshProUGUI>(), textA, textB));
        }
    }

    public void Clear(Constants.Text_Type type)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            choiceAText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            choiceBText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (type == Constants.Text_Type.Dialogue)
        {
            dialogueText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void ClearAll()
    {
        choiceAText.GetComponent<TextMeshProUGUI>().text = "";
        choiceBText.GetComponent<TextMeshProUGUI>().text = "";
        dialogueText.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void ToggleChoices(bool flag)
    {
        choiceAText.SetActive(flag);
        choiceBText.SetActive(flag); 
    }

    public void ToggleDialogue(bool flag)
    {
        dialogueText.SetActive(flag);
    }

    public void SubtitleMode()
    {
        ToggleChoices(false);
        Clear(Constants.Text_Type.Dialogue);
        ToggleDialogue(true);
    }

    public void ChoiceMode()
    {
        ToggleDialogue(false);
        Clear(Constants.Text_Type.ChoiceA);
        Clear(Constants.Text_Type.ChoiceB);
        ToggleChoices(true);
    }

    IEnumerator SegmentedTypewriter(TextMeshProUGUI textMesh, string textA, string textB)
    {

        if (TypewriterEffect)
        {
            int count = 0;
            string typed = "";

            textMesh.text = textA;

            while (count < textB.Length)
            {
                typed += textB[count];
                count++;
                textMesh.text = textA + typed;
                yield return new WaitForSeconds(type_speed);
            }
        }
        else
        {
            textMesh.text = textA + textB;
            yield return new WaitForEndOfFrame();
        }

        
    }

    IEnumerator Typewriter(TextMeshProUGUI textMesh, string text)
    {
        if (TypewriterEffect)
        {
            int count = 0;
            string typed = "";

            while (count < text.Length)
            {
                typed += text[count];
                count++;
                textMesh.text = typed;
                yield return new WaitForSeconds(type_speed);
            }
        }
        else
        {
            textMesh.text = text;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
