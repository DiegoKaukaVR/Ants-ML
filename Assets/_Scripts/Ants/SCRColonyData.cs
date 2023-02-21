using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColonyDataScriptableObject", menuName = "ScriptableObjects/ColonyData", order = 1)]
public class SCRColonyData : ScriptableObject
{
    public int idColony;
    public Character queen;
    public int maxPopulation;
    public List<Character> workers;
}
