using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShow : MonoBehaviour {

    public GameObject Blood_ground;
    GameObject blood;
    public float BloodHigh;

    private void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
       
        
        if(other.transform.localEulerAngles.y==172f)
        {
           
            blood = GameObject.Instantiate(Blood_ground, new Vector3(other.transform.position.x - 9.0f, this.transform.position.y + BloodHigh, other.transform.position.z), this.transform.rotation) as GameObject;
            Destroy(blood, 0.5f);
        }
        else
        {
            blood = GameObject.Instantiate(Blood_ground, new Vector3(other.transform.position.x + 9.0f, this.transform.position.y + BloodHigh, other.transform.position.z), this.transform.rotation) as GameObject;
            Destroy(blood, 0.5f);
        }

       
       
    }

    
}
