using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private AudioSource deathSound,jumpSound;

public void JumpSound(){
    jumpSound.Play();
}

    [SerializeField]
    private GameObject mineralPrefab;
    [SerializeField]
    private float distanceBetweenSpawn = 10f;
    private float yToSpawn = 0;
    // Start is called before the first frame update
    [SerializeField]
    private Image backgroundRoom;
    [SerializeField]
    private Hero whoToFollow;
    bool jumping=false;
    float yToJumpOn;
    float yToJumpFrom;
    [SerializeField]
    float jumpSpeed = 2f;
    float t = 0;
    private static float distanceToJump = 1f;
    [SerializeField]
    private Sprite[] backgrounds;
    [SerializeField]
    private float[] distancesToChange;
    IEnumerator waitForSpaceToRestart(){
        while (true){
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space)){
                break;
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);

    }
    void Start()
    {
        
    }
    void Awake(){
        singleton=this;
    }
    public void GameOverScreen(bool sunIsReached = false){
        string points = pointTable.text;
        if (sunIsReached){
            pointTable.text = "You won!";
        }else{
            pointTable.text = "Game over.";
            deathSound.Play();
        }
        StartCoroutine(waitForSpaceToRestart());
        pointTable.text += "\n You got " + points + " points!" + "\n(press space to restart).";
    }
    public Text pointTable;

    [SerializeField]
    private int maxMinerals;
    public static CameraFollower singleton;

    void SpawnMinerals(){
        int minerals = Random.Range(0,maxMinerals+1);
        float yMin = transform.position.y+10;
        float yMax = yMin + distanceBetweenSpawn;
        float xMin = transform.position.x-4;
        float xMax = transform.position.x+4;
        for (int i = 0; i < minerals;++i){
            Instantiate(mineralPrefab,new Vector3(Random.Range(xMin,xMax),Random.Range(yMin,yMax),0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    private int bckPtr = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
Application.Quit();
        }
        if (jumping){
            t+=jumpSpeed*whoToFollow.speed*Time.deltaTime;
            if (t >= 1){
                t = 1;
                jumping = false;
                if (transform.position.y >= yToSpawn){
                    yToSpawn+=distanceBetweenSpawn;
                    SpawnMinerals();
                }
                if (bckPtr < distancesToChange.Length && transform.position.y >= distancesToChange[bckPtr]){
                    backgroundRoom.sprite = backgrounds[bckPtr];


                    bckPtr++;
                }
            }
            Vector3 from = new Vector3(transform.position.x,yToJumpFrom,transform.position.z);
            Vector3 to = new Vector3(transform.position.x,yToJumpOn,transform.position.z);
            transform.position = Vector3.Lerp(from,to,t);
        }else if (whoToFollow != null){
            
            float dist = Mathf.Abs(transform.position.y-whoToFollow.transform.position.y);
            if (dist >= distanceToJump){
                jumping = true;
                yToJumpOn = whoToFollow.transform.position.y;
                yToJumpFrom = transform.position.y;
                t = 0;
            }
        }
    }
}
