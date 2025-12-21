using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource shootAS;
    public AudioSource shotgunAS;
    public AudioSource impactAS;
    public List<AudioSource> victoryASs;
    public List<AudioSource> failASs;
    public List<AudioSource> ambientASs;
    public AudioSource growlAS;
    public AudioSource timerAS;

    public void PlayShootAS()
    {
        shootAS.Play();
    }
    public void PlayShotgunAS()
    {
        shotgunAS.Play();
    }
    public void PlayTimerAS()
    {
        timerAS.Play();
    }
    public void PlayZombieGrowlAS()
    {
        growlAS.Play();
    }
    public void PlayVictoryAS()
    {
        victoryASs[Random.Range(0, victoryASs.Count)].Play();
    }
    public void PlayFailAS()
    {
        failASs[Random.Range(0, victoryASs.Count)].Play();
    }
    public void PlayImpactAS()
    {
        impactAS.Play();
    }

    public void PlayAmbientAS()
    {
        ambientASs[Random.Range(0, ambientASs.Count)].Play();
    }

    public void StopAmbientAS()
    {
        foreach (var a in ambientASs)
        {
            a.Stop();
        }
    }
}
