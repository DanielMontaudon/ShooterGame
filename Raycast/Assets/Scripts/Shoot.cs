using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shoot : MonoBehaviour
{


    [SerializeField] Transform FPSCam;
    [SerializeField] GameObject[] guns;
    [SerializeField] private int curGun = 0;
    [SerializeField] private TextMeshProUGUI M4A1Text;
    [SerializeField] private TextMeshProUGUI AK47Text;
    [SerializeField] private TextMeshProUGUI UMPText;

    [SerializeField] private GameObject pausedUI;
    public bool isPaused = false;
    public float fireRatePower = 0;
    private float nextShootTime = 0;

    void Start()
    {
        if(!FPSCam)
            FPSCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !isPaused) // Scroll Up
        {
            if(curGun == 0)
                curGun = Mathf.Abs((curGun - 2) % 3);
            else
                curGun = Mathf.Abs((curGun - 1) % 3);

            ChangeGun();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !isPaused) // Scroll Down
        {
            curGun = Mathf.Abs((curGun + 1)) % 3;
            ChangeGun();
        }
        if (Input.GetKey("1") && !isPaused)
        {
            curGun = 0;
            ChangeGun();
        }
        if (Input.GetKey("2") && !isPaused)
        {

            curGun = 1;
            ChangeGun();
        }
        if (Input.GetKey("3") && !isPaused)
        {
            curGun = 2;
            ChangeGun();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            isPaused = true;
            Time.timeScale = 0;
            pausedUI.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        //prevGun = curGun;
        Debug.DrawRay(FPSCam.position, FPSCam.forward, Color.red);
        if(Input.GetButton("Fire1") && Time.time > nextShootTime)
        {
            ShootGun();
            nextShootTime = Time.time + (1 / (guns[curGun].GetComponent<Gun>().shootRate + fireRatePower));
        }
    }

    void ShootGun()
    {
        AudioSource s = guns[curGun].GetComponent<AudioSource>();
        guns[curGun].GetComponent<Gun>().muzzleFlash.Play();


        if (s!=null)
            s.Play();
        if (Physics.Raycast(FPSCam.position, FPSCam.forward, out RaycastHit hitInfo, guns[curGun].GetComponent<Gun>().shootRange))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);

            if (hitInfo.collider.gameObject.tag == "zombie")
                hitInfo.collider.gameObject.GetComponent<ZombieController>().zHealth -= guns[curGun].GetComponent<Gun>().damage;
            if (hitInfo.collider.gameObject.tag == "zombie2")
                hitInfo.collider.gameObject.GetComponent<ZombieController>().zHealth -= guns[curGun].GetComponent<Gun>().damage;
            if (hitInfo.collider.gameObject.tag == "zombie3")
                hitInfo.collider.gameObject.GetComponent<ZombieController>().zHealth -= guns[curGun].GetComponent<Gun>().damage;

            guns[curGun].GetComponent<Gun>().hitFlash.transform.position = hitInfo.point;
            guns[curGun].GetComponent<Gun>().hitFlash.Play();
        }
    }

    void ChangeGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[curGun].SetActive(true);

        if(curGun == 0)
        {
            M4A1Text.fontSize = 25;
            AK47Text.fontSize = 22;
            UMPText.fontSize = 22;
        }
        else if(curGun == 1)
        {
            M4A1Text.fontSize = 22;
            AK47Text.fontSize = 25;
            UMPText.fontSize = 22;
        }
        else if(curGun == 2)
        {
            M4A1Text.fontSize = 22;
            AK47Text.fontSize = 22;
            UMPText.fontSize = 25;
        }
    }

    public void ResumeGame()
    {
        pausedUI.gameObject.SetActive(false);
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;

    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
