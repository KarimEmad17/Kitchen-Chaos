using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnDropTrash;
    public override void InterAct(PlayerControllerScript playerControllerScript)
    {
        if (playerControllerScript.HasKitchenObject())
        {
            playerControllerScript.GetKitchenObject().SetkitchenObjectParent(this);
            this.GetKitchenObject().DestroySelf();
            OnDropTrash?.Invoke(this, EventArgs.Empty);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// potentiol error becouse static Data don't auto clean so , it will couse  a problem at link video = https://www.youtube.com/watch?v=AmGSEH7QcDg&t=41s
    /// at 09:07:30 moment
    /// </summary>
    new public static void ResetStaticData()
    {
        OnDropTrash = null;
    }
}
