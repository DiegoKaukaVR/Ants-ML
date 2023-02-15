 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
using TMPro;
 
 public class FPSdisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;
    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}