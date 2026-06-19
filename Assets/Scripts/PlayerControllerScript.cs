using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour,IKitchenObjectParent
{
    //sigelton Pattern
    public static PlayerControllerScript Instence { get; private set; }
    //
    public event EventHandler<OnSelectedCounterChangedEventArgs> onSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs
    {
        public BaseCounter selectedCounter;
    }
    public event EventHandler OnPickUpSomething;
   
    [SerializeField] float moveSpeed;
    [SerializeField] private float playerSize = 0.6f;
    [SerializeField] private GameInput GameInput;
    [SerializeField] LayerMask countersLayerMask;
    [SerializeField] private Transform TopPoint;
    private bool canMove;
    bool isWalking;
    float playerHeight = 2f;
    Vector3 inputVector;
    Vector3 lastMoveDir;
    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    // Start is called before the first frame update
    void Start()
    {
       
        GameInput.onInteractAction+=GameInput_onInteractAction;
        GameInput.onAltInterAction +=GameInput_onAltInterAction;
    }

    private void GameInput_onAltInterAction(object sender, EventArgs e)
    {
        if (selectedCounter!=null)
        {
            // HandleInterActions();
            selectedCounter.AltInterAct(this);
        }
    }

    private void Awake()
    {
        Instence = this;
    }
    private void GameInput_onInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter!=null)
        {
           // HandleInterActions();
            selectedCounter.InterAct(this);
        }
    }

    // Update is called once per frame
    private  void Update()
  {
        HandleMovement();
        HandleInterActions();
        
  }

    public bool IsWalking() 
    {
        return isWalking;    
    }

    private void HandleMovement() 
    {
         inputVector = GameInput.GetMovementVector();

        if (inputVector!=Vector3.zero)
            lastMoveDir=inputVector;

        if (!canMove)
        {
            //check if it can maove on x direction
            Vector3 moveDirx = new Vector3(inputVector.x, 0, 0);
            canMove =moveDirx.x!=0&&!Physics.CapsuleCast(transform.position, transform.position+Vector3.up*playerHeight, playerSize, moveDirx, moveSpeed*Time.deltaTime);
            if (canMove)
                inputVector=moveDirx;
        }

        canMove =!Physics.CapsuleCast(transform.position, transform.position+Vector3.up*playerHeight, playerSize, inputVector, moveSpeed*Time.deltaTime);
        if (!canMove)
        {
            //check if it can maove on z direction
            Vector3 moveDirz = new Vector3(0, 0, inputVector.z);
            canMove =moveDirz.z!=0&&!Physics.CapsuleCast(transform.position, transform.position+Vector3.up*playerHeight, playerSize, moveDirz, moveSpeed*Time.deltaTime);
            if (canMove)
                inputVector=moveDirz;
        }
        if (canMove)
        {
            inputVector.Normalize();
            transform.position += inputVector *moveSpeed*Time.deltaTime;
            isWalking = inputVector !=Vector3.zero;
            transform.forward=Vector3.Slerp(transform.forward, inputVector, Time.deltaTime*10f);
        }

    }

    private void HandleInterActions()
    {
        Vector3 movedir = new Vector3(inputVector.x, 0, inputVector.z);
        float interActionDistance = 2f;
        if (Physics.Raycast(transform.position, movedir!=Vector3.zero ? movedir : lastMoveDir, out RaycastHit raycastHit, interActionDistance, countersLayerMask))
        {

            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (!KitchenGameManager.Instance.IsGamePlaying())
                    return;
                //has clear counter script component;
                if (baseCounter!=selectedCounter)
                {
                    selectedCounter = baseCounter;
                    SetSelectedCounter(selectedCounter);
                }
            }
            else
            {
                selectedCounter =null;
                SetSelectedCounter(selectedCounter);
            }
        }
        else 
        {
            selectedCounter =null;
            SetSelectedCounter(selectedCounter);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter =selectedCounter;
        onSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter=selectedCounter
        });
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return TopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject =kitchenObject;
        if (kitchenObject!=null)
        {
            OnPickUpSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }
    public void CLearKitchenObject()
    {
        this.kitchenObject = null;
       
    }
    public bool HasKitchenObject()
    {
        return this.kitchenObject!=null;
    }
}
