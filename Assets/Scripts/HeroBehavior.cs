using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroBehavior : MonoBehaviour {

    public EggStatSystem mEggStat = null;
    public float mHeroSpeed = 20f;
    public float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
    private int killCount = 0;
    public bool isSequen = true;

	// Use this for initialization
    
    // Waypoint
    private string[] kWayPointNames = {
            "WayPointA", "WayPointB", "WayPointC",
            "WayPointD", "WayPointE", "WayPointF"};
    private GameObject[] mWayPoints;
    private const int kNumWayPoints = 6;

    // Control
    private bool isMouseControl = true;
    Vector3 mousePosition;

    // Objects
    public GameObject EnemyToSpawn;
    private List<GameObject> enemyList = new List<GameObject>();

	void Start () {
        
        for(int j=0; j<10; j++){

            spawnEnemy();  
        }

        Debug.Assert(mEggStat != null);
    
        mWayPoints = new GameObject[kWayPointNames.Length];
       
        int i = 0;
        foreach (string s in kWayPointNames)
        {
            mWayPoints[i] = GameObject.Find(kWayPointNames[i]);
            Debug.Assert(mWayPoints[i] != null);
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () { 
        UpdateMotion();
        BoundPosition(); 
        ProcessEggSpwan();
        updateEnemy();

        int i = 0;
        foreach(string s in kWayPointNames){
 
            if(mWayPoints[i] == null){

                mWayPoints[i] = GameObject.Instantiate(GameObject.Find(kWayPointNames[i]));
            }

            i++;
        }

        if (Input.GetKeyDown(KeyCode.J)){

            isSequen = !isSequen;
        }

        foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<EnemyBehavior>().changeSequence(isSequen);
        }

        if(isSequen==true){

            GlobalBehavior.sTheGlobalBehavior.UpdateWaypointState("Sequentially   \n");
        }
		
        if(isSequen==false){

            GlobalBehavior.sTheGlobalBehavior.UpdateWaypointState("Randomly   \n");
        }


        GlobalBehavior.sTheGlobalBehavior.UpdateEnemyState(killCount);

        if (Input.GetKey("q")) {
            
            Application.Quit();
        }
    }


    private void UpdateMotion(){

        if (Input.GetKeyDown(KeyCode.M)){

            isMouseControl=!isMouseControl;
        }
 
        // Mouse mode
        if(isMouseControl==true){

            MouseFollowControll();

            GlobalBehavior.sTheGlobalBehavior.UpdateControlState("Mouse   \n");
        }

        // Keyboard mode
        if(isMouseControl==false){

            mHeroSpeed += Input.GetAxis("Vertical");
            transform.position += transform.up * (mHeroSpeed * Time.smoothDeltaTime);

            GlobalBehavior.sTheGlobalBehavior.UpdateControlState("Keyboard   \n");
        }
        

        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                        (kHeroRotateSpeed * Time.smoothDeltaTime));
    }

    void MouseFollowControll(){

        mousePosition  = Input.mousePosition;
        
        mousePosition.z = 10f;
        
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void BoundPosition(){
        GlobalBehavior.WorldBoundStatus status = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
        switch (status)
        {
            case GlobalBehavior.WorldBoundStatus.CollideBottom:
            case GlobalBehavior.WorldBoundStatus.CollideTop:
                transform.up = new Vector3(transform.up.x, -transform.up.y, 0.0f);
                break;
            case GlobalBehavior.WorldBoundStatus.CollideLeft:
            case GlobalBehavior.WorldBoundStatus.CollideRight:
                transform.up = new Vector3(-transform.up.x, transform.up.y, 0.0f);
                break;
        }
    }

    private void ProcessEggSpwan(){
        if (mEggStat.CanSpawn()) {
            if (Input.GetKey("space"))
                mEggStat.SpawnAnEgg(transform.position, transform.up);
        }
    }

    void spawnEnemy(){

        Vector3 randomPosition = new Vector3(Random.Range(-155f,155f),Random.Range(-50f,50f));
        GameObject newEnemy = Instantiate(EnemyToSpawn, randomPosition, Quaternion.identity);
        enemyList.Add(newEnemy);
    }

    void updateEnemy(){

        if(enemyList.Count>=1){

            int en = enemyList.Count-1;

            while (en >= 0) {

                if(!enemyList[en]) {
                    
                    
                    enemyList.Remove(enemyList[en]);
                    
                    killCount++;

                    spawnEnemy();
                }
                
                en--;
            }
        }
    }

}
