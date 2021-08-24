using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killerWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField]
    private bool isSun = false;
    [SerializeField]
    private bool ignoresIntangible = false;
    void OnTriggerEnter2D(Collider2D col){
        var hero = col.gameObject.GetComponent<Hero>();
        if (hero != null){
            if (ignoresIntangible || hero.tangable){
            Destroy(col.gameObject);
            CameraFollower.singleton.GameOverScreen(isSun);
            }

        }
        else if (col.gameObject.transform.tag != "high")
            Destroy(col.gameObject);
    }
}
