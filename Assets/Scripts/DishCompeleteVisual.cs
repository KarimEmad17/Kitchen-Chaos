using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCompeleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
       public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSO_GameObjectList;
    [SerializeField] private DishKitchenObject dishKitchenObject;
    // Start is called before the first frame update
    void Start()
    {
        dishKitchenObject.OnIngredientAdded+=DishKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject KitchenObjectSO_GameObject in KitchenObjectSO_GameObjectList)
        {


            KitchenObjectSO_GameObject.gameObject.SetActive(false);

        }
    }

    private void DishKitchenObject_OnIngredientAdded(object sender, DishKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject KitchenObjectSO_GameObject in KitchenObjectSO_GameObjectList)
        {
            if(e.kitchenObjectSO ==KitchenObjectSO_GameObject.KitchenObjectSO)
            {
                KitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
