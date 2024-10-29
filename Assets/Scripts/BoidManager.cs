using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] public BoidComponent boidTemplate;
    [SerializeField] public int boidNum;
    [SerializeField][Range(1, 180)] public float boidVisionCone;
    [SerializeField] [Range(0.0001f, 100)] public float boidCenterInfluence = 0.5f;
    [SerializeField][Range(0.0001f, 100)] public float boidAvoidInfluence = 0.5f;
    [SerializeField][Range(0.0001f, 100)] public float boidAlignInfluence = 0.5f;
    [SerializeField][Range(1, 500)] public float boidSpeed = 1;
    [SerializeField][Range(0.0001f, 1f)] public float boidCorneringSpeed = 0.5f;

    private float setSpeed;
    private float setVisionConeAngle; 
    private float setCenterInfluence;
    private float setAvoidInfluence;
    private float setAlignInfluence;
    private float setCornering;
    private List<BoidComponent> boids = new List<BoidComponent>();
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        setSpeed = boidSpeed;
        setVisionConeAngle = boidVisionCone; 
        setCornering = boidCorneringSpeed; 
        setCenterInfluence = boidCenterInfluence;
        setAlignInfluence = boidAlignInfluence;
        setAvoidInfluence = boidAvoidInfluence;

        for(int i = 0; i < boidNum; i++)
        {
            BoidComponent tempBoid = BoidComponent.Instantiate(boidTemplate);
            tempBoid.setCenterInfluence(boidCenterInfluence);
            tempBoid.setVisionConeAngle(boidVisionCone);
            tempBoid.setSpeed(boidSpeed);
            tempBoid.setAvoidInfluence(boidAvoidInfluence);
            tempBoid.setAlignInfluence(boidAlignInfluence);
            tempBoid.setCorneringSpeed(boidCorneringSpeed);
            tempBoid.transform.position = new Vector3(
                Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            boids.Add(tempBoid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (BoidComponent boid in boids)
        {
            boid.Move();
        }

        if (boidSpeed != setSpeed || setVisionConeAngle != boidVisionCone || boidCenterInfluence != setCenterInfluence  || 
            setAlignInfluence != boidAlignInfluence || setAvoidInfluence != boidAvoidInfluence|| boidCorneringSpeed != setCornering)
        {
            foreach (BoidComponent boid in boids)
            {
                boid.setSpeed(boidSpeed);
                boid.setVisionConeAngle(boidVisionCone);
                boid.setCorneringSpeed(boidCorneringSpeed);
                boid.setCenterInfluence(boidCenterInfluence);
                boid.setCorneringSpeed(boidCorneringSpeed);
                boid.setAvoidInfluence(boidAvoidInfluence);
                boid.setAlignInfluence(boidAlignInfluence);
            }

            setSpeed = boidSpeed;
            setCenterInfluence = boidCenterInfluence;
            setCornering = boidCorneringSpeed;
            setAvoidInfluence = boidAvoidInfluence;
            setAlignInfluence = boidAlignInfluence;
        }
    }

    private void FixedUpdate()
    {
        
    }
}