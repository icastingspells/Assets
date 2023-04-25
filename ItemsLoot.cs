using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsLoot : MonoBehaviour
{
   
    public Database database;
    public Item Loot;
    public GameObject lootItem;
    public Item droppedItem;
    public Bouce bounce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startLoot(){
        int randomNumber = Random.Range(2,10);
        for(int i = 0; i< randomNumber; i++){
        InstantiateLoot(transform.position, randomNumber);
         Debug.Log(droppedItem.name);
         
        }
        
    }
    public void InstantiateLoot(Vector3 spawnPosition, int rnd){
        droppedItem = GetDroppedItem(rnd);
        if(droppedItem != null){
            GameObject lootGameObject = Instantiate(lootItem, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.img;
            lootGameObject.GetComponent<Animator>().SetTrigger(droppedItem.name);
        }
    }

    Item GetDroppedItem(int randomNumber){
                    int rnd = randomNumber;
                    List<Item> possibleItems = new List<Item>();
                    foreach (Item item in database.items)
                    {
                        if(rnd <= item.dropChance)
                        {
                            possibleItems.Add(item);
                        }
                    }
                    if(database.items.Count > 0){
                        Item droppedItem = database.items[Random.Range(0, database.items.Count)];
                        return droppedItem;
                    }
                    return null;
                }

    

}
