using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] int objCount;
    [SerializeField] float endPos;
    [SerializeField] float speed;
    [SerializeField] float start;
    [SerializeField] GameObject obj;
    [SerializeField] Transform pos;
    [SerializeField] Transform startPos;
    [SerializeField] Transform end;
    [SerializeField] List<GameObject> objs;
    private bool condition;
    int currObj;

    private void Start()
    {
        objs = new List<GameObject>();
        currObj = 0;

        Search();
    }

    private void InstObj()
    {
        for (int i = 0; i < objCount; i++)
        {
            GameObject currObj = Instantiate(obj, pos.position, Quaternion.identity);
            currObj.SetActive(false);
            objs.Add(currObj);
        }
    }

    private void MoveObj()
    {
        objs[currObj].transform.position = Vector2.MoveTowards(objs[currObj].transform.position, end.position, Time.deltaTime * speed);

        if (currObj == objs.Count - 1)
        {
            currObj = 0;
        }
        else
        {
            currObj++;
        }
    }

    private void Deactive()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].transform.position.x < endPos)
            {
                objs[i].SetActive(false);
                objs[i].transform.position = startPos.position;

            }

            if (objs[i].transform.position.x < start)
            {
                objs[i].SetActive(true);
            }
        }
    }

    private void Search()
    {
        int len = 5;
        int b1 = Int32.MinValue;
        int b2 = Int32.MinValue;
        // int[] arr = { -2, -3, -4, -5, -6 };
        int[] arr = { 2, 3, 4, 5, 6 };

        for (int i = 0; i < len; i++)
        {
            if (b1 < arr[i])
            {
                b2 = b1;
                b1 = arr[i];
            }
            else if (b2 < arr[i] && arr[i] != b1)
            {
                b2 = arr[i];
            }
        }

        Debug.Log("B1 : " + b1);
        Debug.Log("B2 : " + b2);
    }
}
