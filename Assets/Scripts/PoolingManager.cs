using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager _instance;

    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _objectContainer;
    [SerializeField] private List<GameObject> _listOfObjects;

    public static PoolingManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.Log("Something went wrong");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    public void GenerateBlocks(int numberOfBlocks)
    {
        for(int i= 0; i<numberOfBlocks; i++)
        {
            var nBlock = Instantiate(_block);
            nBlock.transform.SetParent(_objectContainer.transform);
            nBlock.SetActive(false);
            _listOfObjects.Add(nBlock);
        }
     
    }

    public GameObject RequestBlock()
    {
        foreach(var block in _listOfObjects)
        {
            if(block.activeInHierarchy == false)
            {
                block.SetActive(true);
                return block;
            }

        }
        return null;
    }
}
