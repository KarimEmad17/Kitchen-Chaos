using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
   public KitchenObjectSO inPut;
   public  KitchenObjectSO outPut;
    public int cuttingProgressMax;
    
}
