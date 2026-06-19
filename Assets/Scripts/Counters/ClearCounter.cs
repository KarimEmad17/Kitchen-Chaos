using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] private CuttingRecipeSO kitchenObjectOS;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void InterAct(PlayerControllerScript playerControllerScript) 
    {
       
        if (playerControllerScript.HasKitchenObject()&&!this.HasKitchenObject())
        {
            
            //player has thing and counter not
            playerControllerScript.GetKitchenObject().SetkitchenObjectParent(this);
        }
        else if (this.HasKitchenObject()&!playerControllerScript.HasKitchenObject())
        {
            //counter has thing and player not
            this.GetKitchenObject().SetkitchenObjectParent(playerControllerScript);
        }else if (playerControllerScript.HasKitchenObject()&&this.HasKitchenObject())
        {
            //counter and player has things but not know which has dish
            
            if (playerControllerScript.GetKitchenObject().TryGetPlate(out DishKitchenObject dishKitchenObject))
            {
               
                
                //counter and player has things and -> player <- hold Dish
                 
                if (dishKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()))
                {

                    this.GetKitchenObject().DestroySelf();
                }

            }
            else  
            {
                if (this.GetKitchenObject().TryGetPlate(out DishKitchenObject dishKitchenObject_2))
                {
                    //counter and player has things and -> counter <- has Dish
                    
                    if (dishKitchenObject_2.TryAddIngredient(playerControllerScript.GetKitchenObject().GetKitchenObjectSO()))
                    {

                        playerControllerScript.GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
        
        
    }

    
}
