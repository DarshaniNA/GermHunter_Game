using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableList : MonoBehaviour
{
    public GameObject rowItemPrefab;
    public int itemCount = 10, columnCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rowRectTransform = rowItemPrefab.GetComponent<RectTransform>();
        RectTransform containRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item
        float width = containRectTransform.rect.width / columnCount;
        //float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        float scrollHeight = height * rowCount;
        //containRectTransform.offsetMin = new Vector2(containRectTransform.offsetMin.x, -scrollHeight );
        //containRectTransform.offsetMax = new Vector2(containRectTransform.offsetMax.x, scrollHeight);

        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(rowItemPrefab) as GameObject;
            newItem.name = gameObject.name + "item at (" + i + "," + j + ")";
            newItem.transform.parent = gameObject.transform;

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = -containRectTransform.rect.height / 2 + height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }
}
