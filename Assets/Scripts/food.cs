using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource eatSound;
    void Start()
    {
        
    }
    bool active = true;

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator slowDeath(){
        eatSound = GetComponent<AudioSource>();
        eatSound.Play();
        GetComponentInChildren<SpriteRenderer>().enabled=false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    void OnTriggerStay2D(Collider2D col){
        if (!active)
        return;
        var hero = col.gameObject.GetComponent<Hero>();
        if (hero != null && hero.tangable){
            hero.points+=30;
            active=false;           
            StartCoroutine(slowDeath());
        }
    }

}
