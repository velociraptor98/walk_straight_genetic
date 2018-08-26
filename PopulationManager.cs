using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject bot;
    public int population = 50;
    List<GameObject> pop = new List<GameObject>();
    public static float elapsed = 0.0f;
    public float trial = 5;
    public int generation = 1;
    GUIStyle gui = new GUIStyle();
    private void OnGUI()
    {
        gui.fontSize = 30;
        gui.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(0, 0, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "stats", gui);
        GUI.Label(new Rect(10,25,200,30),"Gen: "+generation,gui);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time:{0:0.00}", elapsed), gui);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population, gui);
        GUI.EndGroup();
    }

    GameObject Breed(GameObject param1,GameObject param2)
    {
        Vector3 starting = new Vector3(this.transform.position.x + Random.Range(-2, 2), this.transform.position.y, this.transform.position.z + Random.Range(-2, 2));
        GameObject child = Instantiate(bot, starting, this.transform.rotation);
        Brain b = child.GetComponent<Brain>();
        if(Random.Range(0,100)==10)
        {
            b.init();
            b.startPosition = starting;
            b.dna.mutate();
        }
        else
        {
            b.init();
            b.startPosition = starting;
            b.dna.combine(param1.GetComponent<Brain>().dna, param2.GetComponent<Brain>().dna);
        }
        return child;
    }
    void BreedPopulation()
    {
        List<GameObject> sorted = pop.OrderBy(o => o.GetComponent<Brain>().distance).ToList();
        pop.Clear();
        //breed the longest lasting bots
        for(int i=(int)(population/2)-1;i<population-1;i++)
        {
            pop.Add(Breed(sorted[i],sorted[i+1]));
            pop.Add(Breed(sorted[i + 1], sorted[i]));
        }
        for(int i=0;i<sorted.Count();i++)
        {
            Destroy(sorted[i]);
        }
        ++generation;
    }

    // Use this for initialization
    void Start () {
		for(int i=0;i<population;i++)
        {
            Vector3 starting = new Vector3(this.transform.position.x+Random.Range(-2,2),this.transform.position.y,this.transform.position.z+Random.Range(-2,2));
            GameObject b = Instantiate(bot,starting,this.transform.rotation);
            b.GetComponent<Brain>().init();
            b.GetComponent<Brain>().startPosition = starting;
            pop.Add(b);
        }
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if(elapsed>trial)
        {
            BreedPopulation();
            elapsed = 0;
        }
		
	}
}
