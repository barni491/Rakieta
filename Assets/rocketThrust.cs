using UnityEngine;
using System.Collections;

public class rocketThrust : MonoBehaviour {
    public AudioClip rocketSound;

    private AudioSource source;
 


    void Awake()
    {

        source = GetComponent<AudioSource>();

    } 


    void Update()
    {

        if (Input.GetKey(KeyCode.M) && Camera.current.name != "FootCamera")
        {
            
            source.PlayOneShot(rocketSound);
        }
        else
        {
            source.Stop();
        }

    }
}
