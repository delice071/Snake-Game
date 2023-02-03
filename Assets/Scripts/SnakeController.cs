using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 _areaLimit = new Vector2(13, 24);

    [SerializeField] private GameObject food;
    [SerializeField] private GameObject tailPrefabs;
    [SerializeField] private float speed = 1;
    
    [SerializeField] private TextMeshPro textGameOver;

    private Vector2 direction = Vector2.down;
    bool yenidenbaslakontrol = false;

    private List<Transform> _snake = new List<Transform>();

    private bool _grow;

    textt textscripti;
    
    
    
    private void Start()
    {
        textscripti = GameObject.Find("Snake").GetComponent<textt>();
        textGameOver.enabled = false;
        ChangePositionFood();
        StartCoroutine(Move());

        _snake.Add(transform);
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right || Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left || Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down || Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up || Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        if ( Input.GetKeyDown(KeyCode.R) && yenidenbaslakontrol)
        {
            SceneManager.LoadScene("levell");
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {

            if (_grow)
            {
                _grow = false;
                Grow();
            }


            for (int i = _snake.Count-1; i > 0; i--)
            {
                _snake[i].position = _snake[i - 1].position;
            }


            var position = transform.position;
            position += (Vector3)direction;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);
            transform.position = position;

          

            yield return new WaitForSeconds(speed);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            //Grow();
            _grow = true;


        }
        if (collision.CompareTag("Wall"))
        {
            Dead();

        }
    }
    private void Grow()
    {
        var tail = Instantiate(tailPrefabs);

        _snake.Add(tail.transform);
        _snake[_snake.Count - 1].position = _snake[_snake.Count - 2].position;

        ChangePositionFood();
        textscripti.yemekle();
    }

    private void ChangePositionFood()
    {
        var x = (int) Random.Range(1, _areaLimit.x);
        var y = (int) Random.Range(1, _areaLimit.y);
        food.transform.position = new Vector3(x, y, 0);

        
    }
    
    

    private void Dead()
    {
        textGameOver.enabled = true;

        StopAllCoroutines();
        yenidenbaslakontrol = true;
    }
    
}
