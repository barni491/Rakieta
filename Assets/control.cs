using UnityEngine;
using System.Collections;

public class control : MonoBehaviour {

    // Use this for initialization

    public int force;

    public int speed;
    public int rotationSpeed; 


    public Material skybox;
    public ParticleSystem particleSystem;

    private Rigidbody m_Rigidbody;

	void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.centerOfMass = new Vector3(0, 10, 0);

    }

    // Update is called once per frame
    void FixedUpdate () {

        Quaternion turnRotation = Quaternion.Euler(1f, 0f, 0f);


        if (transform.position.y > 200) {
            RenderSettings.skybox = skybox;

        }

        if (Input.GetKey(KeyCode.M)) {
            particleSystem.Play();
            m_Rigidbody.AddForce(transform.up*10000);

        }

        if (Input.GetKey(KeyCode.Z))
        {
            speed += 10;
           
        }

        if (Input.GetKey(KeyCode.X))
        {
            
            speed -= 10;

        }

        transform.Translate(Vector3.up * Time.deltaTime*speed);
         if (Input.GetKey(KeyCode.J)) {

            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.L))
        {

            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.I)) { 
        
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        }

        // Apply this rotation to the rigidbody's rotation.
        //  m_Rigidbody.MoveRotation(turnRotation); 
        //   m_Rigidbody.eRotation(m_Rigidbody.rotation * turnRotation);
        //   m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        //  transform.localRotation = turnRotation;
        //  //   transform.Rotate(m_Rigidbody.centerOfMass.,);
        //  transform.Rotate( Vector3.left, 2* Time.deltaTime);

    }
}

//using UnityEngine;
//using System.Collections;
 
//public class control : MonoBehaviour
//{

//    // Use this for initialization

//    public float thrustMultiplier;
//    public Vector3 centerMass;

//    private Rigidbody m_Rigidbody;
//    private bool thurst;
//    private Vector3 forceThrust;

//    void Start()
//    {
//        m_Rigidbody = GetComponent<Rigidbody>();
//        m_Rigidbody.centerOfMass = centerMass;
//    }

//    void ApplyRocketThrust()
//    {
//        if (thurst)
//        {
//            forceThrust = transform.up * thrustMultiplier;
//            m_Rigidbody.AddForce(forceThrust);
//        }
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {

//        if (Input.GetKey(KeyCode.T))
//        {
//            thurst = true;
//        }

//        if (Input.GetKey(KeyCode.G))
//        {
//            thurst = false;
//        }

//        if (Input.GetKey(KeyCode.K))
//        {

//            transform.Rotate(Vector3.forward, 10 * Time.deltaTime);
//        }

//        if (Input.GetKey(KeyCode.I))
//        {

//            transform.Rotate(Vector3.back, 10 * Time.deltaTime);
//        }

//        if (Input.GetKey(KeyCode.L))
//        {
//            transform.Rotate(Vector3.left, 10 * Time.deltaTime);

//        }

//        if (Input.GetKey(KeyCode.J))
//        {

//            transform.Rotate(Vector3.right, 10 * Time.deltaTime);

//        }

//        ApplyRocketThrust();

//    }
//}