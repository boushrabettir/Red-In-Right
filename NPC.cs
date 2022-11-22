using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour    
{


    public GameObject dialogueEntire;
    public Text dialogueText;
    public string[] sentences; //dialogue
    private int positionInString; //index
    public float wordSpeed;
    public bool isPlayerInRange;
    public Button button;
    public Button yesButton;
    public GameObject contextClue;
 
   
   
    
    private Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();

    }
   

    // Update is called once per frame
   public void Update()
    {
    

        if (Input.GetKeyDown(KeyCode.O) && isPlayerInRange)
        {
            if (dialogueEntire.activeInHierarchy)
            {
                restart(); //calling 
                dialogueEntire.SetActive(false);
     
                
            }
            else
            {
                dialogueEntire.SetActive(true);
                
                StartCoroutine(wordsInside());

                GetComponent<Collider2D>().enabled = false;

                contextClue.SetActive(false);
               
              
            }
        }

        if (dialogueText.text == sentences[positionInString])
        {
            button.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = false;

        }


      
    }

  




    public void nextLine()
    {

        button.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        //this function will allow us to go to the next line so it doesnt go past the screen
        if (positionInString < sentences.Length - 1)
        {
            //if the position in the string is less than the dialogue length -1
            positionInString++; // goes to the next line
            dialogueText.text = "";
            StartCoroutine(wordsInside());
           
        }
        else
        {
            restart();
            //if not it goes to false and disables
        }

    }

    

    public void restart()
    {
        //this function is to disable our dialogue and deletes or restarts
        dialogueText.text = "";
        positionInString = 0;
        dialogueEntire.SetActive(false);
    }


    IEnumerator wordsInside()
    {
        foreach (char letter in sentences[positionInString].ToCharArray())
        {
            dialogueText.text += letter; // this loop adds on a letter 
            yield return new WaitForSeconds(wordSpeed); // it will yield for the amount of wordSpeed which we will input
        }
    }

   
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            contextClue.SetActive(true);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            contextClue.SetActive(false);
            restart(); // why are we calling the function twice?
        }
    }


}