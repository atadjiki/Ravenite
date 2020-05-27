using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleTest : MonoBehaviour
{
    enum State
    {
        start, middle, end,
    };
    private TextMeshProUGUI textmeshpro;
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.start;
        textmeshpro = GetComponent<TextMeshProUGUI>();
        //textmeshpro = GetComponent<TextMeshPro>();
        //textmeshpro.SetText("hello");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(state == State.start)
                textmeshpro.SetText("hello");
            else if(state == State.middle)
                textmeshpro.SetText("I'm testing something");
            else if(state == State.end)
                textmeshpro.SetText("Thank you");

            state++;
        }
    }
}
