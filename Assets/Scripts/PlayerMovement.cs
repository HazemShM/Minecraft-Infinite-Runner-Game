using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;   
    private Rigidbody rb;       

    private float jumpForce = 210f; 
    private bool isGrounded = true; 
    private bool isJumping = false;
    private bool lost = false;
    
    private int currentLane = 0;
    private float targetXPosition;

    private float playerMovementSpeed;  
    private float sideMoveSpeed = 5f;

    [SerializeField] Text scoreText;
    private int scoreRate;
    private float playerScore ;

    [SerializeField] Text SpeedText;
    private String playerSpeed ;
    private readonly float defaultSpeed = 2.5f;
    private bool initialWait = true;
    
    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        initialWait = false;  
    }
    private void Start()
    {   playerMovementSpeed = defaultSpeed; 
        sideMoveSpeed = 5f;
        isGrounded = true; 
        isJumping = false;
        lost = false;
        playerScore = 0;
        scoreRate = 1;
        rb = GetComponent<Rigidbody>();
        animator =GetComponent<Animator>();
        targetXPosition = transform.position.x;
        StartCoroutine(StartAfterDelay());
    }
    private void Update()
    {   
        if (initialWait)
            return;
        scoreText.text = ""+(int)playerScore;
        playerScore += scoreRate * Time.deltaTime;

        if(playerMovementSpeed == defaultSpeed){
            SpeedText.text = "Normal";
            SpeedText.color = new Color(11f / 255f, 138f / 255f, 128f / 255f);

        }else if(playerMovementSpeed == defaultSpeed*2){
            SpeedText.text = "High";
            SpeedText.color = new Color(217f / 255f, 102f / 255f, 33f / 255f);

        }
        
        // transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        transform.position += new Vector3(0,0,playerMovementSpeed)* Time.deltaTime;
        if(transform.position.y < -0.6){
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        if(!lost && transform.position.y < 0.7){
            Fall();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !lost && !(Time.timeScale == 0f)){   
            if(isGrounded && !(transform.position.y > 1.1)){
                isGrounded = false;
                isJumping = true;
                Jump();
            }else{
                AudioManager.manager.PlaySFX("Invalid");
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !lost && !(transform.position.y < 0.95) && !(Time.timeScale == 0f))
        {

            MoveLane(-1);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))& !lost && !(transform.position.y < 0.95) && !(Time.timeScale == 0f))
        {
            MoveLane(1);
        }
        float newXPosition = Mathf.MoveTowards(transform.position.x, targetXPosition, sideMoveSpeed * Time.deltaTime);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }

    private void MoveLane(int direction)
    {
        if(!((currentLane == 1 && direction == 1) || (currentLane == -1 && direction == -1))){
            targetXPosition += direction;
            // transform.position = transform.position + (new Vector3(direction,0,0));
            currentLane += direction;
        }else{
            AudioManager.manager.PlaySFX("Invalid");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            // print("in Ground");
        }
    }
    
    void Jump()
    {   
        // float height = GetComponent<Collider>().bounds.size.y;        
        if (isJumping) {
            // rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    public void StartRunning(){
        playerMovementSpeed = defaultSpeed*2;
    }
    public void StopRunning(){
        playerMovementSpeed = defaultSpeed;
    } 
    public void Fall(){
        lost = true;
        // print(lost);
        playerMovementSpeed = 0;
        // rb.AddForce(new Vector3(0,-20,0));
        animator.enabled =false;
        scoreRate = 0;
        rb.useGravity =false;

    }
    public void Die(){
        lost = true;
        // print(lost);
        playerMovementSpeed = 0;
        scoreRate = 0;
        // animator.enabled =false;
        animator.SetBool("isDead", true);
    }
    public bool GameOver()
    {
        return lost;
    }
    public void SetLost(bool lost){
        this.lost = lost;
    }
    public int GetScore(){
        return (int) playerScore;
    }

}








    //Rigidbody rb;

    //[Tooltip("[10, 30]")]
    //[SerializeField]
    //int speed = 10;

    //[SerializeField]
    //GameObject amongUsPrefab;

    //[SerializeField]
    //TMP_Text scoreText;

    //[SerializeField]
    //TMP_Text WinText;

    //int score;

    //// [SerializeField]
    //// GameObject enemy;
    //private void Awake()
    //{
    //    score = 0; 
    //    rb = GetComponent<Rigidbody>();

    //}

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    //private void FixedUpdate()
    //{    
    //    float x = Input.GetAxis("Horizontal");
    //    float y = Input.GetAxis("Jump");
    //    float z = Input.GetAxis("Vertical");

    //    rb.AddForce(new Vector3(x,y,z) * speed);
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    // if(other.gameObject.CompareTag("DestroyAll")){
    //    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    //     foreach(GameObject enemy in  enemies){
    //    //         Destroy(enemy);
    //    //     }
    //    // }
    //    if(other.gameObject.CompareTag("Goal")){
    //        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Obstacle");
    //        foreach(GameObject enemy in  enemies){
    //            Destroy(enemy);
    //        }
    //        WinText.SetText("You Win");
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // if(collision.gameObject.CompareTag("AmongUs")){
    //    //     score++;
    //    //     scoreText.SetText("Score: "+score);
    //    //     Destroy(collision.gameObject);
    //    //     GameObject instance = GameObject.Instantiate(amongUsPrefab);
    //    //     instance.transform.position = new Vector3(Random.Range(-9,3),1,Random.Range(-9,3));
    //    // }
    //    if(collision.gameObject.CompareTag("Obstacle")){

    //        Destroy(this.gameObject);
    //        // GameObject instance = GameObject.Instantiate(amongUsPrefab);
    //        // instance.transform.position = new Vector3(Random.Range(-9,3),1,Random.Range(-9,3));
    //    }
    //    if(collision.gameObject.CompareTag("Goal")){
    //        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Obstacle");
    //        foreach(GameObject enemy in  enemies){
    //            Destroy(enemy);
    //        }
    //        WinText.SetText("You Win");
    //    }
    //}

