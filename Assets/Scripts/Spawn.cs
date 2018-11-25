using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spawn : MonoBehaviour
{

    public GameObject foodPrefab, poisonPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public Camera cam;
    float width;



    // Use this for initialization
    void Start()
    {
        width = 80; //(40 / 203)*406;
        Camera.main.orthographicSize = (width / Screen.width) * Screen.height;
        print(width);
        InvokeRepeating("Spawn1", 3, 8);
        InvokeRepeating("Spawn2", 10, 16);
    }

    void Spawn1()
    {
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity);
    }
    void Spawn2()
    {
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        Instantiate(poisonPrefab,
                    new Vector2(x, y),
                    Quaternion.identity);
    }
    public void restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        
    }
}
