using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() 
    {
        return KitchenObjectSO;
    }

    public void SetkitchenObjectParent(IKitchenObjectParent kitchenObjectParent) 
    {
       
        if (this.kitchenObjectParent!=null)
        {
            this.kitchenObjectParent.CLearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            return;
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
     
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf() 
    {
        kitchenObjectParent.CLearKitchenObject();
        Destroy(gameObject);
    }

   public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectOS,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectOSTransform = Instantiate(kitchenObjectOS.prefab);
        KitchenObject kitchenObject = kitchenObjectOSTransform.GetComponent<KitchenObject>();
        kitchenObject.SetkitchenObjectParent(kitchenObjectParent);
        kitchenObjectOSTransform.localPosition = Vector3.zero;
        return kitchenObject;
    }

    public bool TryGetPlate(out DishKitchenObject dishKitchenObject)
    {
        if (this is DishKitchenObject)
        {
            
            dishKitchenObject = this as DishKitchenObject;
            return true;
        }
        else
        {
            
            dishKitchenObject =null;
            return false;
        }
    }


}
