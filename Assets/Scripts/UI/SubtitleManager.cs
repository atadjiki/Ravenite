using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    //Singleton vars
    private static SubtitleManager _instance;

    public static SubtitleManager Instance { get { return _instance; } }

    public GameObject ChoicePanel;
    public GameObject DialoguePanel;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI choiceAText;
    public TextMeshProUGUI choiceBText;

    public bool TypewriterEffect = false;
    public bool TypewriterSound = false;
    public float type_delay_min = 0.03f;
    public float type_delay_max = 0.09f;
    public float wait_time = 1.0f;
    private bool waiting = false;

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

        Debug.Log(this.name + " Initialized");
    }

    public void SetText(Constants.Text_Type type, string text)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            StartCoroutine(Typewriter(choiceAText, text));
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            StartCoroutine(Typewriter(choiceBText, text));
        }
        else if(type == Constants.Text_Type.Dialogue)
        {
            StartCoroutine(Typewriter(dialogueText, text));
        }
        
    }

    public void SetSegmentedText(Constants.Text_Type type, string textA, string textB)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            StartCoroutine(SegmentedTypewriter(choiceAText, textA, textB));
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            StartCoroutine(SegmentedTypewriter(choiceBText, textA, textB));
        }
        else if (type == Constants.Text_Type.Dialogue)
        {
            StartCoroutine(SegmentedTypewriter(dialogueText, textA, textB));
        }
    }

    public void Clear(Constants.Text_Type type)
    {
        if (type == Constants.Text_Type.ChoiceA)
        {
            choiceAText.text = "";
        }
        else if (type == Constants.Text_Type.ChoiceB)
        {
            choiceBText.text = "";
        }
        else if (type == Constants.Text_Type.Dialogue)
        {
            dialogueText.text = "";
        }
    }

    public void ClearAll()
    {
        choiceAText.text = "";
        choiceBText.text = "";
        dialogueText.text = "";
    }

    public void ToggleChoices(bool flag)
    {
        ChoicePanel.SetActive(flag);
    }

    public void ToggleDialogue(bool flag)
    {
        DialoguePanel.SetActive(flag);
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

        waiting = true;
        float type_delay = Random.Range(type_delay_min, type_delay_max);

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
                AudioManager.Instance.PlayTypeWriter();
                yield return new WaitForSeconds(type_delay);
            }
        }
        else
        {
            textMesh.text = textA + textB;
            yield return new WaitForSeconds(wait_time);
        }

        waiting = false;

        
    }

    IEnumerator Typewriter(TextMeshProUGUI textMesh, string text)
    {

        waiting = true;
        float type_delay = Random.Range(type_delay_min, type_delay_max);

        if (TypewriterEffect)
        {
            int count = 0;
            string typed = "";

            while (count < text.Length)
            {
                typed += text[count];
                count++;
                textMesh.text = typed;
                AudioManager.Instance.PlayTypeWriter();
                yield return new WaitForSeconds(type_delay);
            }
        }
        else
        {
            textMesh.text = text;
            yield return new WaitForSeconds(wait_time);
        }

        waiting = false;
        
    }

    public bool IsWaiting()
    {
        return waiting;
    }
}
