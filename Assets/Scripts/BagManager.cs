using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BagManager : MonoBehaviour {

    public static BagManager _instance;
    private Dictionary<int, Item> ItemList = new Dictionary<int, Item>();

    public GridPanelUI gridPanelUI;
    public TooltipUI tooltipUI;
    private bool isShow = false;
    private bool isDrag = false;
    public DragItemUI dragItemUI; 
    private void Awake()
    {
        _instance = this;
        Load();
        GridUI.OnEnter += GridUI_OnEnter;
        GridUI.OnExit += GridUI_OnExit;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;


    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out position);

        if (isDrag)
        {
            dragItemUI.Show();
            dragItemUI.SetLocalPostion(position);
        }
        else if (isShow)
        {
            tooltipUI.Show();
            tooltipUI.SetLocalPostion(position);
        }

    }

    public void StoreItem(int ID)
    {
        if (!ItemList.ContainsKey(ID))
            return;
       
        Transform emptyGrid = gridPanelUI.GetEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.Log("Full");
            return;
            
        }
        Item temp = ItemList[ID];
        this.CreatNewItem(temp, emptyGrid);
        

        
    }
    //取数据
    private void Load()
    {
        ItemList = new Dictionary<int, Item>();
        Weapon w1 = new Weapon(1, "牛刀", "杀牛刀", 20, 10, "", 100);
        Weapon w2 = new Weapon(2, "羊刀", "杀羊刀", 50, 20, "", 200);
        Weapon w3 = new Weapon(3, "猪刀", "杀猪刀", 100, 80, "", 300);

        Consumable c1 = new Consumable(4, "红瓶", "回血", 20, 10, "", 100, 0);
        Consumable c2 = new Consumable(5, "蓝瓶", "回蓝", 20, 10, "", 0, 100);

        Armor a1 = new Armor(6, "头盔", "保护头部", 40, 25, "", 5, 40, 1);
        Armor a2 = new Armor(7, "护肩", "保护肩部", 45, 25, "", 5, 40, 1);

        ItemList.Add(w1.ItemID, w1);
        ItemList.Add(w2.ItemID, w2);
        ItemList.Add(w3.ItemID, w3);
        ItemList.Add(c1.ItemID, c1);
        ItemList.Add(c2.ItemID, c2);
        ItemList.Add(a1.ItemID, a1);
        ItemList.Add(a2.ItemID, a2);
    }

    private void GridUI_OnEnter(Transform gridTransform)
    {

        Item item = ItemModel.GetItem(gridTransform.name);
        if (item == null)
            return;
        string text = GetTooltipText(item);
        tooltipUI.UpdateTooltip(text);
        isShow = true;
    }

    private void GridUI_OnExit()
    {
        tooltipUI.Hide();
        isShow = false;
    }

    private void GridUI_OnLeftBeginDrag(Transform gridTransform)
    {
        if (gridTransform.childCount == 0)
            return;
        else
        {
            Item item = ItemModel.GetItem(gridTransform.name);
            dragItemUI.UpdateItem(item.Name);
            Destroy(gridTransform.GetChild(0).gameObject);

            isDrag = true;
        }
    }

    private void GridUI_OnLeftEndDrag(Transform oldTransform, Transform enterTransform)
    {
        isDrag = false;
        dragItemUI.Hide();
        if (enterTransform == null)
        {
            ItemModel.DeleteItem(oldTransform.name);
        }
        else if (enterTransform.tag == "Grid")
        {
            if (enterTransform.childCount == 0)
            {
                Item item = ItemModel.GetItem(oldTransform.name);
                CreatNewItem(item, enterTransform);
                ItemModel.DeleteItem(oldTransform.name);
            }
            else
            {
                Destroy(enterTransform.GetChild(0).gameObject);

                Item oldGridItem = ItemModel.GetItem(oldTransform.name);
                Item enterGridItem = ItemModel.GetItem(enterTransform.name);

                this.CreatNewItem(oldGridItem, enterTransform);
                this.CreatNewItem(enterGridItem, oldTransform);

            }
        }
        else
        {
            Item item = ItemModel.GetItem(oldTransform.name);
            CreatNewItem(item, enterTransform);
        }
        

    }


    private string GetTooltipText(Item item)
    {
        if (item == null)
            return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        switch (item.ItemType)
        {
            case "Armor":
                Armor armor = item as Armor;
                sb.AppendFormat("力量:{0}\n防御:{1}\n敏捷:{2}\n\n", armor.Power, armor.Defend, armor.Agility);
                break;
            case "Consumable":
                Consumable consumable = item as Consumable;
                sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consumable.BackHp, consumable.BackMp);
                break;
            case "Weapon":
                Weapon weapon = item as Weapon;
                sb.AppendFormat("攻击:{0}\n\n", weapon.Damage);
                break;
        }
        sb.AppendFormat("<size=16><color=white>购买价格：{0}\n出售价格：{1}</color></size>\n\n<color=yellow><size=12>描述：{2}</size></color>",
            item.BuyPrice, item.SellPrice, item.Description);
        return sb.ToString();
    }

    private void CreatNewItem(Item item,Transform parent)
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        itemPrefab.GetComponent<ItemUI>().UpdateItem(item.Name);
        GameObject itemGo = GameObject.Instantiate(itemPrefab);
        itemGo.transform.SetParent(parent);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        ItemModel.StoreItem(parent.name, item);
    }
}