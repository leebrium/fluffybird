using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float velocity = 1;
    private Rigidbody2D rb;
    private bool firstTap;
    private bool isDead;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        firstTap = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead) {
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
    }

    public void setStartPosition(){
        transform.position = new Vector3(-0.3f, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!isDead) {
            isDead = true;
            animator.enabled = false;
            GameObject.Find("Ground").GetComponent<Animator>().enabled = false;
            GameObject.Find("Background").GetComponent<BackgroundMovement>().enabled = false;
            GameManager.Instance.GameOver();
        }
    }
}
