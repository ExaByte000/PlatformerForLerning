using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audioMixer;
    private Hero hero;
    private float volume = -80f;
    private bool startFlag = true;

    private void Awake()
    {
       hero = GetComponent<Hero>();
    }
    private void Start()
    {
        StartCoroutine(OffBassVolume());   
    }

    void Update()
    {
        if (hero.isAttacking && startFlag)
        {
            StartCoroutine(OnBassVolume());
            startFlag = false;
        }
        if(!hero.isAttacking) startFlag = true;
        
    }

    private IEnumerator OnBassVolume()
    {
        while (volume != 0f)
        {

            volume++;
            if (volume < -20f)
                yield return new WaitForSeconds(0.1f);
            else
                yield return new WaitForSeconds(0.5f);
            
            if (volume == 0f)
            {
                yield break;
                
            }
            audioMixer.audioMixer.SetFloat("Bass", volume);
            
            
        }
    }
    private IEnumerator OffBassVolume()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if(!(volume <= -80f))
            {
                audioMixer.audioMixer.SetFloat("Bass", volume);
                volume--;
            }
            

        }
    }

}
