using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOs;
    private List<KitchenObjectSO> kitchenObjectSOs;


    // Start is called before the first frame update
    void Start()
    {
        kitchenObjectSOs = new List<KitchenObjectSO>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // Check if the ingredient is allowed
        if (validKitchenObjectSOs.Contains(kitchenObjectSO))
        {
            if (kitchenObjectSOs.Contains(kitchenObjectSO))
            {
                
                return false;
            }
            else
            {
                kitchenObjectSOs.Add(kitchenObjectSO);
                
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectSO = kitchenObjectSO
                });
                return true;
                
            }
        }
        else
        {
            return false;
        }

        // Check if the ingredient is already on the plate
       

        // Add the ingredient to the plate
        
    }

    public List<KitchenObjectSO> GetDishIngredeindt()
    {
        return kitchenObjectSOs;
    }

}
