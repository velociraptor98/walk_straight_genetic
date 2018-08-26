using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour {
    public int dnaLength = 1;
    public Vector3 startPosition;
    public float timeAlive;
    public float distance;
    public DNA dna;
    public ThirdPersonCharacter character;
    private Vector3 move;
    private bool jump;
    private bool alive = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="dead")
        {
            alive = false;
        }
    }
    private void FixedUpdate()
    {
        /* 
         * 0 -forward
         * 1 -back
         * 2 -right
         * 3 -left
         * 4 -jump
         * 5 -crouch*/
        int h = 0;
        int v = 0;
        bool isCrouching = false;
        if (dna.getGene(0) == 0)
            v = 1;
        else if (dna.getGene(0) == 1)
            v = -1;
        else if (dna.getGene(0) == 2)
            h = -1;
        else if (dna.getGene(0) == 3)
            h = 1;
        else if (dna.getGene(0) == 4)
            jump = true;
        else if (dna.getGene(0) == 5)
            isCrouching = true;
        move = v * Vector3.forward + h * Vector3.right;
        character.Move(move, isCrouching, jump);
        jump = false;
        if(alive)
        {
            timeAlive += Time.deltaTime;
            distance += Vector3.Distance(this.transform.position, startPosition);
        } 
        
    }
    public void init()
    {
        dna = new DNA(dnaLength, 6);
        character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

}
