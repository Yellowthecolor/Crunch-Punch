using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField] GameObject ItemDropPrefab;
    [SerializeField] List<PowerUp> itemList = new List<PowerUp>();

    PowerUp GetItemDrop(){
        int randItemPicker = Random.Range(1, 101);
        List<PowerUp> possibleItemDrops = new List<PowerUp>();
        foreach(PowerUp powerUp in itemList){
            if (randItemPicker <= powerUp.GetDropChance()){
                possibleItemDrops.Add(powerUp);
            }
        }

        if(possibleItemDrops.Count > 0){
            PowerUp itemToDrop = possibleItemDrops[Random.Range(0, possibleItemDrops.Count)];
            return itemToDrop;
        }
        return null;
    }

    public void InstantiateDrop(Vector3 spawn){
        PowerUp itemDrop = GetItemDrop();
        if (itemDrop != null){
            GameObject item = Instantiate(ItemDropPrefab, spawn, Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = itemDrop.GetItemSprite();

            PickUp pickUpComponent = item.GetComponent<PickUp>();
            if (pickUpComponent != null){
                pickUpComponent.SetPowerUp(itemDrop);
            }
        }
    }
}
