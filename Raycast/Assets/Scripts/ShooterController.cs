using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class ShooterController : MonoBehaviour
{

    [SerializeField] private GameController gameController;
    [SerializeField] private Shoot shootController;
    [SerializeField] private RigidbodyFirstPersonController fpc;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (!shootController)
            shootController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Shoot>();
        if (!fpc)
            fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "zombie")
        {
            //Debug.Log(gameController.health);
            gameController.health -= 10;
            if (gameController.health <= 0)
            {
                Cursor.lockState = CursorLockMode.Confined;

                SceneManager.LoadScene(0);
            }

        }
        if (collision.gameObject.tag == "zombie2")
        {
            //Debug.Log(gameController.health);
            gameController.health -= 25;
            if (gameController.health <= 0)
            {
                Cursor.lockState = CursorLockMode.Confined;

                SceneManager.LoadScene(0);
            }

        }
        if (collision.gameObject.tag == "zombie3")
        {
            //Debug.Log(gameController.health);
            gameController.health -= 30;
            if (gameController.health <= 0)
            {
                Cursor.lockState = CursorLockMode.Confined;

                SceneManager.LoadScene(0);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Speed")
        {
            AudioSource s = other.GetComponent<AudioSource>();
            s.Play();

            fpc.movementSettings.speedup += 1;

            Destroy(other.gameObject, 1);
        }
        if (other.gameObject.tag == "Heal")
        {
            AudioSource s = other.GetComponent<AudioSource>();
            s.Play();

            if (gameController.health < 100)
                gameController.health += 10;

            Destroy(other.gameObject, 1);
        }
        if (other.gameObject.tag == "Fire")
        {
            AudioSource s = other.GetComponent<AudioSource>();
            s.Play();

            shootController.fireRatePower += 3;

            Destroy(other.gameObject, 1);
        }
        if (other.gameObject.tag == "Jump")
        {
            AudioSource s = other.GetComponent<AudioSource>();
            s.Play();

            fpc.movementSettings.jumpHeight += 1;

            Destroy(other.gameObject, 1);
        }
    }
}
