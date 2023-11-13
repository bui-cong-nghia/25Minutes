﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject levelUpPanel;
    public GameObject skill1Button;
    
    private GameObject soundBackground;
    [SerializeField] private List<GameObject> listBuffItems;

    [Header("Sprite Buff Item")]
    public GameObject item1,item2,item3;

    [Header("Title Buff Item")]
    public TextMeshProUGUI titleItem1, titleItem2, titleItem3;

    [Header("Description Buff Item")]
    public TextMeshProUGUI desItem1, desItem2, desItem3;

    List<GameObject> randomBuffItems = new List<GameObject>();
    void Start()
    {
        soundBackground = GameObject.FindGameObjectWithTag("AudioSource");
    }

    public void GetRandomBuffItem()
    {
        randomBuffItems.Clear();

        if (listBuffItems.Count >= 3)
        {
            System.Random random = new System.Random();

            while (randomBuffItems.Count < 3)
            {
                GameObject randomBuffItem = listBuffItems[random.Next(listBuffItems.Count)];

                if (!randomBuffItems.Contains(randomBuffItem))
                {
                    randomBuffItems.Add(randomBuffItem);
                }
            }
        }

        // ITEM 1
        item1.GetComponent<SpriteRenderer>().sprite = randomBuffItems[0].GetComponent<SpriteRenderer>().sprite;
        titleItem1.text = randomBuffItems[0].GetComponent<Items>().GetTitleItem();
        desItem1.text = randomBuffItems[0].GetComponent<Items>().GetDescriptionItem();

        // ITEM 2
        item2.GetComponent<SpriteRenderer>().sprite = randomBuffItems[1].GetComponent<SpriteRenderer>().sprite;
        titleItem2.text = randomBuffItems[1].GetComponent<Items>().GetTitleItem();
        desItem2.text = randomBuffItems[1].GetComponent<Items>().GetDescriptionItem();

        // ITEM 3
        item3.GetComponent<SpriteRenderer>().sprite = randomBuffItems[2].GetComponent<SpriteRenderer>().sprite;
        titleItem3.text = randomBuffItems[2].GetComponent<Items>().GetTitleItem();
        desItem3.text = randomBuffItems[2].GetComponent<Items>().GetDescriptionItem();
    }

    private void UpgradeLevel()
    {
        // tăng âm lượng lại như cũ 
        // Note: trường hợp (như cũ ở đây đang là 0.6f), sau này phát triển thêm Setting Menu -> setting Audio thì tính tiếp
        soundBackground.GetComponent<AudioSource>().volume = 0.6f;
        // ẩn panel nâng câp sau khi đã nâng
        levelUpPanel.SetActive(false);
        levelUpPanel.GetComponent<ActivePanel>().isSetActive = false;
        skill1Button.GetComponent<SpellCooldown>().Continue();
        Time.timeScale = 1.0f;
    }

    public void UpgradeLevel_Item1()
    {
        UpgradeLevel();

        randomBuffItems[0].GetComponent<Items>().IncreaseIndex();
    }

    public void UpgradeLevel_Item2()
    {
        UpgradeLevel();

        randomBuffItems[1].GetComponent<Items>().IncreaseIndex();
    }

    public void UpgradeLevel_Item3()
    {
        UpgradeLevel();

        randomBuffItems[2].GetComponent<Items>().IncreaseIndex();
    }
}
