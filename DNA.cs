using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    List<int> genes=new List<int>();
    public int dnaLenght = 0;
    public int maxValue = 0;
    public DNA(int dnaLenght,int maxValue)
    {
        this.dnaLenght = dnaLenght;
        this.maxValue = maxValue;
        randomize();
    }
    private void randomize()
    {
        genes.Clear();
        for(int i=0;i<dnaLenght;i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }
    public void setVal(int pos,int val)
    {
        genes[pos] = val;
    }
    public void combine(DNA d1, DNA d2)
    {
        for (int i = 0; i < dnaLenght; i++)
        {
            if (i < (int)dnaLenght / 2)
            {
                this.genes[i] = d1.genes[i];
            }
            else
            {
                this.genes[i] = d2.genes[i];
            }
        }
    }
    public void mutate()
    {
        genes[Random.Range(0, dnaLenght)] = Random.Range(0, maxValue);
    }
    public int getGene(int pos)
    {
        return genes[pos];
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
