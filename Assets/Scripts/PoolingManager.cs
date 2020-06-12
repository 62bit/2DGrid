using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager _instance;

    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _objectContainer;
    [SerializeField] public List<GameObject> _listOfObjects;

    private int counter = 0;

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
        for(int i = 0; i < numberOfBlocks; i++)
        {
            var nBlock = Instantiate(_block);
            nBlock.transform.name = "block " + (++counter).ToString();
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

        var tBlock = Instantiate(_block);
        tBlock.transform.name = "block " + (++counter).ToString();
        tBlock.SetActive(true);
        tBlock.transform.SetParent(_objectContainer.transform);
        _listOfObjects.Add(tBlock);
        return tBlock;
    }

    public void PutBactToPull(int numberOfBlocks)
    {
        //TODO : get blocks according to params then deactive
        for(int i = 0; i<numberOfBlocks; i++)
        {
            _listOfObjects[i].SetActive(false);
        }
    }
}
