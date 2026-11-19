using System.Collections.Generic;
using UnityEngine;

public class BaseActivationReceivers : MonoBehaviour
{
    [Header("Activation Sources ( Optinal for Weight Changer !")]
    [SerializeField] protected List<BaseActivationSource> activationSources = new List<BaseActivationSource>();

    [Header("Properties")]
    [SerializeField] protected bool isActive;

    protected void OnEnable()
    {
        SubscribeEvents(true);
    }

    protected void OnDisable()
    {
        SubscribeEvents(false);
    }

    #region ----- Functions -----

    protected virtual void ActivateFunction() { }

    /*-----------------------------------------*/

    protected virtual void OpenClose()
    {
        if (activationSources.Count <= 0)
        {
            Debug.Log($"{gameObject.name} objesine hiç bir eleman atanmadı");
            return;
        }

        foreach (BaseActivationSource item in activationSources)
        {
            if (!item.isActive)
            {
                isActive = false;
                return;
            }

            else
                this.isActive = true;
        }
    }

    /*-----------------------------------------*/

    private void SubscribeEvents(bool subscribe)
    {
        if (activationSources.Count <= 0)
        {
            Debug.Log($"{gameObject.name} objesine hiç eleman atanmadı !");
            return;
        }

        if (subscribe)
        {
            foreach (BaseActivationSource item in activationSources)
            {
                if (item == null)
                {
                    Debug.Log($"{this.gameObject.name} objesinde null eleman var !");
                    return;
                }
                item.OnPowerEvent += OpenClose;
            }
        }
        else
        {
            foreach (BaseActivationSource item in activationSources)
            {
                item.OnPowerEvent -= OpenClose;
            }
        }
    }

    #endregion
}
