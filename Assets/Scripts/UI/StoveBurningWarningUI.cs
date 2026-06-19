using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnProgressChange+=StoveCounter_OnProgressChange;
        Hide();
        
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        float burningFlag = 0.5f;
        if(stoveCounter.IsFried()&& e.progressNormalized>=burningFlag)
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
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    public void Show()
    {

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
