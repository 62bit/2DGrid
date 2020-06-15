using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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

    //Min max used in methods called in Update()
    Vector2 _hotMin = new Vector2();
    Vector2 _hotMax = new Vector2();

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

        _hotMin = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        _hotMax = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        if(_hotMax != _max || _hotMin != _min)
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(_hotMin), Camera.main.ScreenToWorldPoint(_hotMax));
            foreach(var loc in locs)
            {
                if(!selectedBlocks.Contains(loc))
                {
                    selectedBlocks.Add(loc);
                }
            }
        }
        
    }

    private void ReleaseSelectionBox()
    {
        SelectionImage.gameObject.SetActive(false);
        _hotMin = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        _hotMax = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        if(Input.GetKey(KeyCode.S))
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(_hotMin), Camera.main.ScreenToWorldPoint(_hotMax));
            PlaceUnitController.PassToBuilders(locs);
        }
        else
        {
            foreach(var ob in objects)
            {
                Vector3 unitPos = Camera.main.WorldToScreenPoint(ob.transform.position);

                if(unitPos.x > _hotMin.x && unitPos.x < _hotMax.x && unitPos.y > _hotMin.y && unitPos.y < _hotMax.y)
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
        _hotMax = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);
        _hotMin = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);

        if(_hotMax != _max || _hotMin != _min)
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(_hotMin), Camera.main.ScreenToWorldPoint(_hotMax));

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

           
            _min = _hotMin;
            _max = _hotMax;
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
