using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurnedRecipeSO : ScriptableObject
{
   public KitchenObjectSO inPut;
   public  KitchenObjectSO outPut;
    public float BurnedProgressMax;
    
}
