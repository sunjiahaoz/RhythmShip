/*
BeatDetect
By: @sunjiahaoz, 2016-5-25

节奏探测
 * 
*/
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BeatDetect : MonoBehaviour {
    [Header("Visualization Parameters")]
    [Range(2, 48)]
    public int numberOfSampleGroups;        
    [Tooltip("How many samples of the currently playing audio to get each frame.")]
    public int sampleSize = 1024;

    public float smoothSpeed = 4;
    public float visScale = 2f;
#region _Event_
    [System.Serializable]
    public class OnEventBeat : UnityEvent<int> { }  // int为numberOfSampleGroups的索引
    [SerializeField]
    public OnEventBeat _OnBeat;
#endregion


    private float[] localY;
    private float[] samples;
    private float maxBeatSize;
    private float logBase;
    private AudioSource music;


    public void Init(AudioSource audioSrc)
    {
        music = audioSrc;
    }

    void Start()
    {
        maxBeatSize = 0;
        samples = new float[sampleSize];

        localY = new float[numberOfSampleGroups];        
        for (int i = 0; i < numberOfSampleGroups; i++)
        {
            localY[i] = 1;
        }

        // This is important. This is what sets up the math for splitting
        // the audio frequencies into groups. Lower frequencies need fewer
        // samples in each sample group, while higher frequencies need
        // more samples in each sample group. Using a logarithmic scale
        // gives you a nice smooth exponentially growing grouping of samples.
        logBase = Mathf.Log(sampleSize, numberOfSampleGroups);
    }
    
    void Update()
    {
        if (music == null
            || !music.isPlaying)
        {
            return;
        }

        music.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        // For each sample group....
        
        for (int i = 0; i < numberOfSampleGroups; i++)
        {
            // Use the logBase from above to get the low/high frequencies
            // for this particular sample group
            int lowFreq = Mathf.FloorToInt(Mathf.Pow(i, logBase));
            int highFreq = Mathf.FloorToInt(Mathf.Pow(i + 1, logBase)) - 1;

            // Get the size of this sample group's "beat" for this frame
            float thisBeat = GetBeatScale(lowFreq, highFreq);

            // This is just for keeping track of what the largest beat size is.
            // Not actually needed for anything other than knowing relative
            // beat sizes.
            if (thisBeat > maxBeatSize)
            {
                maxBeatSize = thisBeat;
                //Debug.Log("New Max Size: " + maxBeatSize);
            }

            // Pop this sample group's cube up, based on beat size, and then
            // let it smoothly shrink back down
            float newY = 0.1f + (visScale * thisBeat);            
            if (newY < localY[i] * 1.2f)
            {
                newY = Mathf.SmoothStep(localY[i], 0.1f, Time.deltaTime * smoothSpeed);                
            }
            else
            {   
                _OnBeat.Invoke(i);                
            }
            localY[i] = newY;
        }
    }

    // Add up the sample sizes for the frequencies currently being played
    // right now and determine how "much" of that sample group is playing.
    float GetBeatScale(int lowFreq, int highFreq)
    {
        float current = 0;
        for (int i = lowFreq; i <= highFreq; i++)
        {
            current += samples[i];
        }
        return current;
    }
}
