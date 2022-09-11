using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;

    private List<GameObject> _poolObjects;
    private GameObject _poolContainer;


    // Start is called before the first frame update
    void Start()
    {
        _poolContainer = new GameObject ("Pooler " + objectPrefab);
        CreatePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreatePool()
    {
        _poolObjects = new List<GameObject>();

        for(int i = 0 ; i < poolSize ; i++)
        {
            AddObjectToPool();
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(false);
        newObj.transform.SetParent(_poolContainer.transform);

        _poolObjects .Add(newObj);
        return newObj;
    }



    public GameObject GetObjectToPool()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if( !_poolObjects[i].activeInHierarchy)
            {
                return _poolObjects[i];
            }
        }

        if(poolCanExpand)
        {
            return AddObjectToPool();
        }
        
        return null;
    }






}
