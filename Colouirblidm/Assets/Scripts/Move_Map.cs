using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Map : MonoBehaviour
{
    private Vector3 selected;
    public GameObject Placeholder;
    public Camera topCamera;
    private Ray ray;
    private bool canMove = false;
    private Vector3 distanceDifference;
    public Landscape_controller landscapeScript;
    private bool playerOn;
    public bool win;
    private int positionsCorrect = 0;
    public bool easymode;
    public GameObject[] currentPositions;
    public Vector3[] EndPositions;
    Win_Controller Win_Controller;
    private bool firstCorrect = false;
    private bool secondCorrect = false;

    private string colour;
    void Start()
    {
        currentPositions = GameObject.FindGameObjectsWithTag("Moveable");
        Win_Controller = GameObject.FindObjectOfType<Win_Controller>();
    }
    void Update()
    {
        //mouse click to slide puzzle
        if(Input.GetMouseButtonDown(0))
        {
            //ray cast stuff that i dont really get
            RaycastHit raycastHit;
            ray = topCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out raycastHit, 100f))
            {
                if(raycastHit.transform != null)
                {
                    //gets refence to object clicked on
                    if(raycastHit.collider.gameObject.CompareTag("Moveable"))
                    {
                        Placeholder = raycastHit.collider.gameObject;
                        landscapeScript = Placeholder.GetComponent<Landscape_controller>();
                        playerOn = landscapeScript.isTouchingPlayer;
                        canMove = true;  
                    }
                    //allows you to move the object that was clicked
                    if(canMove == true && playerOn == false)
                    {
                        selected = raycastHit.transform.position;
                        distanceDifference = Placeholder.transform.position - selected;
                        Debug.Log(distanceDifference);
                        Debug.Log(playerOn);
                        Debug.Log(canMove);
                        if (((distanceDifference.x == 20 || distanceDifference.x == -20) && distanceDifference.z == 0) || (distanceDifference.z == 20 || distanceDifference.z == -20) && distanceDifference.x == 0)
                        {
                            Placeholder.transform.position = selected;
                            currentPositions = GameObject.FindGameObjectsWithTag("Moveable");
                            //checks to see if current tile positions are the same as win condition
                            if (currentPositions.Length == EndPositions.Length)
                            {

                                AreAllCorrect();
                                CanFinishLevel();
                                Debug.Log(AreAllCorrect());
                                
                                
                            }
                        }
                    }
                }
            }
        }
    }
    //checks to see if all the landscape pieces are in the correct position to finish the level
    private bool AreAllCorrect()
    {
        if (easymode == false)
        {
            for (int i = 0; i < currentPositions.Length; i++)
            {
                if (currentPositions[i].transform.position == EndPositions[i])
                {
                    positionsCorrect++;

                    if (positionsCorrect == 8)
                    {
                        positionsCorrect = 0;
                        return true;
                    }
                }
            }
        }
        if (easymode == true)
        {

            int redPosCorrect = 0;
            int greenPosCorrect = 0;

            colour = landscapeScript.colours;

            if (colour == "orange")
            {
                for (int i = 0; i < currentPositions.Length / 2; i++)
                {
                    for (int n = 0; n < currentPositions.Length / 2; n++)
                    {
                        if (currentPositions[i].transform.position == EndPositions[n])
                        {
                            greenPosCorrect++;
                            Debug.Log(greenPosCorrect);
                            if (greenPosCorrect == currentPositions.Length / 2)
                            {
                                firstCorrect = true;
                            }
                            else
                            {
                                firstCorrect = false;
                            }

                        }
                    }
                }
            }

            if (colour == "blue")
            {
                for (int i = currentPositions.Length / 2; i < currentPositions.Length; i++)
                {
                    for (int n = (currentPositions.Length / 2); n < currentPositions.Length; n++)
                    {
                        if (currentPositions[i].transform.position == EndPositions[n])
                        {
                            redPosCorrect++;
                            Debug.Log(redPosCorrect);
                            if (redPosCorrect == currentPositions.Length / 2)
                            {
                                secondCorrect = true;
                            }

                            else
                            {
                                secondCorrect = false;
                            }


                        }
                    }
                }
            }


            if (firstCorrect == true && secondCorrect == true)
            {

                return true;
            }

        }
        positionsCorrect = 0;
        return false;      
    }
    public void CanFinishLevel()
    {
        if (AreAllCorrect() == true)
        {
            Win_Controller.FinishIsActive();
        }
        if (AreAllCorrect() == false)
        {
            Win_Controller.FinishIsntActive();
        }
        
    }
    
}
      
