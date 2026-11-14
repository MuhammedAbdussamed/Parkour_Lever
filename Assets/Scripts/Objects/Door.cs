using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [Header("Object References")]
    [SerializeField] private List<Activateable_Objects> doorItems = new List<Activateable_Objects>();
    [SerializeField] private Transform[] doorParts;

    [Header("Door Stats")]
    [SerializeField] private float openingSpeed;
    [SerializeField] private float slideDistance;
    [SerializeField] private bool isActive;

    // Variables
    private Vector3[] defaultPartPositions;

    private void OnEnable()
    {
        SubscribeEvents(true);
        
    }

    private void OnDisable()
    {
        SubscribeEvents(false);
    }

    void Awake()
    {
        defaultPartPositions = new Vector3[doorParts.Length];

        for (int i = 0; i < doorParts.Length; i++)
        {
            defaultPartPositions[i] = doorParts[i].localPosition;
        }
    }

    void Update()
    {
        SlideLeftRight(isActive);
    }

    #region ----- Functions -----

    void SlideLeftRight(bool isOpened)
    {
        for (int i = 0; i < doorParts.Length; i++)
        {
            Vector3 targetPosition = defaultPartPositions[i];

            if (isOpened)
                targetPosition.z += (i == 0 ? slideDistance : -slideDistance);

            doorParts[i].localPosition = Vector3.MoveTowards(
            doorParts[i].localPosition,
            targetPosition,
            openingSpeed * Time.deltaTime
            );
        }
    }
    

    /*---------------------------------------------*/

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
                return;
            }

            else
                this.isActive = true;
        }
    }

    /*---------------------------------------------*/

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

    #endregion
}
