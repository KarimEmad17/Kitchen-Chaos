using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishIconsUI : MonoBehaviour
{
    [SerializeField] private DishKitchenObject dishKitchenObject;
    [SerializeField] private Transform iconTemplate;
    // Start is called before the first frame update
    void Start()
    {
        dishKitchenObject.OnIngredientAdded+=DishKitchenObject_OnIngredientAdded;
    }
    private void Awake()
    {
        
    }

    private void DishKitchenObject_OnIngredientAdded(object sender, DishKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child==iconTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in dishKitchenObject.GetDishIngredeindt())
        {
           Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<SingleIconUI>().SetIconImage(kitchenObjectSO.sprite);
        }
    }
}
