using TMPro;
using UnityEngine;

public class WeigthChanger : BaseInteractable
{
    [Header("Mass Value Screen text")]
    [SerializeField] private TextMeshPro massText;
    private Collision currentItem;
    private Rigidbody itemRb;

    void Awake()
    {
        PrintWeight.PrintMass(massText, 0f);
    }

    private void OnCollisionEnter(Collision col)
    {
        currentItem = col;
        itemRb = col.gameObject.GetComponent<Rigidbody>();
        PrintWeight.PrintMass(massText, itemRb.mass);
        // Debug.Log($"Giren item : {col.gameObject.name}");
    }

    private void OnCollisionExit(Collision col)
    {
        if (col == currentItem)
        {
            currentItem = null;
            PrintWeight.PrintMass(massText, 0);
        }

        // Debug.Log($"Çıkan item : {col.gameObject.name}");
    }

    public override void InteractFunction()
    {
        ChangeWeight(itemRb);
    }

    #region ----- Functions -----

    public virtual void ChangeWeight(Rigidbody colRb)
    {
        if (colRb.mass + 10f > 50f)
        {
            colRb.mass = (colRb.mass + 10f) - 50f;
            PrintWeight.PrintMass(massText, itemRb.mass);
        }
        else
        {
            colRb.mass += 10f;
            PrintWeight.PrintMass(massText, itemRb.mass);
            // Debug.Log($"İtemin ağırlığı 10 arttırıldı. Şimdi ki ağırlık {colRb.mass}");
        }
    }

    #endregion
}
