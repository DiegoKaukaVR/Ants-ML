using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Information of the world
/// </summary>
public class PerceptionSystem : MonoBehaviour
{
    /// <summary>
    /// World State
    /// </summary>
    public List<Data> DataTable;
    [System.Serializable]
    public class Data
    {
        public string dataName;

        [Multiline]
        public string Description;

        /// <summary>
        /// Sets WorldPosition of something, may be an enemy target or the queen
        /// </summary>
        public Vector3 worldPosition;

        /// <summary>
        /// Collection of points that are created by quimical communicators
        /// </summary>
        public Vector3[] trace;

        /// <summary>
        /// Data implies danger?
        /// </summary>
        public bool danger = false;
    }

    public void DataRecord(Data newData)
    {
        DataTable.Add(newData);
    }



}
