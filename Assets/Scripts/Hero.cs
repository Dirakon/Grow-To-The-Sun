using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero : MonoBehaviour
{
    [SerializeField]
    public float speed=10f;
    public bool tangable = true;
    [SerializeField]
    private float angleSpeed=10f;

    [SerializeField]
    public Text pointTable;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject sonPrefab;
    [SerializeField]
    private GameObject crackPrefab;
    [SerializeField]
    private GameObject spriter;
    [SerializeField]
    private float reloadInSeconds = 5f;
    bool isJumpReloaded = true;
    private float scaleFactor = 2f;
    [SerializeField]
    private float secondsInJump = 2f;
    IEnumerator makeJump(){
        tangable = false;
        transform.position = new Vector3 (transform.position.x,transform.position.y,-3);
        gameObject.tag = "high";
        var scalel = new Vector3(1,1,1)*scaleFactor;
        var crack = Instantiate(crackPrefab,transform.position + new Vector3(0,0,1),Quaternion.identity);//
        crack.tag="high";
        var scalelBak = new Vector3(1f,1f,1f);
        transform.localScale = scalel;
        CameraFollower.singleton.JumpSound();
        yield return new WaitForSeconds(secondsInJump);
        CameraFollower.singleton.JumpSound();
        gameObject.tag="low";


        transform.localScale = scalelBak;
        crack = Instantiate(crackPrefab,transform.position + new Vector3(0,0,1),Quaternion.identity);
        crack.tag="high";
        transform.position = new Vector3 (transform.position.x,transform.position.y,-0.5f);
        tangable = true;


        yield return new WaitForSeconds(reloadInSeconds);
        isJumpReloaded = true;
        
    }

    public int points = -1;
    public void Grow(){
        points+=1;
        Vector3 pos = transform.position;
        Quaternion dir = directionist.transform.rotation;
       // dir.eulerAngles = new Vector3(0,0,dir.eulerAngles.z);
        var obj = Instantiate(sonPrefab,pos,Quaternion.identity).GetComponent<Son>();
        obj.gameObject.tag = gameObject.tag;
        obj.gameObject.transform.localScale = transform.localScale;
        obj.onRealSpawn += Grow;
        obj.stem.transform.rotation = dir;

    }
    void Awake(){
            gameObject.tag="low";
    }
    [SerializeField]
    private float pointMultiplier = 0.001f;
    private float startSpeed;
    private float startAngleSpeed;
    // Start is called before the first frame update*
    void Start()
    {
        CameraFollower.singleton.pointTable=pointTable;

        startSpeed = speed;
        startAngleSpeed = angleSpeed;
        rb = GetComponent<Rigidbody2D>();
        Grow();
    }
    float dotPosition = 90; 
    [SerializeField]
    private GameObject directionist;
    // Update is called once per frame
    void Update()
    {

        if (isJumpReloaded&&Input.GetKey(KeyCode.Space)){
            isJumpReloaded=false;
            StartCoroutine(makeJump());
        }
        pointTable.text = points.ToString();
    Vector3 worldPosition;
    speed = startSpeed*(1+  pointMultiplier*points);
    Debug.Log(speed);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z=transform.position.z;

        const float distanceToRebel = 1f;

        if ((transform.position-worldPosition).magnitude <= distanceToRebel){
            worldPosition += directionist.transform.forward* distanceToRebel;
        }

       // old way: at mouse
        Quaternion dir = directionist.transform.rotation;
        Vector3 change = worldPosition-transform.position;
        int dop = 0;
        if (change.y>0)
            dop = 180;
        Quaternion finish = Quaternion.Euler(0,0,dop-Mathf.Atan(change.x/change.y)*Mathf.Rad2Deg-90);
        float acelertor = Input.GetAxis("Vertical");
        if (acelertor < 0)
            acelertor = 0;
        acelertor+=1;


       //worldPosition = directionist.transform.position;
       angleSpeed = Mathf.Max(angleSpeed,600);
       angleSpeed = startAngleSpeed*(1 + pointMultiplier*points);
        dir = Quaternion.Lerp(dir,finish,Time.deltaTime*angleSpeed);
        directionist.transform.rotation=dir;
        spriter.transform.rotation=dir;
       // dotPosition -= Time.deltaTime*angleSpeed*Input.GetAxis("Horizontal");
       //worldPosition.x+= Mathf.Cos(Mathf.Deg2Rad*dotPosition)*10;
       //worldPosition.y+= Mathf.Sin(Mathf.Deg2Rad*dotPosition)*10;
      // directionist.transform.LookAt(worldPosition,Vector3.back);
    
       // var rot = directionist.transform.rotation;
       // rot.eulerAngles=new Vector3(rot.eulerAngles.x + Input.GetAxis("Horizontal")*Time.deltaTime*angleSpeed,0,0);
       // directionist.transform.rotation=rot;
        Debug.Log(directionist.transform.forward*speed*acelertor);
        rb.velocity =directionist.transform.right*speed*acelertor;
        Debug.Log(rb.velocity);
    }

}
