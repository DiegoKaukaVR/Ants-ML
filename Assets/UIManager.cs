using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    TextMeshProUGUI _timeTxt;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void AddTimeScale(float value)
    {
        Debug.Log("Time.Scale");
        gameManager.TimeScale += value;
        _timeTxt.text = gameManager.TimeScale.ToString();
    }
}
