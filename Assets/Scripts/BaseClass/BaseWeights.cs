using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class BaseWeights : BaseInteractable
{
    // Variables
    public bool isHolding;                          // Objenin "tutuluyor mu ?" sorusunu cevaplar
    private const int objectMovingSpeed = 10;

    // Components
    private Rigidbody rb;
    private AudioSource audio;

    // References
    private TextMeshPro massText;

    private void Awake()
    {
        // Component Assigns
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        // Reference Assigns
        massText = transform.Find("Mass_Text").GetComponent<TextMeshPro>();
    }

    public void Update()
    {
        massText.text = rb.mass.ToString();
    }

    private void LateUpdate()
    {
        CatchMousePosition();
    }

    private void OnCollisionEnter(Collision col)
    {
        PlayFallSound(col);
    }

    #region ----- Functions -----

    private void HoldItem()
    {
        isHolding = !isHolding;
        rb.isKinematic = isHolding;
    }

    /*-----------------------------------*/

    private void CatchMousePosition()               // Etkileşime girilen objenin ( Ağırlığın ) mouseu takip etmesini sağlar.
    {
        if (!isHolding) return;

        Transform cam = UnityEngine.Camera.main.transform;
        Vector3 targetPosition = cam.position + cam.forward * 2f;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * objectMovingSpeed
            );
    }

    /*-----------------------------------*/

    public override void InteractFunction()
    {
        HoldItem();
    }

    /*-----------------------------------*/

    private void PlayFallSound(Collision col)
    {
        if (col.relativeVelocity.magnitude > 2f)            // Çarpışmadan önce ki göreceli hız !!!
        {
            float audioPower = rb.mass / 100;
            audio.PlayOneShot(audio.clip, audioPower);
        }
    }

    #endregion
}

