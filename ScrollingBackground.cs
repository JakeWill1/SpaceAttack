using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float scrollSpeed;
    public float tempX;
    public float width;
    public SpriteRenderer myRenderer;

    private void Awake()
    {
        tempX = transform.position.x;
        myRenderer = GetComponent<SpriteRenderer>();
        width = myRenderer.bounds.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempX -= scrollSpeed * Time.deltaTime;
        transform.position = new Vector2(tempX, transform.position.y);
        if (transform.position.x < -width)
        {
            Vector2 groundOffset = new Vector2(width * 2f, 0);
            transform.position = (Vector2)transform.position + groundOffset;
            tempX = transform.position.x;
        }
    }
}
