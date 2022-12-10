using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public float health;
    public int attack;
    public float moveSpeed = 3f;
    private Animator anim;
    public Rigidbody2D thisRigid;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    public Transform[] setPath;
    public int currentPoint;
    public Transform currentGoal;
    public float distanceFromPoint; //we can use ints to be exact, but floats have a more smooth transition


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        thisRigid = GetComponent<Rigidbody2D>();
  

        // set the bool for awake to be true
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }


    public void CheckDistance()
    {
        if(Vector2.Distance(target.position, transform.position) <= chaseRadius && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
           Vector3 thisVector = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
           ChangeTheAnimationDirection(thisVector - transform.position); // why? what?
           thisRigid.MovePosition(thisVector); //why do we do this??
                                                   
         
        } else if (Vector2.Distance(target.position, transform.position) > chaseRadius)
        {
            if(Vector2.Distance(transform.position, setPath[currentPoint].position) > distanceFromPoint)
            {
                Vector3 thisVectorPoint = Vector2.MoveTowards(transform.position, setPath[currentPoint].position, moveSpeed * Time.deltaTime);
                ChangeTheAnimationDirection(thisVectorPoint - transform.position); //why
                thisRigid.MovePosition(thisVectorPoint); // why
            } else
            {
                // if our position from and to the path is greater than the radius of that point, then we will move towards the next, however, if it is not
                // them we will move towards the current path 
                ChangeThePath();
            }
           
        }
    }

    private void ChangeThePath()
    {
        if(currentPoint == setPath.Length - 1)
        {
            currentPoint = 0;
            currentGoal = setPath[0];
        } else
        {
            currentPoint++;
            currentGoal = setPath[currentPoint];
        }
    }


    private void SetAnimationDirection(Vector2 settingVector)
    {
        anim.SetFloat("moveX", settingVector.x);
        anim.SetFloat("moveY", settingVector.y);

    }

    public void ChangeTheAnimationDirection(Vector2 theDirection)
    {
        if(Mathf.Abs(theDirection.x) > Mathf.Abs(theDirection.y))
        {
            if (theDirection.x > 0)
                SetAnimationDirection(Vector2.right);
            else if (theDirection.x < 0)
                SetAnimationDirection(Vector2.left);
        } else if (Mathf.Abs(theDirection.y) > Mathf.Abs(theDirection.x))
        {
            if (theDirection.y > 0)
                SetAnimationDirection(Vector2.up);
            else if (theDirection.y < 0)
                SetAnimationDirection(Vector2.down);
        }
    }


    private void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            //set bool to be true for death animation
            StartCoroutine(TakeDamage());
        }
    }


    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);
    }


}
