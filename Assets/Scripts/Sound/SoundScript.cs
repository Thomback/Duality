using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static AudioClip audio1, audio2, audio3, audio4, audio5, audio6, audio7;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audio1 = Resources.Load<AudioClip>("laugh1");
        audio2 = Resources.Load<AudioClip>("laugh2");
        audio3 = Resources.Load<AudioClip>("vocalisation1");
        audio4 = Resources.Load<AudioClip>("vocalisation2");
        audio5 = Resources.Load<AudioClip>("vocalisation3");
        audio6 = Resources.Load<AudioClip>("jump");
        audio7 = Resources.Load<AudioClip>("sword");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "laugh1":
                audioSrc.PlayOneShot(audio1);
                break;
            case "laugh2":
                audioSrc.PlayOneShot(audio2);
                break;
            case "vocalisation1":
                audioSrc.PlayOneShot(audio3);
                break;
            case "vocalisation2":
                audioSrc.PlayOneShot(audio4);
                break;
            case "vocalisation3":
                audioSrc.PlayOneShot(audio5);
                break;
            case "jump":
                audioSrc.PlayOneShot(audio6);
                break;
            case "sword":
                audioSrc.PlayOneShot(audio7);
                break;
        }
    }
}
