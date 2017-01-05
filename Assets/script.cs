using UnityEngine;
using System.Collections;

public class script : MonoBehaviour {

    // Use this for initialization

    private Rigidbody m_Rigidbody;
    private ParticleSystem m_BoomParticle;

    private void Awake()
    {
       m_Rigidbody = GetComponent<Rigidbody>();
       m_BoomParticle = GetComponentInChildren<ParticleSystem>();

    }


    void Start () {



    }
	
	// Update is called once per frame
	void Update () {



        if (Input.GetKey(KeyCode.Q)) {

           

            transform.FindChild("boom").transform.parent = null;
            m_BoomParticle.Play();

        //    m_Rigidbody.isKinematic = true;
            Destroy(gameObject,1f);


        }

           





    }
}
