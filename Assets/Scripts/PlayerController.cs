using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour {

    private Vector3 targetPosition;

    public Rigidbody2D MyRigidbody2D;

    public GameObject ShurikenPrefab;

    private GameObject shurikenClone;
    //private bool attacking;
    private Vector2 shurikenVelo;
    // Use this for initialization
    void Start () {
        //attacking = false;
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

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            //gameObject.SetActive(false);
        }
    }
    [Command]
    void CmdAttack(Vector3 pos)
    {
        GameObject shurikenClone = (GameObject)Instantiate(ShurikenPrefab, transform.position, transform.rotation);
        shurikenClone.transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
        NetworkServer.Spawn(shurikenClone);
    }
}
