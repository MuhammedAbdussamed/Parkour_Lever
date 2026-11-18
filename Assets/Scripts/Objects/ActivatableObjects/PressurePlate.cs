using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PressurePlate : BaseActivatable
{
    [Header("Mass Activate Value")]
    [SerializeField] private int massActivateValue;
    private TextMeshPro massText;

    // Components
    private MeshRenderer meshRenderer;

    // Veights
    private List<GameObject> weights = new List<GameObject>();
    public float weightSum;

    void Awake()
    {
        // Assigns
        meshRenderer = GetComponent<MeshRenderer>();
        massText = GetComponentsInChildren<TextMeshPro>(true)[0];       // Child objectlerde ki textmeshpro componentini içeren İLK objeyi al

        massText.text = massActivateValue.ToString();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weight"))            // Objenin tagi ağırlıksa
        {
            weights.Add(col.gameObject);                               // Ağırlık listesine ekle
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


    private void CheckActivateMass()
    {
        if (weightSum >= massActivateValue && !isActive)
        {
            TurnOn();
            meshRenderer.material.color = Color.green;
            Debug.Log("Basınç Plakası Açık !");
        }
        else if (weightSum < massActivateValue && isActive)
        {
            TurnOff();
            meshRenderer.material.color = Color.red;
            Debug.Log("Basınç Plakası Kapalı !");
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
    
    #endregion
}
