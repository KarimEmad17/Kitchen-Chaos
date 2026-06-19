using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        Instance = this;
    }
    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
        if (playerControllerScript.HasKitchenObject())
        {
            if(playerControllerScript.GetKitchenObject().TryGetPlate(out DishKitchenObject dishKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(dishKitchenObject);
                dishKitchenObject.DestroySelf();
            }
        }
    }
}
