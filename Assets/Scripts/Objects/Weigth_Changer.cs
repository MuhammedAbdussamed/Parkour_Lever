using UnityEngine;

public class Weigth_Changer : Interactable_Objects
{
    internal Collider currentItem;
    internal Rigidbody itemRb;

    private void OnTriggerEnter(Collider col)
    {
        currentItem = col;
        itemRb = col.GetComponent<Rigidbody>();
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

    public override void InteractFunction()
    {
        ChangeWeight(itemRb);
    }

    public virtual void ChangeWeight(Rigidbody colRb)
    {
        if (colRb.mass + 10f > 50f)
        {
            colRb.mass = (colRb.mass + 10f) - 50f;
        }
        else
        {
            colRb.mass += 10f;
            Debug.Log($"İtemin ağırlığı 10 arttırıldı. Şimdi ki ağırlık {colRb.mass}");
        }
    }
}
