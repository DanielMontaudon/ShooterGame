using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform target;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject healPowerPrefab;
    [SerializeField] private GameObject slowPowerPrefab;
    [SerializeField] private GameObject speedPowerPrefab;
    [SerializeField] private GameObject jumpPowerPrefab;

    public bool triedDropChance = false;

    public float zHealth = 100f;
    private float distanceFromPlayer;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        distanceFromPlayer = Vector3.Distance(target.transform.position, transform.position);

        anim.SetFloat("distance", distanceFromPlayer);

        if(distanceFromPlayer <= 2)
        {
            StartCoroutine("pause");
        }

        if (zHealth <= 0 && !triedDropChance)
        {

            float powerUpChance = Random.Range(0, 100);

            if (powerUpChance == 1)
                Instantiate(healPowerPrefab, transform.position, Quaternion.identity);
            else if(powerUpChance == 2)
                Instantiate(slowPowerPrefab, transform.position, Quaternion.identity);
            else if (powerUpChance == 3)
                Instantiate(speedPowerPrefab, transform.position, Quaternion.identity);
            else if (powerUpChance == 4)
                Instantiate(jumpPowerPrefab, transform.position, Quaternion.identity);


            anim.SetBool("isDead", true);
            gameController.numZombiesLeft--;
            agent.isStopped = true;
            Destroy(gameObject, 3);
            triedDropChance = true;

        }


        //Debug.Log(distanceFromPlayer);
    }

    IEnumerator pause()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(2);
        agent.isStopped = false;

    }


}
