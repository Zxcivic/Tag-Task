using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeinitialize : MonoBehaviour
{
    //References
    public GameObject redCube;
    public GameObject blueCube;

    //public variables
    public int numberOfCubes;
    public float radius = 20f;


    private void Awake()
    {
        for(int i = 0; i < numberOfCubes; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfCubes;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x,10,z);
            float angleDegree = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0,angleDegree,0);
            if (i%2 == 0 )
            {
                Instantiate(redCube, pos, rot);
                Instantiate(blueCube, pos + new Vector3(0f, 10, 0f), rot);
                
            }
            else
            {
                Instantiate(blueCube, pos, rot);
                Instantiate(redCube, pos + new Vector3(0f, 10, 0f), rot);
            }
        }
    }

    public int GetNumberOfCubes()
    {
        return numberOfCubes;
    }

}