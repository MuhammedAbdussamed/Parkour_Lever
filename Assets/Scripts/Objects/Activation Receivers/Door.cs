using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseActivationReceivers
{
    [Header("Door Parts")]
    [SerializeField] private Transform[] doorParts;

    [Header("Door Stats")]
    [SerializeField] private float slideDistance;
    private float openingSpeed;

    // Variables
    private Vector3[] defaultPartPositions;

    void Awake()
    {
        openingSpeed = 2.5f;

        defaultPartPositions = new Vector3[doorParts.Length];

        for (int i = 0; i < doorParts.Length; i++)
        {
            defaultPartPositions[i] = doorParts[i].localPosition;
        }
    }

    void Update()
    {
        ActivateFunction();
    }

    #region ----- Functions -----

    protected override void ActivateFunction()
    {
        SlideLeftRight(isActive);
    }

    /*---------------------------------------------*/

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

    #endregion
}
