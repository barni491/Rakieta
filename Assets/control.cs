using UnityEngine;
using System.Collections;

public class control : MonoBehaviour
{

    // Use this for initialization

    public int force;

    public int speed;
    public int rotationSpeed;

    public GameObject terrain;
    public Material skybox;
    public ParticleSystem particleSystem;
    public GameObject planet;

    private float throtle = 0;


    private Vector3 gravitationalAcceleration = new Vector3(9.71f, 9.71f, 9.71f);

    private Rigidbody m_Rigidbody;

    private bool terrainHidden = false;
    private bool engineStarded = false;
    public bool isMovable = false;
    private bool inAir = false;
    private static float height;
    private string heightText = "";

    private bool customGravity;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.centerOfMass = new Vector3(0, 10, 0);

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !isMovable)

        {

     

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" && isMovable)

        {
         
        }
    }


    private void moveInput()
    {

        if (isMovable)
        {

            throtle = Input.GetAxis("RacketThrotle");

            if (!engineStarded)
            {
                particleSystem.Play();
                engineStarded = true;
            }

            //  particleSystem.Play();
            //  transform.Rotate(Vector3.forward,throtle* rotationSpeed * Time.deltaTime);
            //  m_Rigidbody.AddForce(transform.up * 10000);
            particleSystem.startSpeed = 2.0f + (throtle * 3.0f);
            m_Rigidbody.AddForce(transform.up * 10000 * throtle);
            if (throtle == 0)
            {
                engineStarded = false;
                particleSystem.Stop();

            }

            if (inAir)
            {
                float backMove = Input.GetAxis("RacketHorizontal") * rotationSpeed * Time.deltaTime;

                float turnMove = Input.GetAxis("RacketVertical") * rotationSpeed * Time.deltaTime;

                transform.Rotate(-turnMove, 0, -backMove);
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Wysokość " + heightText);
    }

    void Update()
    {

        Quaternion turnRotation = Quaternion.Euler(1f, 0f, 0f);

        heightText = ((int)height - 1026).ToString();


        //  Debug.Log(throtle);

        

        if (transform.position.y > 200)
        {
            //    m_Rigidbody.velocity += 9.71f * Time.fixedTime * (planet.transform.position - transform.position);
            if (!terrainHidden)
            {
                RenderSettings.skybox = skybox;
                terrain.SetActiveRecursively(false);
                terrainHidden = true;
            }
        }

      /*  if (Input.GetKey(KeyCode.M))
        {
            particleSystem.Play();
            m_Rigidbody.AddForce(transform.up * 10000);
            particleSystem.startSpeed += 5;


        }



        if (Input.GetKey(KeyCode.Z))
        {
            speed += 10;

        }

        if (Input.GetKey(KeyCode.X))
        {

            speed -= 10;

        }

        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.J))
        {
            //  m_Rigidbody.AddTorque(new )
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

        if (Input.GetKey(KeyCode.I))
        {

            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        }

    */

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        moveInput();

        if (!inAir && transform.position.y > 20) {
            inAir = true;
        }

            if (transform.position.y > 100 && !customGravity)
        {

            customGravity = true;

        }


        height = Vector3.Distance(planet.transform.position, transform.position);

        //  if (transform.position.y > 100)
        //  {
        //    m_Rigidbody.velocity += 9.71f * Time.fixedTime * (planet.transform.position - transform.position);
        if (customGravity)
        {
            if (height <= 10000)
            {
                //   Debug.Log("grwitacja");
                Vector3 v = planet.transform.position - transform.position;
                m_Rigidbody.AddForce(v.normalized * (1.0f - height / 10000) * 1000);

                //   }



            }

            //   Debug.Log(dist);



            // Apply this rotation to the rigidbody's rotation.
            //  m_Rigidbody.MoveRotation(turnRotation); 
            //   m_Rigidbody.eRotation(m_Rigidbody.rotation * turnRotation);
            //   m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
            //  transform.localRotation = turnRotation;
            //  //   transform.Rotate(m_Rigidbody.centerOfMass.,);
            //  transform.Rotate( Vector3.left, 2* Time.deltaTime);

        }
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