using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
 
public List<ItemInventory> items = new List<ItemInventory>(); 
public GameObject gameObjShow;
public GameObject InventoryMainObject;
public Database database;
public int maxCount =32;
public Camera cam;
public EventSystem evesys;
public int currentID;
public ItemInventory currentItem;
public Transform movingObject;
public Vector3 offset;
public GameObject backGround;


public void Start(){
    if (items.Count == 0){
        AddGraphics();
    }
    UpdateInventory();
} 
public void Update(){
    if ( currentID != -1){
        MoveObject();
    }
    if (Input.GetKeyDown(KeyCode.E)){
        backGround.SetActive(!backGround.activeSelf);
        if(backGround.activeSelf){
            UpdateInventory();
        }
    }
}

public void SearchForSameItem(Item item, int count){
    for(int i = 0; i < maxCount; i++){
        if (items[i].id == item.id){
            if(items[i].count < 128){
                items[i].count += count;

                if(items[i].count > 128){
                    count = items[i].count - 128;
                    items[i].count = 64;

                }
                else {
                    count = 0;
                    i = maxCount;
                }
            }
        }
    }
    if (count > 0){
        for (int i = 0;i <maxCount; i++ ){
            if (items[i].id == 0){
                AddItem(i, item, count);
                i = maxCount;
            }
        }
    }
}

public void AddItem(int id, Item item, int count)
{
    items[id].id = item.id;
    items[id].count = count;
    items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

    if (count > 1 && item.id != 0){
        items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();    
    }
    else 
    {
        items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    }
}


public void AddInventoryItem(int id, ItemInventory invItem)
{
    items[id].id = invItem.id;
    items[id].count = invItem.count;
    items[id].itemGameObj.GetComponent<Image>().sprite = database.items[invItem.id].img;

    if (invItem.count > 1 && invItem.id != 0){
        items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();    
    }
    else 
    {
        items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    }
}
 public void AddGraphics()
 {
    for (int i = 0; i < maxCount; i++){
        GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

        newItem.name = i.ToString();

        ItemInventory ii = new ItemInventory();
        ii.itemGameObj = newItem;
        Transform rt = newItem.GetComponent<Transform>();
        rt.localPosition = new Vector3(0,0,0);
        rt.localScale = new Vector3(1,1,1);
        newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1,1,1);

        Button tempButton = newItem.GetComponent<Button>();
        tempButton.onClick.AddListener(delegate{SelectObject();});
        items.Add(ii);
    }
 }

public void UpdateInventory(){
    for(int i = 0; i < maxCount; i++){

        if (items[1].id != 0 && items[i].count > 1){            
    
            items[1].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
        }
        else{
            items[1].itemGameObj.GetComponentInChildren<Text>().text = "";
        }

        items[i].itemGameObj.GetComponent<SpriteRenderer>().sprite = database.items[items[i].id].img;
    }

}
public void SelectObject(){
    if (currentID == -1){
        currentID = int.Parse(evesys.currentSelectedGameObject.name);
        currentItem = CopyInventoryItem(items[currentID]);
        movingObject.gameObject.SetActive(true);
        movingObject.GetComponent<Image>().sprite = database.items[currentItem.id].img;

        AddItem(currentID, database.items[0], 0);
    }
    else 
    {
    ItemInventory II = items[int.Parse(evesys.currentSelectedGameObject.name)];
    if(currentItem.id != II.id){
    AddInventoryItem(currentID, II);
    AddInventoryItem(int.Parse(evesys.currentSelectedGameObject.name), currentItem);
    }
    else 
    {
        if(II.count + currentItem.count <= 128){
            II.count += currentItem.count;
        }
        else 
        {
            AddItem(currentID, database.items[II.id], II.count + currentItem.count - 128);
            II.count = 128;
        }
        II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
    }
    currentID = -1;
    movingObject.gameObject.SetActive(false);
    }
}

 public void MoveObject(){
    Vector3 pos = Input.mousePosition + offset;
    pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
    movingObject.position = cam.ScreenToWorldPoint(pos);
}
public ItemInventory CopyInventoryItem(ItemInventory old){
    ItemInventory New = new ItemInventory();
    New.id = old.id;
    New.itemGameObj = old.itemGameObj;
    New.count  = old.count;
    return New;
}
 
}

[System.Serializable]

public class ItemInventory{
    public int id;
    public GameObject itemGameObj;

    public int count;
}