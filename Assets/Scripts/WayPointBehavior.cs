using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointBehavior : MonoBehaviour
{
    private int life = 4;


    Vector3 iniPosition;

    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
       iniPosition = transform.position;
       
       col= gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (life == 0 ) {

            MoveDiagnolly();

            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            life = 4;
           
        }

        // Hide waypoint
        if (Input.GetKeyDown("h"))
        {
            this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;
            
            col.enabled = !col.enabled;

        }

    }

    private void MoveDiagnolly(){

        float randomNumber = Random.Range(0, 100);

        if (randomNumber<=25){

            transform.position = iniPosition + (new Vector3(15, 15, 0));

        }else if(randomNumber>25 && randomNumber<=50){

            transform.position = iniPosition + (new Vector3(15, -15, 0));

        }else if(randomNumber>50 && randomNumber<=75){

            transform.position = iniPosition + (new Vector3(-15, -15, 0));
            
        }else{

            transform.position = iniPosition + (new Vector3(-15, 15, 0));
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Egg") {

            Destroy(collision.gameObject);
            GlobalBehavior.sTheGlobalBehavior.DestroyAnEgg();

            life--;

            float newColor = GetComponent<Renderer>().material.color.a * 0.8f;

            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, newColor);
        }
    }

   
}
