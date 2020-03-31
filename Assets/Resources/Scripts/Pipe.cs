using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isMoving;
    void Awake()
    {
        resetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            transform.position += Vector3.left * GameManager.Instance.settings.pipeSpeed * Time.deltaTime;

            if(transform.position.x <= -GameManager.Instance.settings.pipeXMin){
                resetPosition();
            }
        }
    }

    public void resetPosition(){
        isMoving = false;
        float y = Random.Range(GameManager.Instance.settings.pipeYMin, GameManager.Instance.settings.pipeYMax);
        transform.position = new Vector3(GameManager.Instance.settings.pipeXMax, y, 0);
    }

    public void enableMoving() {
        isMoving = true;
    }
}
