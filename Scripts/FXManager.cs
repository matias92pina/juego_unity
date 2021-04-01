using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager obj;

    public GameObject pop;

    void Awake()
    {
        obj = this;
    }

    public void showPop(Vector3 pos)
    {
        pop.gameObject.GetComponent<Pop>().show(pos);
    }

    void OnDestroy()
    {
        obj = null;
    }
}
