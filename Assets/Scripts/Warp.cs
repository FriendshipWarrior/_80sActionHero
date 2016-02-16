using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

    public Transform enter;
	
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("fader").GetComponent<ScreenFader>();
        yield return StartCoroutine(sf.FadeToBlack());
        
        //Debug.Log("an object has warped");
        other.gameObject.transform.position = enter.position;
        Camera.main.transform.position = enter.position;

        yield return StartCoroutine(sf.FadeToClear());
    }
}
