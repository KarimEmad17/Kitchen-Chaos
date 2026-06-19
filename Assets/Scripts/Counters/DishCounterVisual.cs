using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DishCounter dishCounter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private KitchenObjectSO dishKitchenObjectSO;
    private List<GameObject> dishKitchenObjectSOs;


    void Start()
    {
        dishCounter.OnDishNumChange+=DishCounter_OnDishNumChange;
        dishCounter.OnHoldingDish+=DishCounter_OnHoldingDish;
        dishKitchenObjectSOs = new List<GameObject>();
    }

    private void DishCounter_OnHoldingDish(object sender, System.EventArgs e)
    {
        GameObject dish = dishKitchenObjectSOs[dishKitchenObjectSOs.Count-1];
        dishKitchenObjectSOs.Remove(dish);
        Destroy(dish);
    }

    private void DishCounter_OnDishNumChange(object sender, System.EventArgs e)
    {
       
        float dishOffsetY = 0.1f;
        Transform dishVisual = Instantiate(dishKitchenObjectSO.prefab, topPoint);
         dishVisual.localPosition=new Vector3(0, dishOffsetY*dishKitchenObjectSOs.Count, 0);
        dishKitchenObjectSOs.Add(dishVisual.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
