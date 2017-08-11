using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPanelUI : MonoBehaviour {

    public Transform[] girds;
    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < girds.Length; i++)
        {
            if (girds[i].childCount == 0)
            {
                return girds[i];
            }
        }
        return null;
    }

}
