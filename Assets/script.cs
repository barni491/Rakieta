using UnityEngine;
using System.Collections;

public class script : MonoBehaviour {

    // Use this for initialization

    private Rigidbody m_Rigidbody;
    private ParticleSystem m_BoomParticle;
    public ParticleSystem flame;
    public GameObject planet;

    private void Awake()
    {
       m_Rigidbody = GetComponent<Rigidbody>();
       m_BoomParticle = GetComponentInChildren<ParticleSystem>();

    }

    void OnCollisionEnter(Collision col)
    {

        transform.FindChild("boom").transform.parent = null;
        m_BoomParticle.Play();
        Destroy(gameObject, 1f);
    }


    void Start () {



    }

    void FixedUpdate() {

        Vector3 v = planet.transform.position - transform.position;
        m_Rigidbody.AddForce(v.normalized  * 10000);
       transform.rotation = Quaternion.LookRotation(-m_Rigidbody.velocity, Vector3.up);
        flame.transform.rotation = Quaternion.LookRotation(-m_Rigidbody.velocity, Vector3.up);
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
