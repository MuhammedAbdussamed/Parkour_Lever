using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class PressurePlate : BaseActivatable
{
    [Header("Mass Activate Value")]
    [SerializeField] private int massActivateValue;
    private TextMeshPro massText;

    // Components
    private MeshRenderer meshRenderer;
    private Collision? currentCol;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        massText = GetComponentsInChildren<TextMeshPro>(true)[0];       // Child objectlerde ki textmeshpro componentini içeren İLK objeyi al
        massText.text = massActivateValue.ToString();
    }

    void OnCollisionEnter(Collision col)
    {
        currentCol = col;
    }

    private void OnCollisionStay(Collision col)
    {
        StayActive();
    }

    void OnCollisionExit(Collision col)
    {
        if (col == currentCol)
        {
            currentCol = null;
            TurnOff();
            meshRenderer.material.color = Color.yellow;
            Debug.Log("Basınç plakası deaktif !");
        }
    }

    #region ----- Functions -----


    private void StayActive()
    {
        if (currentCol == null)
            return;

        Rigidbody colRb = currentCol.gameObject.GetComponent<Rigidbody>();

        if (colRb.mass > massActivateValue)
        {
            TurnOn();
            meshRenderer.material.color = Color.green;
            Debug.Log("Basınç Plakası Aktif !");
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
