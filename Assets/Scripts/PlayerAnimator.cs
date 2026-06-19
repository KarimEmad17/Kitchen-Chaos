using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
   [SerializeField]PlayerControllerScript playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", playerController.IsWalking());
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
}
