using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMechanic : MonoBehaviour
{
    public GameObject Sphere;
    public Gameflow gameflow;
    void Start(){
        gameflow = GameObject.FindObjectsOfType<Gameflow>()[0];
    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
        if(gameObject.tag == "red")
        {
            
            
            if (other.gameObject.tag == "red"){
                gameflow.currentcubes ++;
                Debug.Log("Correct");
                Destroy(other.gameObject);
            }

            else if (other.gameObject.tag =="blue"){
                gameflow.iswrong = true;
                Debug.Log("Wrong");
                Destroy(other.gameObject);
            }
        }
        else if(gameObject.tag == "blue"){
        
        
            
        
            if (other.gameObject.tag == "blue"){
                gameflow.currentcubes ++;
                Debug.Log("Correct");
                Instantiate(Sphere, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "red"){
                gameflow.iswrong = true;
                Debug.Log("Wrong");
                Instantiate(Sphere, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            }

    }
    Destroy (gameObject);
    
}
}