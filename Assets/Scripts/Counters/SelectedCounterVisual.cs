using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualCounters ;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript.Instence.onSelectedCounterChanged +=Player_onSelectedCounterChanged;
    }

    private void Player_onSelectedCounterChanged(object sender, PlayerControllerScript.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter==baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Show()
    {
        foreach (GameObject visualCounter in visualCounters) {
            visualCounter.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualCounter in visualCounters)
        {
            visualCounter.SetActive(false);
        }
    }
}
