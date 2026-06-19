using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set;  }
    [SerializeField] RecipeSOList _recipeAvailable;
    private List<ResiepeSO> waitingOrders;

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucceed;
    public event EventHandler OnRecipeFail;
    private float spawnTimer;
    private float spawnTimerMax = 6f;
    private int succesfullRecipeAmount;
    // Start is called before the first frame update
    void Start()
    {
        succesfullRecipeAmount=0;
        waitingOrders = new List<ResiepeSO>();
    }
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer-=Time.deltaTime;
        if (spawnTimer<=0)
        {
            spawnTimer=spawnTimerMax;
            if (KitchenGameManager.Instance.IsGamePlaying()&&waitingOrders.Count<4)
            {
                waitingOrders.Add(_recipeAvailable.resiepeListSO[UnityEngine.Random.Range(0,_recipeAvailable.resiepeListSO.Count)]);
                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }

    public void DeliverRecipe(DishKitchenObject dishKitchenObject)
    {
        for(int i = 0; i<waitingOrders.Count; i++)
        {
            ResiepeSO waitingOrder = waitingOrders[i];
            if (waitingOrder.kitchenObjectSOList.Count==dishKitchenObject.GetDishIngredeindt().Count)
            {
               
                bool gotMatch = true;
                //the ingredient are equal so we continue 
                foreach(KitchenObjectSO orderkitchenObjectSO in waitingOrder.kitchenObjectSOList)
                {
                    //search in the exact order 
                    bool found = false;
                    foreach(KitchenObjectSO dishKitchenObjectSO in dishKitchenObject.GetDishIngredeindt())
                    {
                        if (orderkitchenObjectSO==dishKitchenObjectSO)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        gotMatch=false;
                        //if in any time don't get match
                        
                        
                    }
                }
                if (gotMatch)
                {
                    
                    waitingOrders.Remove(waitingOrder);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucceed?.Invoke(this, EventArgs.Empty);
                    succesfullRecipeAmount+=1;
                    return;
                }
            }
            
            
        }
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }

    public List<ResiepeSO> GetLastWaitingOrders()
    {
        return waitingOrders;
    }
    public int GetRecipeDelivered()
    {
        return succesfullRecipeAmount;
    }
}
