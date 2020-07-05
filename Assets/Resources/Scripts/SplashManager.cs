using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public float shiningTime = 1f;
	public float width = 0.2f;

	SpriteRenderer sr;

    Animator animator;
    float animationTime;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator>();

        animationTime = animator.runtimeAnimatorController.animationClips[0].length;
        Debug.Log("Animation Time : " + animationTime);

        StartCoroutine(Shine());
	}

	IEnumerator Shine () {
        yield return new WaitForSeconds(animationTime);

		float currentTime = 0;
		float speed = 1f / shiningTime;

		sr.material.SetFloat ("_Width", width);

		while (currentTime <= shiningTime) {
			currentTime += Time.deltaTime;
			float value = Mathf.Lerp (0, 1, speed * currentTime);
			sr.material.SetFloat ("_TimeController", value);
			yield return null;
		}
		yield return new WaitForSeconds (0.5f);
		sr.material.SetFloat ("_Width", 0);

        animator.SetTrigger("TriggerFading");

        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(1);
	}
}
