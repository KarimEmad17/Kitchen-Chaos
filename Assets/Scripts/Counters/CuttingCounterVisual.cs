using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;
    private string animName = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        cuttingCounter.OnCut+=CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(animName); 
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
