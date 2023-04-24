using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float _timeScale = 1f;
    public float TimeScale
    {
        get { return _timeScale; }   
        set 
        {
            _timeScale = Mathf.Clamp(value, 0, 10);
            Time.timeScale = _timeScale;
        }  
    }
    public bool ShowDebug = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        _timeScale = 1;
    }

    bool toggleDebug;
    public void EnableAllTrails()
    {
        for (int i = 0; i < allAnts.Count; i++)
        {
            if (toggleDebug)
            {
                allAnts[i].traceGenerator.EnableTrails();
            }
            else
            {
                allAnts[i].traceGenerator.DisableTrails();
            }
        }

        if (toggleDebug)
        {
            toggleDebug = false;

        }
        else
        {
            toggleDebug = true;
        }
    }

    public List<Ant3D> allAnts;

}
