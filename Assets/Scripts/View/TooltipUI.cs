using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {
    public Text outLine;
    public Text contentText;

    public void UpdateTooltip(string text)
    {
        outLine.text = text;
        contentText.text = text;
    }

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
