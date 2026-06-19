using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO dishKitchenObjectSO; 
    private int dishNumber=0;
    private int dishMaxNumber=4;
    private float spawnTimer=0f;
    private float spawnTimerMax=5.5f;
    public event EventHandler OnDishNumChange;
    public event EventHandler OnHoldingDish;

    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
        if (!playerControllerScript.HasKitchenObject())
        {
            if (dishNumber>0)
            {
                OnHoldingDish?.Invoke(this, EventArgs.Empty);
                dishNumber--;
                KitchenObject.SpawnKitchenObject(dishKitchenObjectSO, playerControllerScript);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer+=Time.deltaTime;
       
        if (spawnTimerMax<=spawnTimer)
        {
            spawnTimer=0f;
            if (KitchenGameManager.Instance.IsGamePlaying() && dishNumber<dishMaxNumber)
            {
                dishNumber++;
                OnDishNumChange?.Invoke(this, EventArgs.Empty);
            }
        }    
    }
}
