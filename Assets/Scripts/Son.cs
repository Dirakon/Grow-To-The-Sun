using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Son : MonoBehaviour
{
    // Start is called before the first frame update
    public System.Action onRealSpawn;
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    public GameObject stem;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private static float distToDie = 0.5f;

    void Awake(){
        spriteRenderer.enabled=false;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col){
        var hero = col.gameObject.GetComponent<Hero>();
        if (onRealSpawn == null&& hero!= null &&hero.gameObject.tag == gameObject.tag && (transform.position - col.transform.position).magnitude <= distToDie){
            
            CameraFollower.singleton.GameOverScreen();
            Destroy(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.GetComponent<Hero>() != null){
            spriteRenderer.enabled=true;
            onRealSpawn();
            onRealSpawn = null;
        }
    }
}
