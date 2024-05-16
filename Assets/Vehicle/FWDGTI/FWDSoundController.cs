using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDSoundController : MonoBehaviour
{
    public FWDController contr;
    public AudioSource source;
    public AudioClip[] powrpms;
    public AudioClip[] drpms;
    public float[] rpmRanges;
    private int currentClipIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Read the RPM from the car controller
        float currentRPM = contr.rpm;

        // Determine which audio clip to play based on the current RPM
        int newClipIndex = GetClipIndexForRPM(currentRPM);
        
        if (newClipIndex != currentClipIndex)
        {
            currentClipIndex = newClipIndex;
            PlayEngineSound(currentClipIndex);
        }
        
    }
    int GetClipIndexForRPM(float rpm)
    {
        for (int i = 0; i < rpmRanges.Length - 1; i++)
        {
            if (rpm >= rpmRanges[i] && rpm < rpmRanges[i + 1])
            {
                return i;
            }
        }

        return rpmRanges.Length - 1; // Return the last clip if RPM exceeds the last range
    }
    void PlayEngineSound(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < powrpms.Length)
        {
            if (contr.gasInput!=0){
                source.clip = powrpms[clipIndex];
                source.Play();
            }
            else{
                source.clip = drpms[clipIndex];
                source.Play();
            }
        }
    }
}
