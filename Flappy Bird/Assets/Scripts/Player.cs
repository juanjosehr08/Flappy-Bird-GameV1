using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Vector3 direction;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform trans;
    [SerializeField] public Animator anim;
    [SerializeField] public AudioSource fuaaa;



    public bool shieldOn;

    private bool jumped;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        

    }

    public void Start()
    {
        
        Invoke("BodyTypeKinematic", 0f);
        transform.position = new Vector2(-16, 0);
        Invoke("BodyTypeDynamic", 0.2f);
        anim.SetBool("touchingObstacle", false);

    }
    void BodyTypeKinematic()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }
    void BodyTypeDynamic()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {

            FindObjectOfType<GameManager>().DelayGameOver();
            anim.SetBool("touchingObstacle", true);


        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();

        }
        
        else if (other.gameObject.tag == "Enemy")
        {
            

        }
        
        else if (other.gameObject.tag == "Enemy" && shieldOn == false)
        {
            FindObjectOfType<GameManager>().DelayGameOver();
            anim.SetBool("touchingObstacle", true);           

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            anim.SetBool("cuchoHits", true);
        }
    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            jumped = true;                     


        }
        if (jumped == true && rb.velocity.y < 0)
        {
            jumped = false;
        }
                
        
        
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("cuchoFua", true);
            BlinkObjectNow();                    

        } 

        if(anim.GetBool("cuchoHits"))
        {
            
        }
    }

    /*
    public void cuchoHits()
    {
        fuaaa.Play();
    }
    */


    private void BlinkObjectNow()
    {
        StartCoroutine(CuchoShield());

    }   
    

    IEnumerator CuchoShield()
    {
        
        transform.localScale = new Vector2(0.7f, 0.7f);
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("cuchoFua", false);
        transform.localScale = new Vector2(0.6f, 0.6f);        
        shieldOn = false;   

    }
    

}
