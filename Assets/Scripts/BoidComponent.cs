using System.Collections.Generic;
using UnityEngine;

public class BoidComponent : MonoBehaviour
{
    private float centerInfluence;
    private float avoidInfluence;
    private float alignInfluence;
    private float speed;
    private float corneringSpeed; 
    private Vector3 closestCenter;
    [SerializeField] public float visionConeAngle;
    [SerializeField] public float movementConeAngle;
    List <Collider> collisions;

    
    void Awake()
    {
        collisions = new List<Collider>();
    }

    public void setCenterInfluence(float centerInfluence)
    {
        this.centerInfluence = centerInfluence; 
    }

    public void setAvoidInfluence(float avoidInfluence)
    {
        this.avoidInfluence = avoidInfluence;
    }

    public void setAlignInfluence(float alignInfluence)
    {
        this.alignInfluence = alignInfluence;
    }

    public void setSpeed(float boidSpeed)
    {
        speed = boidSpeed;
    }

    public void setVisionConeAngle(float visionConeAngle)
    {
        this.visionConeAngle = visionConeAngle;
    }

    public void setCorneringSpeed(float corneringSpeed)
    {
        this.corneringSpeed = corneringSpeed;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!collisions.Contains(collision))
        {
            collisions.Add(collision);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        collisions.Remove(collision);
    }

    public void Move()
    {
        Vector3 targetDir = -(calcCloseBoidInteractionsV3());

        //Debug.Log(targetDir);
        if(!targetDir.Equals(transform.forward))
        {
            targetDir += calcCenterInfluenceV3(closestCenter) * centerInfluence; 
        }
        else
        {
            targetDir = calcCenterInfluenceV3(closestCenter) * centerInfluence;
        }

        Debug.DrawLine(transform.position, transform.position + targetDir, Color.magenta);

        Quaternion directionRotation = Quaternion.LookRotation(-targetDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, directionRotation, corneringSpeed * Time.deltaTime);
        
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private Vector3 calcCloseBoidInteractionsV3()
    {
        Vector3 desiredDirectionToLook = Vector3.zero;

        float centerX = 0;
        float centerY = 0; 
        float centerZ = 0;
        int boidCounter = 0;

        foreach (Collider collision in collisions)
        {
            GameObject obstacle = collision.gameObject;
            if(obstacle.tag == "boid")
            {
                centerX += obstacle.transform.position.x;
                centerY += obstacle.transform.position.y;
                centerZ += obstacle.transform.position.z;
                boidCounter++;
            }

            //Debug.Log("DIST: " + Vector3.Distance(transform.position, collision.transform.position));
            float dist = Vector3.Distance(transform.position, collision.transform.position);
            if (dist > 0)
            {
                //Find angle between objects 
                Vector3 targetDir = obstacle.transform.position - transform.position;

                float angleBetweenObjects = Vector3.Angle(targetDir, transform.forward);
                

                if (angleBetweenObjects < visionConeAngle)
                {
                    //align
                    if(obstacle.layer == 0 )
                    {
                        //Debug.Log("HERE");
                        desiredDirectionToLook += (obstacle.transform.forward * alignInfluence);
                        Debug.DrawLine(transform.position, transform.position + obstacle.transform.forward, Color.green);
                    }

                    //Find angle between forward vectors 
                    /*Vector3 contact = obstacle.transform.forward;
                    float angleBetweenForwards = Vector3.Dot(transform.forward, contact);

                    if (angleBetweenForwards != 0)
                    {
                        angleBetweenForwards = Mathf.Rad2Deg * Mathf.Acos(angleBetweenObjects / (transform.forward.magnitude * contact.magnitude));
                    }*/

                    if (collision.gameObject.layer == 0 && angleBetweenObjects < 180 - movementConeAngle)
                    {
                        //Debug.Log(angleBetweenObjects);
                        //avoid
                        /*
                        Vector3 avoid = new Vector3();
                        avoid.x = targetDir.x * (1 / dist);
                        avoid.y = targetDir.y * (1 / dist);
                        avoid.z = targetDir.z * (1 / dist);
                        */

                        //Debug.DrawLine(transform.position, -targetDir);
                        

                        Vector3 avoid = (-targetDir.normalized * 5) / (dist);

                        desiredDirectionToLook += (avoid * Mathf.Pow(avoidInfluence, 3));
                        Debug.DrawLine(transform.position, transform.position + (avoid * Mathf.Pow(avoidInfluence, 3)), Color.blue);
                        //Debug.Log(avoid.magnitude);
                    }
                }
            }
            
        }

        if(boidCounter > 0)
        {
            centerX /= boidCounter;
            centerY /= boidCounter;
            centerZ /= boidCounter;

            closestCenter = new Vector3(centerX, centerY, centerZ);
        }

        if(desiredDirectionToLook.Equals(Vector3.zero))
        {
            return transform.forward;
        }
        return desiredDirectionToLook.normalized;
    }

   

    private Vector3 calcCenterInfluenceV3(Vector3 center)
    {
        //Debug.DrawLine(transform.position, center);
        return (transform.position - center).normalized;
    }
}
