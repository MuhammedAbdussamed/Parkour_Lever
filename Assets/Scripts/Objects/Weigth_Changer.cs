using TMPro;
using UnityEngine;

public class Weigth_Changer : Interactable_Objects
{
    [Header("Mass Value Screen text")]
    [SerializeField] private TextMeshPro massText;
    internal Collision currentItem;
    internal Rigidbody itemRb;

    private void OnCollisionEnter(Collision col)
    {
        currentItem = col;
        itemRb = col.gameObject.GetComponent<Rigidbody>();
        Print_Weight.PrintMass(massText, itemRb.mass);
        Debug.Log($"Giren item : {col.gameObject.name}");
    }

    private void OnCollisionExit(Collision col)
    {
        if (col == currentItem)
        {
            currentItem = null;
            Print_Weight.PrintMass(massText, 0);
        }

        Debug.Log($"Çıkan item : {col.gameObject.name}");
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
            Print_Weight.PrintMass(massText, itemRb.mass);
        }
        else
        {
            colRb.mass += 10f;
            Print_Weight.PrintMass(massText, itemRb.mass);
            Debug.Log($"İtemin ağırlığı 10 arttırıldı. Şimdi ki ağırlık {colRb.mass}");
        }
    }
}
