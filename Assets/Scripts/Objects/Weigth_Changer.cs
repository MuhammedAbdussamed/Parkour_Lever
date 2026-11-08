using TMPro;
using UnityEngine;

public class Weigth_Changer : Interactable_Objects
{
    internal TextMeshPro massText;
    internal Collider currentItem;
    internal Rigidbody itemRb;

    private void OnTriggerEnter(Collider col)
    {
        currentItem = col;
        itemRb = col.GetComponent<Rigidbody>();
        Print_Weight.PrintMass(massText, itemRb);
        Debug.Log($"Giren item : {col.name}");
    }

    private void OnTriggerExit(Collider col)
    {
        if (col = currentItem)
        {
            currentItem = null;
        }

        Debug.Log($"Çıkan item : {col.name}");
    }

    private void Awake()
    {
        massText = transform.Find("Mass_Value").GetComponent<TextMeshPro>();
    }

    public override void InteractFunction()
    {
        ChangeWeight(itemRb);
    }

    public virtual void ChangeWeight(Rigidbody colRb)
    {
        if (colRb.mass + 10f > 50f)
        {
            colRb.mass = (colRb.mass + 10f) - 50f;
            Print_Weight.PrintMass(massText, itemRb);
        }
        else
        {
            colRb.mass += 10f;
            Print_Weight.PrintMass(massText, itemRb);
            Debug.Log($"İtemin ağırlığı 10 arttırıldı. Şimdi ki ağırlık {colRb.mass}");
        }
    }
}
