using UnityEngine;

public abstract class Interactable_Objects : MonoBehaviour
{
    public bool isActivated { get; protected set; }     // Herkes okuyabilir. Sadece alt sınıflar değiştirebilir.

    protected virtual void BeActive()                   // Alt sınıflar bu fonksiyonu override etmek zorunda değillerdir. ( virtual )
    {
        isActivated = true;
    }

    protected virtual void DeActive()
    {
        isActivated = false;
    }
}
