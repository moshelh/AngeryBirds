using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _bridWasLaunched;
    private float _timeSittingAround;

    [SerializeField]
    private float _launchPower = 250;

    private void Awake()
    {
        _initialPosition = transform.position;

    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        checkPosition();
    }

    private void checkPosition()
    {
        if(_bridWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }
        if (transform.position.y > 10 ||
            transform.position.y <-10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 || 
            _timeSittingAround >3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;

    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPostion = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPostion  * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _bridWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag()
    {
        Vector3 newPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position =  new Vector3(newPostion.x,newPostion.y,0);
    }
}
