using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChange;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver
    };
    private State state;
    
    private float CountDownToStartTimer = 3f;
    private float PlayingTimer =0f;
    private float PlayingTimerMax = 30f;
    private bool isGamePause=false;

    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.onPauseAction+=GameInpute_onPauseAction;
        GameInput.Instance.onInteractAction+=GameInput_onInteractAction;

    }

    private void GameInput_onInteractAction(object sender, EventArgs e)
    {
        if (state==State.WaitingToStart)
        {
            state=State.CountDownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInpute_onPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart :
                
                break;
            case State.CountDownToStart:
                CountDownToStartTimer-=Time.deltaTime;
                if (CountDownToStartTimer<0)
                {
                    PlayingTimer =PlayingTimerMax;
                    state = State.Playing;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                PlayingTimer-=Time.deltaTime;
                if (PlayingTimer<0)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
       
    }
    private void Awake()
    {
        state = State.WaitingToStart;
        Instance=this;
    }

    public bool IsGamePlaying()
    {
        return state==State.Playing;
    }
    public bool IsCountDownActive()
    {
        return state==State.CountDownToStart;
    }
    public float GetCountDownTimer()
    {
        return CountDownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state==State.GameOver;
    }
    public float GetPlayingTimerNormalized()
    {
        return 1-(PlayingTimer/PlayingTimerMax);
    }
    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause) 
        {
            Time.timeScale=0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
            Time.timeScale=1f;
        }
    }
}
