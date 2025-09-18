using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{

    public float bulletSpeed;
    public float rightScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.up * bulletSpeed) * Time.deltaTime);
        if(transform.position.x > rightScreen)
        {
            Destroy(gameObject);
        }
    }
}
