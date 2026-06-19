using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerControllerScript player;
    private float footStepTimer;
    private float footStepTimerMax = 0.1f;
    float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        footStepTimer-=Time.deltaTime;
        if (footStepTimer<=0f)
        {
            footStepTimer=footStepTimerMax;
            if (player.IsWalking())
            {
                
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
        }
    }
}
