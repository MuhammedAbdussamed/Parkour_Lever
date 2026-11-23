using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BaseWeights : MonoBehaviour, BaseInteractable
{
    // Variables
    public bool isHolding;                          // Objenin "tutuluyor mu ?" sorusunu cevaplar
    private const int objectMovingSpeed = 10;
    private float objScreenDistance;
    private float soundValueThreshold;
    private float soundValueMultiplier;

    // Components
    private Rigidbody weightRb;
    private AudioSource weightAudio;

    // References
    private TextMeshPro massText;

    private void Awake()
    {
        // Component Assigns
        weightRb = GetComponent<Rigidbody>();
        weightAudio = GetComponent<AudioSource>();

        // Reference Assigns
        massText = transform.Find("Mass_Text").GetComponent<TextMeshPro>();

        // Variables
        objScreenDistance = 2f;
        soundValueThreshold = 2f;
        soundValueMultiplier = 100f;
    }

    public void Update()
    {
        massText.text = weightRb.mass.ToString();
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
        weightRb.isKinematic = isHolding;
    }

    /*-----------------------------------*/

    private void CatchMousePosition()               // Etkileşime girilen objenin ( Ağırlığın ) mouseu takip etmesini sağlar.
    {
        if (!isHolding) return;

        Transform cam = UnityEngine.Camera.main.transform;
        Vector3 targetPosition = cam.position + cam.forward * objScreenDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * objectMovingSpeed
            );
    }

    /*-----------------------------------*/

    public void InteractFunction()
    {
        HoldItem();
    }

    /*-----------------------------------*/

    private void PlayFallSound(Collision col)
    {
        if (col.relativeVelocity.magnitude > soundValueThreshold)            // Çarpışmadan önce ki göreceli hız !!!
        {
            float audioPower = weightRb.mass / soundValueMultiplier;        // Sesi bu değişkenle küçülttük
            weightAudio.PlayOneShot(weightAudio.clip, audioPower);
        }
    }

    #endregion
}

