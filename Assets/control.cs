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
    public GameObject planet;

    public ParticleSystem particleSystem;
    public ParticleSystem particleSystemW;
    public ParticleSystem particleSystemS;
    public ParticleSystem particleSystemA;
    public ParticleSystem particleSystemD;

    private float throtle = 0;
    private float backThrotle = 0;
    private float turnThrotle = 0;


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

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision");
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

            particleSystem.startSpeed = 2.0f + (throtle * 3.0f);
            m_Rigidbody.AddForce(transform.up * 10000 * throtle);
            if (throtle == 0)
            {
                engineStarded = false;
                particleSystem.Stop();
            }

            if (inAir)
            {
                backThrotle = Input.GetAxis("RacketHorizontal");
                float backMove = backThrotle * rotationSpeed * Time.deltaTime;

                Debug.Log("backMove " + backMove + " backThrotle " + backThrotle);

                turnThrotle = Input.GetAxis("RacketVertical");
                float turnMove = turnThrotle * rotationSpeed * Time.deltaTime;

                Debug.Log("turnMove " + turnMove + " turnThrotle " + turnThrotle);

                generateSideParticles();
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

        heightText = ((int)height - 1026).ToString();

        moveInput();

        if (transform.position.y > 200)
        {
            if (!terrainHidden)
            {
                RenderSettings.skybox = skybox;
                terrain.SetActiveRecursively(false);
                terrainHidden = true;
            }
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!inAir && transform.position.y > 12.6f)
        {
            inAir = true;
            m_Rigidbody.useGravity = true;
        }
        if (transform.position.y > 100 && !customGravity)
        {
            customGravity = true;
            m_Rigidbody.useGravity = false;
        }

        height = Vector3.Distance(planet.transform.position, transform.position);

        if (customGravity)
        {
            if (height <= 10000)
            {
                Vector3 v = planet.transform.position - transform.position;
                m_Rigidbody.AddForce(v.normalized * (1.0f - height / 10000) * 1000);
                
            }
            
        }
    }

    private void generateSideParticles()
    {
        //w -1 s 1 turn a -1 d 1 back
        if (turnThrotle > 0)
            particleSystemS.Play();
        else
            particleSystemS.Stop();

        if (turnThrotle < 0)
            particleSystemW.Play();
        else
            particleSystemW.Stop();
        
        if (backThrotle > 0)
            particleSystemD.Play();
        else
            particleSystemD.Stop();

        if (backThrotle < 0)
            particleSystemA.Play();
        else
            particleSystemA.Stop();
    }
}

