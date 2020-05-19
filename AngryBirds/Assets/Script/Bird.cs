using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField]
    private float range_size = 12f;

    [SerializeField]
    private float _launchPower = 250;

    [SerializeField]
    private float wait_time = 4f;

    public static int numOfLifes = 3;

    [SerializeField]
    private Text healthBar;


    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        healthBar.text = "Health: " + numOfLifes;
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        checkPosition();
    }

    private void checkPosition()
    {
        if(_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > range_size ||
            transform.position.y < -range_size ||
            transform.position.x > range_size ||
            transform.position.x < -range_size || 
            _timeSittingAround > wait_time)
        {   
            if(_birdWasLaunched){
                numOfLifes--;
                _birdWasLaunched = false;
            }
            if(numOfLifes == 0){
                SceneManager.LoadScene(0);
            }
            resetPosition();
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Collider2D>().enabled = true;

        Vector2 directionToInitialPostion = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPostion  * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag()
    {
        Vector3 newPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position =  new Vector3(newPostion.x,newPostion.y,0);
    }

    private void resetPosition() {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.position = _initialPosition;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
}
