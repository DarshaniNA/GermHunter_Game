using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    public float scrollSpeed = -2f;
    public float totalLength;
    public bool IsScrolling { set; get; }

    private float scrollLoction;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //IsScrolling = true;
    }

    private void Update()
    {
        if (!IsScrolling)
            return;
        scrollLoction += scrollSpeed * Time.deltaTime;
        Vector3 newLocation = (playerTransform.position.z + scrollLoction) * Vector3.forward;
        transform.position = newLocation;

        if (transform.GetChild(0).transform.position.z < playerTransform.position.z - Constant.DISTANCE_TO_RESPAWN)
        {
            transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);

            transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);
        }
    }
}
