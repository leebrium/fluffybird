using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 1f;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        loadBackground();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((new Vector2(-1, 0)) * speed * Time.deltaTime);
        if(transform.position.x < -2.2f) {
            transform.position = startPos;
        }
    }

    private void loadBackground(){
        float hour = System.DateTime.Now.Hour;
        float minutes = System.DateTime.Now.Minute;
        float hourMinutes = hour + (minutes/60);
        Debug.Log("Hour: " + hour + ", Minute: " + minutes + ", HourMinutes: " + hourMinutes);

        string backgroundName = "";
        if(hourMinutes > 5.5 && hourMinutes < 18.5) {
            backgroundName = "background_day";
        } else {
            backgroundName = "background_night";
        }

        Sprite spBackground = Resources.Load<Sprite>("Sprites/" + backgroundName);

        GetComponent<SpriteRenderer>().sprite = spBackground;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spBackground;
    }
}
