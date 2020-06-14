using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager _instance;

    [SerializeField] private GameObject _block;
    [SerializeField] private GameObject _tempSelectionBlock;
    [SerializeField] private GameObject _tempSelectionBlockContainer;
    [SerializeField] private GameObject _objectContainer;

    [SerializeField] private List<GameObject> _listOfTreeObjects;
    [SerializeField] private List<GameObject> _listOfSelectionObjects;

    private List<GameObject> _listAlreadyActiveSelectionObjts;

    private int _selection_object_count = 0;


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

    public void GenerateBlocks(int numberOfBlocks, int numberOfSelectionBlock)
    {
        for(int i = 0; i < numberOfBlocks; i++)
        {
            var nBlock = Instantiate(_block);
            nBlock.transform.name = "block " + (++counter).ToString();
            nBlock.transform.SetParent(_objectContainer.transform);
            nBlock.SetActive(false);
            _listOfTreeObjects.Add(nBlock);
        }


        for(int i = 0; i < numberOfSelectionBlock; i++)
        {
            var tBlock = Instantiate(_tempSelectionBlock);
            tBlock.transform.name = "selectionBlock" + (++_selection_object_count).ToString();
            tBlock.SetActive(false);
            tBlock.transform.SetParent(_tempSelectionBlockContainer.transform);
            _listOfSelectionObjects.Add(tBlock);

        }

    }

    public GameObject RequestBlock()
    {

        foreach(var block in _listOfTreeObjects)
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
        _listOfTreeObjects.Add(tBlock);
        return tBlock;
    }

    public void RequestSelectionBlock(Vector2 loc)
    {
        foreach(var block in _listOfSelectionObjects)
        {
            if(!block.activeInHierarchy)
            {
                block.SetActive(true);
                block.transform.position = loc;
                return;
            }

        }

        var b = Instantiate(_tempSelectionBlock);
        b.transform.position = loc;
        b.transform.SetParent(_tempSelectionBlockContainer.transform);
        b.transform.name = "selectionBlock" + (++_selection_object_count).ToString();
        _listOfSelectionObjects.Add(b);

    }



    public void DeactivateSelectionBlocs()
    {
        //Fix this 
        foreach(var block in _listOfSelectionObjects)
        {
            block.SetActive(false);
        }
    }


}
