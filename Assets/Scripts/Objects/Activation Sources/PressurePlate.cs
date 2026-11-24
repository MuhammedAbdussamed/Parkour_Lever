using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PressurePlate : BaseActivationSource
{
    [Header("Mass Activate Value")]
    [SerializeField] private int massActivateValue;
    [SerializeField] private CompareSituation compareSit;
    private TextMeshPro massText;

    // Components
    private MeshRenderer meshRenderer;
    private Material baseMaterial;

    // Veights
    private List<GameObject> weights = new List<GameObject>();
    private float weightSum;

    void Awake()
    {
        // Assigns
        meshRenderer = GetComponent<MeshRenderer>();
        massText = GetComponentsInChildren<TextMeshPro>(true)[0];       // Child objectlerde ki textmeshpro componentini içeren İLK objeyi al

        // Materials
        baseMaterial = meshRenderer.materials[1];
        baseMaterial.color = Color.red;

        massText.text = massActivateValue.ToString();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weight"))            // Objenin tagi ağırlıksa
        {
            weights.Add(col.gameObject);                    // Ağırlık listesine ekle
            CalculateSumWeight();                           // Ağırlık toplamını hesapla
            CheckActivateMass();                            // Ağırlık toplamına göre plakayı aç ya da kapat.
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (weights.Contains(col.gameObject))
        {
            weights.Remove(col.gameObject);
            CalculateSumWeight();
            CheckActivateMass();
        }
    }

    #region ----- Functions -----

    private bool CompareMass()
    {
        switch (compareSit)
        {
            case CompareSituation.GreaterThan:
                return weightSum > massActivateValue;
                
            case CompareSituation.LessThan:
                return weightSum < massActivateValue;

            case CompareSituation.Equal:
                return weightSum == massActivateValue;

            case CompareSituation.GreaterThanOrEqual:
                return weightSum >= massActivateValue;

            case CompareSituation.LessThanOrEqual:
                return weightSum <= massActivateValue;

            default:
                return false;
        }
    }

    /*------------------------------*/

    private void CheckActivateMass()
    {
        if (CompareMass() && !isActive)
        {
            TurnOn();
            baseMaterial.color = Color.green;
        }
        else if (!CompareMass() && isActive)
        {
            TurnOff();
            baseMaterial.color = Color.red;
        }
    }

    /*------------------------------*/

    private void CalculateSumWeight()
    {
        weightSum = 0f;

        for (int i = 0; i < weights.Count; i++)
        {
            weightSum += weights[i].GetComponent<Rigidbody>().mass;
        }
    }

    /*------------------------------*/

    public override void TurnOff()
    {
        isActive = false;
        InvokePowerEvent();
    }

    /*-----------------------------*/

    private enum CompareSituation
    {
        GreaterThan,
        LessThan,
        Equal,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

    #endregion
}
