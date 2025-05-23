using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI numZomText;

    [SerializeField] private TextMeshProUGUI cleared;


    [SerializeField] private GameObject zombie1Prefab;
    [SerializeField] private GameObject zombie2Prefab;
    [SerializeField] private GameObject zombie3Prefab;



    private float[] choices = { -23, 23 };

    public int health = 100;
    public int level = 1;
    public int numZombies = 5;
    public int numZombiesLeft = 5;



    
    void Start()
    {
        
        for (int i = 0; i < numZombies * level; i++)
        {
            float choiceZom = Random.Range(0, 10);

            float choiceX = choices[Random.Range(0, choices.Length)];
            float choiceZ = choices[Random.Range(0, choices.Length)];

            if(choiceZom < 5)
                Instantiate(zombie1Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);
            else if(choiceZom < 8)
                Instantiate(zombie2Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);
            else
                Instantiate(zombie3Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);


        }


    }

  
    void Update()
    {
        levelText.text = "Level " + level;
        healthText.text = "HP:" + health;
        numZomText.text = "Zombies Left:" + numZombiesLeft;


        if (numZombiesLeft == 0)
        {
            StartCoroutine("clearedActive");
          
            level++;

            for (int i = 0; i < numZombies * level; i++)
            {
                float choiceZom = Random.Range(0, 10);

                float choiceX = choices[Random.Range(0, choices.Length)];
                float choiceZ = choices[Random.Range(0, choices.Length)];

                if (choiceZom < 5)
                    Instantiate(zombie1Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);
                else if (choiceZom < 8)
                    Instantiate(zombie2Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);
                else
                    Instantiate(zombie3Prefab, new Vector3(choiceX, 0, choiceZ), Quaternion.identity);
            }

            numZombiesLeft = numZombies * level;

        }
    }

    IEnumerator clearedActive()
    {
        cleared.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cleared.gameObject.SetActive(false);
    }

}
