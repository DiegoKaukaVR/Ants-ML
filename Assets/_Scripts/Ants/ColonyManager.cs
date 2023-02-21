using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColonyManager : MonoBehaviour
{
    public SCRColonyData colonyData;
    public Colony colony;
    private void Start()
    {
        colony = new Colony(colonyData);
    }
}

public class Colony
{
    [Header("Population")]
    public int idColony;
    public Character queen;
    public int maxPopulation;
    public List<Character> workers;

    public Colony(int idColony, Character queen, int maxPopulation, List<Character> workers)
    {
        this.idColony = idColony;
        this.queen = queen;
        this.maxPopulation = maxPopulation;
        this.workers = workers;
    }
    public Colony(SCRColonyData colonyData)
    {
        this.idColony = colonyData.idColony;
        this.queen = colonyData.queen;
        this.maxPopulation = colonyData.maxPopulation;
        this.workers = colonyData.workers;
    }
}

