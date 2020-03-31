using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        SoundManager.Instance.playScored();
        GameManager.Instance.scoreManager.AddScore();
    }
}
