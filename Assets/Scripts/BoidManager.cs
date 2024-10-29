using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] public BoidComponent boidTemplate;
    [SerializeField] public int boidNum;
    [SerializeField] [Range(0.0001f, 1)] public float boidCenterInfluence = 0.5f;
    [SerializeField][Range(0.0001f, 1)] public float boidBoidInfluence = 0.5f;
    [SerializeField][Range(1, 100)] public float boidSpeed = 1;
    [SerializeField][Range(0.0001f, 0.1f)] public float boidCorneringSpeed = 0.5f;

    private float setSpeed;
    private float setCenterInfluence;
    private float setBoidInfluence; 
    private float setCornering;
    private List<BoidComponent> boids = new List<BoidComponent>();
    public Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = boidSpeed;
        setCornering = boidCorneringSpeed; 
        setCenterInfluence = boidCenterInfluence;
        setBoidInfluence = boidBoidInfluence;

        for(int i = 0; i < boidNum; i++)
        {
            BoidComponent tempBoid = BoidComponent.Instantiate(boidTemplate);
            tempBoid.setCenterInfluence(boidCenterInfluence);
            tempBoid.setSpeed(boidSpeed); 
            tempBoid.transform.position = new Vector3(
                Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            boids.Add(tempBoid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(boidSpeed != setSpeed || boidCenterInfluence != setCenterInfluence || 
            boidBoidInfluence != setBoidInfluence || boidCorneringSpeed != setCornering)
        {
            foreach (BoidComponent boid in boids)
            {
                boid.setSpeed(boidSpeed);
                boid.setCenterInfluence(boidCenterInfluence);
                boid.setCorneringSpeed(boidCorneringSpeed);
            }

            setSpeed = boidSpeed;
            setCenterInfluence = boidCenterInfluence;
            setCornering = boidCorneringSpeed; 
        }
    }

    private void FixedUpdate()
    {
        foreach (BoidComponent boid in boids)
        {
            boid.Move();
        }
    }
}