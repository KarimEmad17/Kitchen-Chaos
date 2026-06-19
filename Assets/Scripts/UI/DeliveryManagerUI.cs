using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform iconTemplate;
   

    // Start is called before the first frame update
    void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn+=DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted+=DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            
            Destroy(child.gameObject);
        }
        List<ResiepeSO> resiepeSO = DeliveryManager.Instance.GetLastWaitingOrders();
        foreach (ResiepeSO resiepeSO1 in resiepeSO) 
        { 
            Transform iconTemplateTransform = Instantiate(iconTemplate, container);
            iconTemplateTransform.gameObject.SetActive(true);
            iconTemplateTransform.name = resiepeSO1.RecipeName;
            iconTemplateTransform.GetComponent<DeliveryManagerSingleUI>().setIcon(resiepeSO1);
        }
        
    }
}
