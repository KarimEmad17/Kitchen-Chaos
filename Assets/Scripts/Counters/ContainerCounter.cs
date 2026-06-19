using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
   
    [SerializeField] private KitchenObjectSO kitchenObjectOS;
    public event EventHandler onPlayerGrabbedObject;
   
    // Start is called before the first frame update

    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
        if (!playerControllerScript.HasKitchenObject())
        {
            onPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Transform kitchenObjectOSTransform = Instantiate(kitchenObjectOS.prefab);
            kitchenObjectOSTransform.GetComponent<KitchenObject>().SetkitchenObjectParent(playerControllerScript);
            kitchenObjectOSTransform.localPosition = Vector3.zero;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
