using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class but : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }
}
