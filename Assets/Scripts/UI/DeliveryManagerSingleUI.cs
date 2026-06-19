using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI recipeName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setIcon(ResiepeSO resiepeSO)
    {
        recipeName.text = resiepeSO.RecipeName;
        foreach (KitchenObjectSO kitchenObjectSO in resiepeSO.kitchenObjectSOList)
        {
            Transform IconTransform = Instantiate(image.transform, iconContainer);
            IconTransform.gameObject.name = kitchenObjectSO.ObjectName;
            IconTransform.gameObject.SetActive(true);
            IconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
