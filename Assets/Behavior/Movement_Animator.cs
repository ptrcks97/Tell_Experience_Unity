using UnityEngine;
using System;
using System.Collections;
public class Movement_Script : MonoBehaviour
{
    [SerializeField] private GameObject wilhelm;
    [SerializeField] private GameObject wilhelm_main;
    private AudioSource audioSource;
    private AnimatorStateInfo stateInfo;
    private float silence = 0f;
    private float silenceDelay = 1f;
    private int emotion;
    bool talks = false;
    bool wasTalking = false;
    float[] samples;
    Animator anim;
    private readonly double epsilon = 1e-4;

    void Start()
    {
        audioSource = wilhelm.GetComponent<AudioSource>();
        samples = new float[256];
        anim = wilhelm_main.GetComponent<Animator>();
    }
    void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        talks = false;
        audioSource.GetOutputData(samples,0);
        foreach(float sample in samples)
        {
            if(Math.Abs(sample) > epsilon)
            {
                talks = true;
            }
        }
        if(wasTalking && !talks)
        {
            silence += Time.deltaTime;
            if(silence > silenceDelay)
            {
                talks = false;
                silence = 0;
            }
            else
            {
                talks = true;
            }
        }
        Array.Clear(samples, 0, samples.Length);
        if (talks && !wasTalking)
        {
            emotion = UnityEngine.Random.Range(0, 3);
            anim.SetBool("Speaking", talks);
            anim.SetInteger("emotion", emotion);
        }
        else if(!talks && wasTalking){
            anim.SetBool("Speaking", talks);
        }
        wasTalking = talks;
    } 
}
