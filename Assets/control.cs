using UnityEngine;
using System.Collections;

public class control : MonoBehaviour
{

    // Use this for initialization
    private const int ATMOSPHERE_BORDER = 2026;

    public int force;

    public int speed;
    public int rotationSpeed;

    public GameObject terrain;
    public Material skybox;
    public GameObject planet;
    public GameObject capsuleEngine;

    public ParticleSystem particleSystem;
    public ParticleSystem particleSystemW;
    public ParticleSystem particleSystemS;
    public ParticleSystem particleSystemA;
    public ParticleSystem particleSystemD;
    public GameObject testParticles;

    private float throtle = 0;
    private float backThrotle = 0;
    private float turnThrotle = 0;


    private Vector3 gravitationalAcceleration = new Vector3(9.71f, 9.71f, 9.71f);

    private Rigidbody m_Rigidbody;
    private Rigidbody c_Rigidbody;

    private bool terrainHidden = false;
    private bool engineStarded = false;
    public bool isMovable = false;
    private bool inAir = false;
    private static float height;
    private string heightText = "";
    private string speedText = "";
    private ParticleSystem[] particles;
    private bool customGravity;
    private bool startFire;

    private bool inAtmospfere = true;
    private bool wasOutSideAtmosfhere = false;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.centerOfMass = new Vector3(0, 11, 0);
        c_Rigidbody = capsuleEngine.GetComponent<Rigidbody>();
        particles = testParticles.GetComponentsInChildren<ParticleSystem>();
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
        Destroy(gameObject);
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
            m_Rigidbody.AddForce(transform.up * 80000 * throtle);
            if (throtle == 0)
            {
                engineStarded = false;
                particleSystem.Stop();
            }

            if (inAir)
            {
                backThrotle = Input.GetAxis("RacketHorizontal");
                float backMove = backThrotle * rotationSpeed * Time.deltaTime;
                c_Rigidbody.AddForce(transform.right * 500 * backThrotle);

                //Debug.Log("backMove " + backMove + " backThrotle " + backThrotle);

                turnThrotle = Input.GetAxis("RacketVertical");
                float turnMove = turnThrotle * rotationSpeed * Time.deltaTime;
                c_Rigidbody.AddForce(transform.forward * -500 * turnThrotle);

                //Debug.Log("turnMove " + turnMove + " turnThrotle " + turnThrotle);

                generateSideParticles();
                //transform.Rotate(-turnMove, 0, -backMove);
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Wysokość " + heightText);
        GUI.Label(new Rect(10, 20, 100, 20), "Szybkość " + speedText);
    }

    void Update()
    {

        heightText = ((int)height - 1026).ToString();
        speedText = m_Rigidbody.velocity.magnitude.ToString();

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
        Vector3 v = planet.transform.position - transform.position;
        height = Vector3.Distance(planet.transform.position, transform.position);

        if (inAtmospfere && wasOutSideAtmosfhere && !startFire  && inAir)
        {
            startFire = true;
            foreach (ParticleSystem p in this.particles)
            {
                p.Play();
            }

        }



        if (!wasOutSideAtmosfhere && height > ATMOSPHERE_BORDER && inAir ) {
            wasOutSideAtmosfhere = true;
        }

        inAtmospfere = (height <= ATMOSPHERE_BORDER) ? true : false;

        if (inAtmospfere && inAir && startFire && wasOutSideAtmosfhere)
        {
            foreach (ParticleSystem p in this.particles)
            {
                p.transform.rotation = Quaternion.LookRotation(-m_Rigidbody.velocity, Vector3.up);
                // p.startSpeed = 1.0f + (1 - (1 / m_Rigidbody.velocity.magnitude) * 6.0f);
                p.startSpeed =  ((300f/(height-1023) ) * 12.0f);
           //     particleSystem.startSpeed = 2.0f + (throtle * 3.0f);

            }
        }

        if (!inAir && transform.position.y > 12.6f)
        {
            inAir = true;
            m_Rigidbody.useGravity = true;
            c_Rigidbody.useGravity = true;
        }
        if (transform.position.y > 100 && !customGravity)
        {
            customGravity = true;
            m_Rigidbody.useGravity = false;
            c_Rigidbody.useGravity = false;
        }

       
        if (customGravity)
        {   
            if (height <= 10000)
            {
                
                m_Rigidbody.AddForce(v.normalized * (1.0f - height / 10000) * 10000);
                
              //  testParticle
                
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

