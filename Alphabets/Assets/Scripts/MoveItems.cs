using System;
using UnityEngine;

public class MoveItems : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Transform destinationPos;
    public static Action<MoveItems> OnItemPlaced;
    public static Action<MoveItems> OnItemRemoved;

    private Vector3 resetPos;
    private float startPosX;
    private float startPosY;
    private bool isMoving;

    private void Start()
    {
        resetPos = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosX = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;
            isMoving = true;

            ItemHold();
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if ((Mathf.Abs(this.transform.position.x - destinationPos.position.x) <= 2.5f) &&
        (Mathf.Abs(this.transform.position.y - destinationPos.position.y) <= 2.5f))
        {
            this.transform.position = destinationPos.position + new Vector3(0, 0.463334f, 0);
            OnItemPlaced?.Invoke(this);
        }
        else
        {
            this.transform.position = resetPos;
            OnItemRemoved?.Invoke(this);
        }

        ItemRelease();
    }

    private void ItemHold()
    {
        transform.localScale = (0.6f) * Vector3.one;
        Color holdColor = spriteRenderer.color;
        holdColor.a = 0.8f;
        spriteRenderer.color = holdColor;
    }

    private void ItemRelease()
    {
        transform.localScale = (0.5f) * Vector3.one;
        Color holdColor = spriteRenderer.color;
        holdColor.a = 1f;
        spriteRenderer.color = holdColor;
    }
}
