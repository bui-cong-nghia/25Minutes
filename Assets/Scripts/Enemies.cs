﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    GameObject scoreUIText;

    //public float maxHealth;
    //public float currentHealth;
    [SerializeField] private float health;

    public int enemyScore;
    public float moveSpeed;
    public GameObject deathAnimation;

    [SerializeField]
    private bool chasingPlayer = false; // Dí theo mục tiêu
    private SpriteRenderer spriteRenderer;

    private Transform player;

    [SerializeField] private int enemyDamage = 10; // Damage của quái


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //scoreUIText = GameObject.FindGameObjectWithTag("ScoreText");

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;

            if (direction.x < 0)
            {
                // Nếu người chơi ở bên trái, flip sprite qua trái
                spriteRenderer.flipX = false;
            }
            else if (direction.x > 0)
            {
                // Nếu người chơi ở bên phải, không flip sprite
                spriteRenderer.flipX = true;
            }
        }

        if (chasingPlayer && player != null)
        {
            ChasePlayer();
        }
        else
        {
            MoveNormally();
        }

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (health <= 0)
            {
                Die();
            }

            // Gây sát thương cho người chơi
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(enemyDamage);
            }
        }
    }


    private bool beingAttacked = false;

    public void SetBeingAttacked(bool attacked)
    {
        beingAttacked = attacked;
    }

    public bool IsBeingAttacked()
    {
        return beingAttacked;
    }


    void PlayExplosion()
    {
        GameObject explosion = Instantiate(deathAnimation);
        explosion.transform.position = transform.position;
    }

    // Hàm để bật hoặc tắt chế độ theo đuổi người chơi
    //public void SetChasePlayer(bool chase)
    //{
    //    chasingPlayer = chase;
    //}

    // Hàm để thực hiện hành động theo đuổi người chơi
    void ChasePlayer()
    {
        // Thực hiện các hành động liên quan đến việc theo đuổi người chơi ở đây
        // Ví dụ: Di chuyển theo hướng tới vị trí của người chơi
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        //if (Vector2.Distance(transform.position, player.position) > 3)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //} // Nên dùng cho boss để boss giữ khoảng cách
    }


    // Hàm để thực hiện hành động di chuyển thường
    void MoveNormally()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - moveSpeed * Time.deltaTime);
        transform.position = position;

        //if (Vector2.Distance(transform.position, player.position) > 3)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //}
    }

    void Die()
    {
        // Gọi ra từ hàm LootBag để quái rớt đồ
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        PlayExplosion();
        ExperienceManager.Instance.AddExperience(enemyScore);
        Destroy(gameObject);
        Debug.Log("Enemy die");
    }
}