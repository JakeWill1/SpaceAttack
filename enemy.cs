using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public float minSpeed, maxSpeed;
    public float enemySpeed;
    public float tempX, tempY;
    public float bottomOFScreen, topOfScreen;
    public float maxLeft, maxRight;
    public GameObject particle;
    public player player;
    public int enemyValue;
    public int hitPoints;
    public SpriteRenderer myNumber;
    public Sprite num1, num2, num3, num4, num5, num6;

    void ResetEnemy()
    {
        tempX = maxRight;
        tempY = Random.Range(bottomOFScreen, topOfScreen);
        enemySpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetEnemy();
        int tempSize = Random.Range(0, 3);
        myNumber = transform.Find("numbers").GetComponent<SpriteRenderer>();
        switch (tempSize)
        {
            case 0:
                transform.localScale = new Vector3(.5f, .5f, 1);
                hitPoints = 2;
                myNumber.sprite = num2;
                enemyValue = 10;
                break;

            case 1:
                transform.localScale = new Vector3(1, 1, 1);
                hitPoints = 4;
                myNumber.sprite = num4;
                enemyValue = 20;
                break;

            case 2:
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
                hitPoints = 6;
                myNumber.sprite = num6;
                enemyValue = 30;
                break;
        }
        transform.position = new Vector2(tempX, tempY);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        tempX -= enemySpeed * Time.deltaTime;
        if (tempX < maxLeft)
        {
            ResetEnemy();
        }
        transform.position = new Vector2(tempX, tempY);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Instantiate(particle, transform.position, particle.transform.rotation);
            hitPoints--;
            switch (hitPoints)
            {
                case 5:
                    myNumber.sprite = num5;
                    break;

                case 4:
                    myNumber.sprite = num4;
                    break;

                case 3:
                    myNumber.sprite = num3;
                    break;

                case 2:
                    myNumber.sprite = num2;
                    break;

                case 1:
                    myNumber.sprite = num1;
                    break;
            }
            if(hitPoints <= 0)
            {
                player.AddScore(enemyValue);
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            player.SubtractLife();
            Instantiate(particle, transform.position, particle.transform.rotation);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "PlayerShield")
        {
            Instantiate(particle, transform.position, particle.transform.rotation);
            Destroy(gameObject);
        }
    }
    public void Death()
    {
        player.AddScore(enemyValue);
        Destroy(gameObject);
    }
}
