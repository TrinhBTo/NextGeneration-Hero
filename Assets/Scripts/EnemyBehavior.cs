using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
     public static EnemyBehavior sEnemyBehavior = null;

	[SerializeField]
    private string[] kWayPointNames = {
        "WayPointA", "WayPointB", "WayPointC",
        "WayPointD", "WayPointE", "WayPointF"};

   // private GameObject[] wayPoints;

    private const int kNumWayPoints = 6;

    public bool isSequential = true;

    public float mSpeed = 30f;

    public float enemyRotateSpeed = 90f/2f;
    
    int waypointInd;

    // // Start is called before the first frame update
    void Start()
    {
        waypointInd = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
       

        if(isSequential==true){

            moveSquentially();
            
            //GlobalBehavior.sTheGlobalBehavior.UpdateWaypointState("Sequentially   \n");
        }
		
        if(isSequential==false){

            moveRandomly();

            //GlobalBehavior.sTheGlobalBehavior.UpdateWaypointState("Randomly   \n");
        }

        transform.position += transform.up * (mSpeed * Time.smoothDeltaTime);
    }

    public void changeSequence(bool sequen){

        isSequential = sequen;
    }

    void moveSquentially(){


        if( Vector3.Distance(GameObject.Find(kWayPointNames[waypointInd]).transform.position,transform.position)  < 15){

            waypointInd++;    

            if(waypointInd == kWayPointNames.Length){

                waypointInd = 0;
            }        
        }

        transform.up = GameObject.Find(kWayPointNames[waypointInd]).transform.position - transform.position;
    }
    
    void moveRandomly(){

        if( Vector3.Distance(GameObject.Find(kWayPointNames[waypointInd]).transform.position,transform.position)  <15){

            int randomN = Random.Range(0, kWayPointNames.Length);

            while(randomN == waypointInd){

                randomN = Random.Range(0, kWayPointNames.Length);
            }

            waypointInd = randomN;
        }

        transform.up = GameObject.Find(kWayPointNames[waypointInd]).transform.position - transform.position;
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Egg") {

            Destroy(collision.gameObject);
            GlobalBehavior.sTheGlobalBehavior.DestroyAnEgg();

            Destroy(this.gameObject);
        }
    }
}
