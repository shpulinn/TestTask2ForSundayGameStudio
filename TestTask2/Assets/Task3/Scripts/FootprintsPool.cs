using System.Collections.Generic;
using UnityEngine;

public class FootprintsPool : MonoBehaviour
{
    [SerializeField] private GameObject leftFootprintPrefab;
    [SerializeField] private GameObject rightFootprintPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private Transform poolObjectsParent;

    private List<GameObject> _leftPool;
    private List<GameObject> _rightPool;

    private void Start ()
    {
        // create a list for objects
        _leftPool = _rightPool = new List<GameObject>();

        // create "poolSize" amount of objects and add to list for right footprints
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(rightFootprintPrefab, poolObjectsParent);
            obj.gameObject.SetActive(false);
            _rightPool.Add(obj);
        }
        // create "poolSize" amount of objects and add to list for left footprints
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(leftFootprintPrefab, poolObjectsParent);
            obj.gameObject.SetActive(false);
            _leftPool.Add(obj);
        }
    }

    public GameObject GetLeftFootprint ()
    {
        // search for inactive object and return 
        foreach (GameObject obj in _leftPool) {
            if (!obj.activeInHierarchy) {
                obj.SetActive(true);
                return obj;
            }
        }

        // create a new object, if needed
        GameObject newObj = Instantiate(leftFootprintPrefab, poolObjectsParent);
        newObj.SetActive(true);
        _leftPool.Add(newObj);
        return newObj;
    }

    public GameObject GetRightFootprint()
    {
        // search for inactive object and return 
        foreach (GameObject obj in _rightPool) {
            if (!obj.activeInHierarchy) {
                obj.SetActive(true);
                return obj;
            }
        }

        // create a new object, if needed
        GameObject newObj = Instantiate(rightFootprintPrefab, poolObjectsParent);
        newObj.SetActive(true);
        _rightPool.Add(newObj);
        return newObj;
    }

    public void ReleaseObject (GameObject obj)
    {
        // deactivate object and place away from player 
        obj.SetActive(false);
        obj.transform.position = new Vector3(0, -100, 0);
    }
}
