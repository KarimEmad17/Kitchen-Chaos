using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IHasProgress
{
    //  public new event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    public  event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
   
    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectRecipe;
    private int cuttingProgress;
    
    // Start is called before the first frame update
    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
      
        
        if (playerControllerScript.HasKitchenObject()&&HasObjectRecipe(playerControllerScript.GetKitchenObject().GetKitchenObjectSO()))
        {

            playerControllerScript.GetKitchenObject().SetkitchenObjectParent(this);
            cuttingProgress = 0;
            var kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            var max = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax;
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNormalized =(float)cuttingProgress/max
            });
        }
        else if (this.HasKitchenObject()&!playerControllerScript.HasKitchenObject())
        {
            this.GetKitchenObject().SetkitchenObjectParent(playerControllerScript);
            
        }else if (this.HasKitchenObject()&&playerControllerScript.HasKitchenObject())
        {
            
            if(playerControllerScript.GetKitchenObject().TryGetPlate(out DishKitchenObject dishKitchenObject))
            {
               
                if (dishKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()))
                {

                    this.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
    public override void AltInterAct(PlayerControllerScript playerControllerScript)
    {
        if (this.HasKitchenObject()&&HasObjectRecipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            var kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            var max = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressMax;
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNormalized =(float)cuttingProgress/max
            });
            if (cuttingProgress==max)
            {
                GetKitchenObject().DestroySelf();
                //
                KitchenObject.SpawnKitchenObject(kitchenObjectSO.outPut, this);
            }

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private CuttingRecipeSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
       CuttingRecipeSO cuttingRecipeSO= GetCuttingRecipeSO(kitchenObjectSO);
       if (cuttingRecipeSO!=null)
        {
            return cuttingRecipeSO;
        }
        return null;
        
    }

    private bool HasObjectRecipe(KitchenObjectSO kitchenObjectSO)
    {
        return GetCuttingRecipeSO(kitchenObjectSO)!=null;
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectRecipe)
        {
            if (cuttingRecipeSO.inPut==kitchenObjectSO)
            {
                return cuttingRecipeSO;
            }

        }
        return null;
    }
    /// <summary>
    /// potentiol error becouse static Data don't auto clean so , it will couse  a problem at link video = https://www.youtube.com/watch?v=AmGSEH7QcDg&t=41s
    /// at 09:07:30 moment
    /// </summary>
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
}
