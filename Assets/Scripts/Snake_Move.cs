using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake_Move : MonoBehaviour {

    bool Dead,ate = false;
    public GameObject deadscrn;
    public Text resulttxt;

    public GameObject tailPrefab;
    public RectTransform referenceHandle;

    public GameObject SnakeHead;
    Vector2 directionSnake = Vector2.right;

    List<Transform> tail = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        // Move the Snake every 300ms
        deadscrn.SetActive(false);
        PlayerPrefs.SetInt("ScoreP", 0);
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tail.Count<8) {
            ate = true;
        }

        if (!Dead)
        {

            if (referenceHandle.anchoredPosition3D.x < -25 && PlayerPrefs.GetInt("helpmove") == 1)
            {

                SnakeHead.GetComponent<Transform>().Rotate(0, 0, (SnakeHead.GetComponent<Transform>().rotation.z + 90));
                PlayerPrefs.SetInt("helpmove", 0);

            }
            else if (referenceHandle.anchoredPosition3D.x > 25 && PlayerPrefs.GetInt("helpmove") == 1)
            {
                SnakeHead.GetComponent<Transform>().Rotate(0, 0, (SnakeHead.GetComponent<Transform>().rotation.z - 90));
                PlayerPrefs.SetInt("helpmove", 0);

            }
        }
        else {
            //Time.timeScale = 0;
            deadscrn.SetActive(true);
            resulttxt.text = PlayerPrefs.GetInt("ScoreP").ToString();
        }
    }

    void Move()
    {
        if (!Dead)
        {
            Vector2 v = transform.position;

            transform.Translate(directionSnake);

            if (ate&&tail.Count<8)
            {
                GameObject g = (GameObject)Instantiate(tailPrefab,
                                  v,
                                  Quaternion.identity);

                tail.Insert(0, g.transform);

                ate = false;
            }
            else if (tail.Count > 0)
            {   
                tail.Last().position = v;

                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Food"))
        {
            ate = true;
            PlayerPrefs.SetInt("ScoreP", PlayerPrefs.GetInt("ScoreP") + 1);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "border") {
            SnakeHead.GetComponent<Transform>().Rotate(0, 0, (SnakeHead.GetComponent<Transform>().rotation.z + 180));
        } else{   
            Dead = true;
        }
    }
}
