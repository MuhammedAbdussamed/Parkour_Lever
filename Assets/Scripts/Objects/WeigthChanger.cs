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
        massText.text = 0.ToString();
    }

    private void OnCollisionEnter(Collision col)
    {
        currentItem = col;
        itemRb = col.gameObject.GetComponent<Rigidbody>();
        massText.text = itemRb.mass.ToString();
        // Debug.Log($"Giren item : {col.gameObject.name}");
    }

    private void OnCollisionExit(Collision col)
    {
        if (col == currentItem)
        {
            currentItem = null;
            massText.text = 0.ToString();
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
        { colRb.mass = (colRb.mass + 10f) - 50f; }

        else
        { colRb.mass += 10f; }

        massText.text = itemRb.mass.ToString();
    }

    #endregion
}
