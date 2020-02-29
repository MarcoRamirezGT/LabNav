using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NavMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public float speed;
    public Text scoreText;
    private Rigidbody playerRb;
    private int count;
    private GameObject[] PowerUp;
    private Vector3 starPos;
    public float force;
   

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
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray1, out hit))
            {
                agent.SetDestination(hit.point);
            }
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
                scoreText.text = "Felicidades!!!! Completaste el juego ahora eres inmortal, gracias por jugar";   
            }

        }
        else if (collider.gameObject.CompareTag("Trampa"))
        {
            Destroy(this.gameObject);

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
}
