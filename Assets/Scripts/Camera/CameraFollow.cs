using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float cam_speed = 0.1f;
    Camera mycam;

    private static bool camExists;

	// Use this for initialization
	void Start () {
        mycam = GetComponent<Camera>();
        if (!camExists)
        {
            camExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        mycam.orthographicSize = (Screen.height / 100f) / 3.5f;
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, cam_speed) + new Vector3(0,0,-10);
        }
	}
}
