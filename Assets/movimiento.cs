using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class movimiento : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    private Rigidbody playerRb;
    private int count;
    private GameObject[] PowerUp;
    private Vector3 starPos;
    public float force;
    public Text ins;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        PowerUp = GameObject.FindGameObjectsWithTag("PowerUp");
        starPos = playerRb.position;
        count = 0;
        UpdateScoreText();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRb.AddForce(move * speed);
        
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();

        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PowerUp"))
        {
            collider.gameObject.SetActive(false);
            count++;
            UpdateScoreText();
            if (count == 4)
            {
                Destroy(collider.gameObject);
                ins.text = "Felicidades!!!! Completaste el juego ahora eres inmortal, gracias por jugar";

                scoreText.text = "Juego Completado!";
            }

        }
        else if (collider.gameObject.CompareTag("Trampa"))
        {
            resetGameState();


        }
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + count;

    }
    void resetGameState()
    {
        count = 0;
        UpdateScoreText();
        for (int i = 0; i < PowerUp.Length; i++)
        {
            PowerUp[i].gameObject.SetActive(true);
        }
        playerRb.position = starPos;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
    private void Jump()
    {
        if (playerRb && Mathf.Abs(playerRb.velocity.y) < 0.05f)
            playerRb.AddForce(0, force, 0, ForceMode.Impulse);

    }
}