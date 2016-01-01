using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Vector3 targetPosition, velocity = Vector3.zero;

    public Rigidbody2D MyRigidbody2D;

    public float MaxSpeed = 5f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            targetPosition = new Vector3(ray.origin.x, ray.origin.y, transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
        // other way to move
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 1);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, MaxSpeed);



    }
}
