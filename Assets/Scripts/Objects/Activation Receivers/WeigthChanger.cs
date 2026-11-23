using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class WeigthChanger : BaseActivationReceivers, BaseInteractable
{
    [Header("Mass Value Screen text")]
    [SerializeField] private TextMeshPro massText;

    // Components
    private GameObject currentItem;
    private Rigidbody itemRb;

    // Variables
    private int massAddingValue;
    private int maxMass;

    private void Awake()
    {
        massAddingValue = 10;
        maxMass = 50;
    }

    private void Start()
    {
        SetActivate();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!isActive) { return; }

        if (!col.gameObject.CompareTag("Weight")) { return; }

        GetItem(col);
    }

    private void OnCollisionExit(Collision col)
    {
        if (!isActive) { return; }

        if (!col.gameObject.CompareTag("Weight")) { return; }

        CleanItem(col);
    }

    #region ----- Functions -----

    public virtual void ChangeWeight(Rigidbody colRb)
    {
        if (colRb.mass + massAddingValue > maxMass)
        { colRb.mass = (colRb.mass + massAddingValue) - maxMass; }

        else
        { colRb.mass += massAddingValue; }

        massText.text = colRb.mass.ToString();
    }

    /*----------------------------*/

    private void GetItem(Collision col)
    {
        currentItem = col.gameObject;
        itemRb = col.gameObject.GetComponent<Rigidbody>();
        PrintMassToScreen();
    }

    /*---*/

    private void CleanItem(Collision col)
    {
        if (col.gameObject == currentItem)
        {
            currentItem = null;
            itemRb = null;
            CleanScreen();
        }
    }

    /*---------------------------*/

    private void PrintMassToScreen()
    {
        massText.text = itemRb.mass.ToString();
    }

    /*---*/

    private void CleanScreen()
    {
        massText.text = string.Empty;
    }

    /*---------------------------*/

    public void InteractFunction()
    {
        if(itemRb == null) { return; }                  // Null check. İtemin rigidbodysi eksikse returnle

        ChangeWeight(itemRb);
    }

    /*---------------------------*/

    private void SetActivate()
    {
        if (activationSources.Count <= 0)
        {
            isActive = true;
        }
    }

    #endregion
}
