using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;
    private string animName = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        containerCounter.onPlayerGrabbedObject +=ContainerCounter_onPlayerGrabbedObject;
    }

    private void ContainerCounter_onPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(animName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
