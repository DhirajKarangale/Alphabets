using UnityEngine;
using System.Collections.Generic;

public class Box : MonoBehaviour
{
    public List<Transform> itemList;
    private Vector3 firstItemPos;

    private void Start()
    {
        itemList = new List<Transform>();
    }

    private void OnEnable()
    {
        MoveItems.OnItemPlaced += ItemAdded;
        MoveItems.OnItemRemoved += ItemRemoved;
    }

    private void OnDesable()
    {
        MoveItems.OnItemPlaced -= ItemAdded;
        MoveItems.OnItemRemoved -= ItemRemoved;
    }

    private void ItemAdded(MoveItems item)
    {
        if (item.destinationPos.name == this.name)
        {
            Transform placedItem = itemList.Find((tempItem) => tempItem.name == item.name);
            if (!placedItem)
            {
                itemList.Add(item.transform);
                firstItemPos = itemList[0].position;
                if (itemList.Count == 2)
                {
                    itemList[0].transform.position = itemList[0].transform.position - new Vector3(0.65f, 0, 0);
                    itemList[1].transform.position = itemList[1].transform.position + new Vector3(0.65f, 0, 0);
                    itemList[0].transform.localScale = (0.43f) * Vector3.one;
                    itemList[1].transform.localScale = (0.43f) * Vector3.one;
                }
            }
        }
    }

    private void ItemRemoved(MoveItems item)
    {
        if (item.destinationPos.name == this.name)
        {
            Vector3 removedItemPos = Vector3.zero;
            Transform removedItem = itemList.Find((tempItem) => tempItem.name == item.name);
            if (removedItem)
            {
                item.transform.localScale = (0.5f) * Vector3.one;
                itemList.Remove(removedItem);
            }

            if (itemList.Count == 1)
            {
                itemList[0].localScale = (0.5f) * Vector3.one;
                itemList[0].position = firstItemPos;
            }
        }
    }
}
