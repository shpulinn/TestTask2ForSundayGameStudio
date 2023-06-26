using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private ArrowProjectile prefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform poolObjectsParent;

    private List<ArrowProjectile> pool;

    private void Start ()
    {
        // create a list for objects
        pool = new List<ArrowProjectile>();

        // create "poolSize" amount of objects and add to list
        for (int i = 0; i < poolSize; i++) {
            ArrowProjectile obj = Instantiate(prefab, poolObjectsParent);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public ArrowProjectile GetObject ()
    {
        // search for inactive object and return 
        foreach (ArrowProjectile obj in pool) {
            if (!obj.gameObject.activeInHierarchy) {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // create a new object, if needed
        ArrowProjectile newObj = Instantiate(prefab, poolObjectsParent);
        newObj.gameObject.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void ReleaseObject (GameObject obj)
    {
        // deactivate object and place away from player 
        obj.SetActive(false);
        obj.transform.position = new Vector3(0, -100, 0);
    }
}
