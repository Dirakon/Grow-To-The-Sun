using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmer : MonoBehaviour
{
    // Start is called before the first frame update
    private static bgmer singleton;
    void Awake(){
        if (singleton != null){
            Destroy(gameObject);
        }else{
            singleton=this;
            DontDestroyOnLoad(gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
