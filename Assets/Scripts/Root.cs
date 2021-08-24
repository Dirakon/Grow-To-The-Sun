using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField]
    private CameraFollower cameraToFollow;
    [SerializeField]
    private float distanceToFollow = 20f;

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Abs(transform.position.y - cameraToFollow.transform.position.y);
        if (dist  >= distanceToFollow){
            transform.position = new Vector3(transform.position.x,cameraToFollow.transform.position.y-distanceToFollow,transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if (col.GetComponent<Hero>()!=null)
            CameraFollower.singleton.GameOverScreen();
        Destroy(col.gameObject);
    }
}
