using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio.playOnAwake = true;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
