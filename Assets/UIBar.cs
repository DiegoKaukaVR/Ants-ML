using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBar : MonoBehaviour
{
    bool billboardOn;
    Transform cameraTransform;
    public bool showBar = true;

    public Image HealthBar;

    float porcentage;


    public void SetHPBar(float currentHP, float maxHP)
    {
        porcentage = currentHP / maxHP;
        HealthBar.fillAmount = porcentage;
    }

    private void Awake()
    {
        cameraTransform = Camera.main.transform;

        if (showBar)
        {
            EnableBar(true);
        }

        HealthBar = GetComponent<Image>();
    }

    public void EnableBar(bool value)
    {
        if (value)
        {
            billboardOn = true;
            StartCoroutine(Bilboard3DCoroutine());
        }
        else
        {
            billboardOn = false;
            StopCoroutine(Bilboard3DCoroutine());
        }

    }

    IEnumerator Bilboard3DCoroutine()
    {
        while (billboardOn)
        {
            //transform.LookAt(cameraTransform);

            transform.rotation = cameraTransform.rotation;
            yield return null;
        }
    }
}
