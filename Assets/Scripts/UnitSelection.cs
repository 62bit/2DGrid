using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UnitSelection : MonoBehaviour
{
    public static UnityAction StartBuilders;

    [SerializeField] private RectTransform SelectionImage;
    private Vector2 startPos;
    private List<GameObject> objects;
    private Stack<GameObject> selectedUnits;
    private PlaceUnitController placeUnitController;

    private bool isSelectionObjectsPlaced = false;

    
    private List<Vector2> selectedBlocks = new List<Vector2>();

    //Min Max from prewious frame
    private Vector2 _min = new Vector2();
    private Vector2 _max = new Vector2();

    //Placed Selection Blocks
    private List<Vector2> _selectionBlocks = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {

        placeUnitController = GameObject.FindWithTag("Placer").GetComponent<PlaceUnitController>();
        selectedUnits = new Stack<GameObject>();
        objects = GameObject.FindWithTag("Placer").GetComponent<PlaceUnitController>().objectList;
        SelectionImage.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            StartSelection(Input.mousePosition);
            OnSelection();
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PoolingManager.Instance.RequestBlock();
        }

        //if(Input.GetMouseButtonDown(1))
        //{
        //    List<Vector2> locs = new List<Vector2> { new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f) };
        //    PoolingManager.Instance.RequestSelectionBlock(locs);
        //}
    }

    private void StartSelection(Vector2 currentMousePos)
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits.Pop().GetComponent<SpriteRenderer>().color = Color.white;
        }

        if(!SelectionImage.gameObject.activeInHierarchy)
            SelectionImage.gameObject.SetActive(true);

        float widgt = currentMousePos.x - startPos.x;
        float heigth = currentMousePos.y - startPos.y;

        SelectionImage.anchoredPosition = startPos + new Vector2(widgt / 2, heigth / 2);

        SelectionImage.sizeDelta = new Vector2(Mathf.Abs(widgt), Mathf.Abs(heigth));

        Vector2 min = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        Vector2 max = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(min), Camera.main.ScreenToWorldPoint(max));
        foreach(var loc in locs)
        {
            if(!selectedBlocks.Contains(loc))
            {
                selectedBlocks.Add(loc);
            }
        }
    }

    private void ReleaseSelectionBox()
    {
        SelectionImage.gameObject.SetActive(false);
        Vector2 min = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        Vector2 max = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        if(Input.GetKey(KeyCode.S))
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(min), Camera.main.ScreenToWorldPoint(max));
            PlaceUnitController.PassToBuilders(locs);
        }
        else
        {
            foreach(var ob in objects)
            {
                Vector3 unitPos = Camera.main.WorldToScreenPoint(ob.transform.position);

                if(unitPos.x > min.x && unitPos.x < max.x && unitPos.y > min.y && unitPos.y < max.y)
                {
                    selectedUnits.Push(ob);
                    ob.GetComponent<SpriteRenderer>().color = new Color(0, 250, 248);
                }
            }
        }

        selectedBlocks.Clear();
        _selectionBlocks.Clear();
        PoolingManager.Instance.DeactivateSelectionBlocks();

    }

    private void OnSelection()
    {
        Vector2 min = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        Vector2 max = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        if(max != _max || min != _min)
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(min), Camera.main.ScreenToWorldPoint(max));

            if(_selectionBlocks != locs)
            {
                if(_selectionBlocks.Count < locs.Count)
                {
                    foreach(var block in locs.Except(_selectionBlocks).ToList())
                    {
                        PoolingManager.Instance.RequestSelectionBlock(block);
                        _selectionBlocks.Add(block);

                    }
                }
                else
                {
                    foreach(var block in _selectionBlocks.Except(locs).ToList())
                    {
                        PoolingManager.Instance.PutBackSelectionBlock(block);
                        _selectionBlocks.Remove(block);

                    }
                }
                PaintSelectionBlocks(Grid.Instance.ValidateArea(locs));
            }

           
            _min = min;
            _max = max;
        }

    }


    private void PaintSelectionBlocks(bool check)
    {
        if(!check)
        {
            foreach(var o in PoolingManager.Instance._listAlreadyActiveSelectionObjts)
            {
                o.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            foreach(var o in PoolingManager.Instance._listAlreadyActiveSelectionObjts)
            {
                o.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
