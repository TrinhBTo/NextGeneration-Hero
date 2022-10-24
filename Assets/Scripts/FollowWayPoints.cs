using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{   
    [SerializeField]
    private string[] kWayPointNames = {
        "WayPointA", "WayPointB", "WayPointC",
        "WayPointD", "WayPointE", "WayPointF"};

   // private GameObject[] wayPoints;

    private const int kNumWayPoints = 6;

    private bool isSequential = true;

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
       

        if (Input.GetKeyDown(KeyCode.J)){

            isSequential=!isSequential;
        }

        if(isSequential==true){

            moveSquentially();
        }
		
        if(isSequential==false){

            moveRandomly();
        }

        transform.position += transform.up * (mSpeed * Time.smoothDeltaTime);
    }

     void moveSquentially(){


        if( Vector3.Distance(GameObject.Find(kWayPointNames[waypointInd]).transform.position,transform.position)  < 18){

            waypointInd++;    

            if(waypointInd == kWayPointNames.Length){

                waypointInd = 0;
            }        
        }

        transform.up = GameObject.Find(kWayPointNames[waypointInd]).transform.position - transform.position;
       
    }
    
    void moveRandomly(){

        

        if( Vector3.Distance(GameObject.Find(kWayPointNames[waypointInd]).transform.position,transform.position)  <18){

            int randomN = Random.Range(0, 5);

            while(randomN == waypointInd){

                randomN = Random.Range(0, 5);
            }

            waypointInd = randomN;
        }

        transform.up = GameObject.Find(kWayPointNames[waypointInd]).transform.position - transform.position;
        // transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(kWayPointNames[waypointInd]).transform.position, mSpeed*Time.deltaTime);
        
    } 
}
