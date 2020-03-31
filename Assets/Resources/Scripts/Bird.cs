using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float velocity = 1;
    private Rigidbody2D rb;
    private bool firstTap;
    
    // Start is called before the first frame update
    void Start()
    {
        firstTap = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(firstTap){
                if(GameManager.Instance.currentMenu != Menu.start){
                    return;
                }
                firstTap = false;
                GameManager.Instance.PlayGame();
                rb.isKinematic = false;
            }
            rb.velocity = Vector2.up * velocity;
            SoundManager.Instance.playFlap();
        }
    }

    public void setStartPosition(){
        transform.position = new Vector3(-0.3f, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameManager.Instance.GameOver();
    }
}
