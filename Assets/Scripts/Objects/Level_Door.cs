using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Level_Door : MonoBehaviour
{
    
    [Header("Door Items")]
    [SerializeField] private List<Activateable_Objects> doorItems = new List<Activateable_Objects>();
    public event Action OnAnimationEvent;
    [SerializeField] public bool isActive;

    private void OnEnable()
    {
        SubscribeEvents(true);
    }

    private void OnDisable()
    {
        SubscribeEvents(false);
    }

    private void OpenClose() 
    {
        if (doorItems.Count <= 0)
        { 
            Debug.Log("Level kapısına hiç bir eleman atanmadı"); 
            return; 
        }

        foreach (Activateable_Objects item in doorItems)
        {
            if (!item.isActive)
            {
                isActive = false;
                OnAnimationEvent?.Invoke();
                return;
            }
            else
            {
                this.isActive = true;
                OnAnimationEvent?.Invoke();
                Debug.Log("Kapı Açıldı !");
            }
        }
    }

    /*-----*/

   

    /*-----*/

    private void SubscribeEvents(bool subscribe)  
    {
        if(subscribe)
        {
            foreach (Activateable_Objects item in doorItems)
            {
                item.OnPowerEvent += OpenClose;
            }
        }
        else
        {
            foreach (Activateable_Objects item in doorItems)
            {
                item.OnPowerEvent -= OpenClose;
            }
        } 
    }
}
