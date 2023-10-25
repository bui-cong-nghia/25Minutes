﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] protected Transform holder;
    public GameObject objectSpawn;

    public float timeDestroy;

    void Start()
    {
        //Destroy(this.gameObject, timeDestroy);

        //Vector2 pos = this.gameObject.transform.position;

        //GameObject spawn = (GameObject)Instantiate(objectSpawn);
        //spawn.transform.position = pos;
        StartCoroutine(DestroyAndSpawn());
    }

    //protected void Reset()
    //{
    //    this.LoadHolder();
    //}

    //protected void LoadHolder()
    //{
    //    if (this.holder != null) return;
    //    this.holder = transform.Find("Holder");
    //    Debug.Log(transform.name + ": LoadHolder", gameObject);
    //}
    IEnumerator DestroyAndSpawn()
    {
        yield return new WaitForSeconds(timeDestroy);  // Đợi thời gian timeDestroy r mới hủy

        Vector2 pos = transform.position;

        Destroy(gameObject);  // Hủy đối tượng hiện tại.

        GameObject spawn = Instantiate(objectSpawn);
        spawn.transform.position = pos;  // Đặt vị trí cho đối tượng mới.

        spawn.transform.parent = holder;
    }

}
