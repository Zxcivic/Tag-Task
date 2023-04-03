using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameflow : MonoBehaviour
{
   public bool iswrong;
   public cubeinitialize cube;
   cubeinitialize script;
   public int totalcubes;
   public int currentcubes;
   void Start(){
        iswrong = false;
        currentcubes = 0;
        
        script = cube.GetComponent<cubeinitialize>();
        totalcubes = script.numberOfCubes*2;
   }

    // Update is called once per frame
    void Update()
    {
        if (iswrong){
            SceneManager.LoadSceneAsync(2);
        }
        if (currentcubes >= totalcubes){
            SceneManager.LoadSceneAsync(1);
    }
}
}
