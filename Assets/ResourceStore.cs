using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStore : MonoBehaviour
{
    public List<Resource> listResource = new List<Resource>();
    public Resource[] resourceType;

    public List<int> quantityResourceList = new List<int>();
    public int[] quantityResourcesArray = { 1, 3, 2, 3 };

    public int index1;
    public int index2;

    public List<ResourceManager> resourceListCreator = new List<ResourceManager>();
    [System.Serializable]
    public struct ResourceManager
    {
        public Resource.ResourceType resourcetype;
        public int quantity;
    }
   

    private void Start()
    {

        for (int i = 0; i < resourceListCreator.Count; i++)
        {
            Resource resource = new Resource(resourceListCreator[i].resourcetype, resourceListCreator[i].quantity);
            listResource.Add(resource);
        }


        listResource.AddRange(resourceType);
        quantityResourceList.AddRange(quantityResourcesArray);

        index1 = listResource.BinarySearch(resourceType[0]);
        index2 = quantityResourceList.BinarySearch(2);

        Debug.Log(index1);
        Debug.Log(index2);
        Debug.Log(listResource.Count);


    }
}
