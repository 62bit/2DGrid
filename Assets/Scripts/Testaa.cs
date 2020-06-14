using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testaa : MonoBehaviour
{
    List<int> a = new List<int>();
    List<int> b = new List<int>{ 1, 2, 3, 4, 5, 6 };

    // Start is called before the first frame update
    void Start()
    {
        List<int> c = b.Except(a).ToList();
        foreach(var v in c)
        {
            Debug.Log(v);
        }
    }

}
