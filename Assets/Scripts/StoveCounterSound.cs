using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool PlayWarningSound = false;
    private float warningSoundTimer=0;
    private float warningSoundTimerMax=0.2f;

    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnStateChange+=StoveCounter_OnStateChange;
        stoveCounter.OnProgressChange+=StoveCounter_OnProgressChange;
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        float BurningFlag = 0.5f;
        PlayWarningSound = stoveCounter.IsFried()&& e.progressNormalized >= BurningFlag;
    }

    private void Awake()
    {
        audioSource=GetComponent<AudioSource>();
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playing = e.state==StoveCounter.States.frying||e.state== StoveCounter.States.fryed;
        if (playing)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayWarningSound)
        {
            warningSoundTimer-=Time.deltaTime;
            if (warningSoundTimer<=0)
            {
                warningSoundTimer=warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(transform.position);
            }
        }
    }
}
