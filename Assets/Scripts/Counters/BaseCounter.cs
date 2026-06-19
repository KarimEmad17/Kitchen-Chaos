using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform TopPoint;
   
    private KitchenObject kitchenObject;
    public static event EventHandler OnDropSomething;


    // public virtual event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;


    public virtual void InterAct(PlayerControllerScript playerControllerScript) {
        Debug.Log("karim");
    }
    public virtual void AltInterAct(PlayerControllerScript playerControllerScript)
    {
        Debug.Log("karim");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            OnDropSomething?.Invoke(this, EventArgs.Empty);
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
    /// <summary>
    /// potentiol error becouse static Data don't auto clean so , it will couse  a problem at link video = https://www.youtube.com/watch?v=AmGSEH7QcDg&t=41s
    /// at 09:07:30 moment
    /// </summary>
    public static void ResetStaticData()
    {
        OnDropSomething = null;
    }
}
