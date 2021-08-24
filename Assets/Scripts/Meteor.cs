using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private static float camSize=14;
    [SerializeField]
    private float speed = 20f;
    // Start is called before the first frame update
    IEnumerator cyclicMovement(){
        float t = 0;
        Vector3 startPosition = new Vector3(-camSize,transform.position.y,-1);
        Vector3 endPosition = new Vector3(camSize,transform.position.y,-1);
        while (true){
            t+= Time.deltaTime*speed;
            if (t > 1)
                t = 1;
            transform.position = Vector3.Lerp(startPosition,endPosition,t);
            if (t == 1){
                t = 0;
                Vector3 temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
            }
            yield return null;
        }
    }
    void Start()
    {
        StartCoroutine(cyclicMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        var hero = col.gameObject.GetComponent<Hero>();
        if (hero != null){
            if (hero.tangable){
            Destroy(col.gameObject);
            CameraFollower.singleton.GameOverScreen();
            }
        }
    }
}
