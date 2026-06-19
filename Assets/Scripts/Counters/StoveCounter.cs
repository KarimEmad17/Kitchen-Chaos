using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public States state;
    }
    public  event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    

    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;
    [SerializeField] private BurnedRecipeSO[] burnedRecipeSOs;
    private float fryingProgress;
    private float burningProgress;
    private FryingRecipeSO fryingRecipeSO;
    private BurnedRecipeSO burnedRecipeSO;
    public enum States
    {
        idle,
        frying,
        fryed,
        burned
    }
    private States state;

    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
        if (playerControllerScript.HasKitchenObject()&&HasObjectRecipe(playerControllerScript.GetKitchenObject().GetKitchenObjectSO())&&!this.HasKitchenObject())
        {

            playerControllerScript.GetKitchenObject().SetkitchenObjectParent(this);

            fryingRecipeSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            fryingProgress=0f;
            state=States.frying;
            OnStateChange?.Invoke(this, new OnStateChangeEventArgs
            {
                state =state
            });


        }
        else if (this.HasKitchenObject()&!playerControllerScript.HasKitchenObject())
        {
            this.GetKitchenObject().SetkitchenObjectParent(playerControllerScript);
            fryingProgress=0f;
            burningProgress=0f;
            OnProgressChange.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNormalized = fryingProgress/fryingRecipeSO.fryingProgressMax
            });
            state =States.idle;
            OnStateChange?.Invoke(this, new OnStateChangeEventArgs
            {
                state =state
            });

        }else if(this.HasKitchenObject()&&playerControllerScript.HasKitchenObject())
        {
            if (playerControllerScript.GetKitchenObject().TryGetPlate(out DishKitchenObject dishKitchenObject))
            {
                if (dishKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()))
                {
                    state=States.idle;
                    fryingProgress=0f;
                    burningProgress=0f;
                    OnProgressChange.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        progressNormalized = fryingProgress/fryingRecipeSO.fryingProgressMax
                    });
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state =state
                    });
                    this.GetKitchenObject().DestroySelf();
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.idle:
                break;
            case States.frying :
                
                if (HasKitchenObject())
                {
                    fryingProgress+=Time.deltaTime;
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs
                    {
                        progressNormalized=fryingProgress/fryingRecipeSO.fryingProgressMax
                    });
                    
                    
                    if (fryingRecipeSO.fryingProgressMax<fryingProgress)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.outPut, this);
                        state=States.fryed;
                        burnedRecipeSO= GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                        burningProgress =0f;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state =state
                        });
                    }
                }
                break;
            case States.fryed :
                if (HasKitchenObject())
                {
                    burningProgress+=Time.deltaTime;
                    
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        progressNormalized=burningProgress/burnedRecipeSO.BurnedProgressMax
                    });
                    if (burnedRecipeSO.BurnedProgressMax<burningProgress)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burnedRecipeSO.outPut, this);
                        state=States.burned;
                        burningProgress =0f;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state =state
                        });
                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized=0f
                        });

                    }
                }
                break;
            case States.burned:
                break;
        }
    }

    private FryingRecipeSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetCuttingRecipeSO(kitchenObjectSO);
        if (fryingRecipeSO!=null)
        {
            return fryingRecipeSO;
        }
        return null;

    }

    private bool HasObjectRecipe(KitchenObjectSO kitchenObjectSO)
    {
        return GetCuttingRecipeSO(kitchenObjectSO)!=null;
    }

    private FryingRecipeSO GetCuttingRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs)
        {
            if (fryingRecipeSO.inPut==kitchenObjectSO)
            {
                return fryingRecipeSO;
            }

        }
        return null;
    }
    private BurnedRecipeSO GetBurningRecipeSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurnedRecipeSO fryingRecipeSO in burnedRecipeSOs)
        {
            if (fryingRecipeSO.inPut==kitchenObjectSO)
            {
                return fryingRecipeSO;
            }

        }
        return null;
    }

    public bool IsFried()
    {
        return state == States.fryed;
    }
}
