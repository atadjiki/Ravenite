using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{
    public GameObject record;
    public GameObject twistyboi;
    public GameObject needle;
    private Transform recordT;
    private Transform tBT;
    private SkinnedMeshRenderer phonograph;
    private float bs;
    private float p;
    // Start is called before the first frame update
    void Start()
    {
        recordT = record.transform;
        tBT = twistyboi.transform;
        phonograph = needle.GetComponent<SkinnedMeshRenderer>();
        p = 1; 
    }

    // Update is called once per frame
    void Update()
    {

        if (GameState.Instance.IsMusicPlaying())
        {
            recordT.Rotate(0, 3, 0);
            tBT.Rotate(0, 0, 1);
            bs = bs + (1f * p);
            blendshapedelta();
            phonograph.SetBlendShapeWeight(0, bs);
        }
    }


    void blendshapedelta()
    {
       
       

        if (bs < 0f)
        {
            p = 1;
        }else if(bs > 100f)
        {
            p = -1;
        }
    }
}
