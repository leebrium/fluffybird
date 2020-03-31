using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public Vector2 pipeHeightRange;
    public int maxPipe;
    private GameObject[] pipes;
    public GameObject pipeFab;
    private int currentPipeIndex;
    public Vector2 pipeXRange;
    public float maxSpawnTime = 1;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        pipes = new GameObject[maxPipe];
        for(int i = 0; i<maxPipe; i++){
            pipes[i] = Instantiate(pipeFab);
        }
        currentPipeIndex = 0;
        pipes[currentPipeIndex].GetComponent<Pipe>().enableMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxSpawnTime){
            spawnPipe();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    public void spawnPipe(){
        if (currentPipeIndex == maxPipe - 1){
            currentPipeIndex = 0;
        } else {
            currentPipeIndex++;
        }
        pipes[currentPipeIndex].GetComponent<Pipe>().enableMoving();
    }
}
