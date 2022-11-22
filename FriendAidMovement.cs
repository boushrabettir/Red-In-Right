using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAidMovement : MonoBehaviour
{

    public float stoppingDistance;
    public float speed;
    private Transform isFollowing;

    void Start()
    {

        isFollowing = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }

    void Update()
    {
        if(Vector3.Distance(transform.position, isFollowing.position) > stoppingDistance)
        {

            transform.position = Vector3.MoveTowards(transform.position, isFollowing.position, speed * Time.deltaTime);

        }


    }
}


