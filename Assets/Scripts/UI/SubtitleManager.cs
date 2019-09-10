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
    public float type_delay_min = 0.01f;
    public float type_delay_max = 0.02f;
    public float wait_time = 1.0f;
    private bool waiting = false;
    public float return_amount = 6;
    public float play_every_frames = 5;

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

        float type_delay;

        if (GameState.Instance.debug)
        {
            type_delay = 0;
        }
        else
        {
            type_delay = Random.Range(type_delay_min, type_delay_max);
        }
         

        if (TypewriterEffect && !GameState.Instance.debug)
        {
            int count = 0;
            string typed = "";
            float current_frames = 0;

            while (count < text.Length)
            {
                if(current_frames > play_every_frames)
                {
                    AudioManager.Instance.PlayTypeWriter();
                    current_frames = 0;
                }
                else
                {
                    current_frames++;
                }
                

                typed += text[count];
                count++;
                textMesh.text = typed;
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

        //AudioManager.Instance.PlayCarriage();
        waiting = false;
        
    }

    public bool IsWaiting()
    {
        return waiting;
    }
}
