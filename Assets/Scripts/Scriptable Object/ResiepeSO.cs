using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ResiepeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string RecipeName;
}
