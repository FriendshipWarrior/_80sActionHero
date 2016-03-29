using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

    public Transform enter;
    public HeroMovement hero;
    public ScreenFader sf;

    void Start()
    {
        hero = FindObjectOfType<HeroMovement>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        sf = GameObject.FindGameObjectWithTag("fader").GetComponent<ScreenFader>();

        if (other.gameObject.name == "Hero")
        {
            yield return StartCoroutine(sf.FadeToBlack());
            hero.transform.position = enter.position;
            Camera.main.transform.position = enter.position;
        }
        //Debug.Log("an object has warped");


        yield return StartCoroutine(sf.FadeToClear());
    }
}
