using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    
    public Text itemName;
    public void UpdateItem(string name)
    {
        itemName.text = name;
    }

    /*
     * public Image itemImage;
     * public void UpdateImage(Sprite s)
     * {
     *      itemImage = s;
     * }
     */
}
