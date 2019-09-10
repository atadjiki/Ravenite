using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    //Singleton vars
    private static SubtitleManager _instance;

    public static SubtitleManager Instance { get { return _instance; } }

    public TextMeshProUGUI TextBox;

    public bool TypewriterEffect = false;
    public bool TypewriterSound = false;
    public float type_delay_min = 0.03f;
    public float type_delay_max = 0.09f;
    public float wait_time = 1.0f;
    private bool waiting = false;
    public float return_amount = 5;

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

    public void SetText(string text)
    {
        Clear();
        StartCoroutine(Typewriter(TextBox, text));
        
    }

    public void Clear()
    {
        TextBox.text = "";
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

            for (int i = 0; i < return_amount; i++)
            {
                textMesh.text += "\n";
                yield return new WaitForSeconds(type_delay);
            }
        }
        else
        {
            textMesh.text = text;

            for(int i = 0; i < return_amount; i++)
            {
                textMesh.text += "\n";
            }
            yield return new WaitForSeconds(wait_time);
        }

        waiting = false;
        
    }

    public bool IsWaiting()
    {
        return waiting;
    }
}
