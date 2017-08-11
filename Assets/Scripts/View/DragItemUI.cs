using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemUI : ItemUI {
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPostion(Vector2 postion)
    {
        transform.localPosition = postion;
    }

}
