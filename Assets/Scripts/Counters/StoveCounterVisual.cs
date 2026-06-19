using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnStateChange+=StoveCounter_OnStateChange;    
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = (e.state==StoveCounter.States.frying||e.state==StoveCounter.States.fryed);

        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
