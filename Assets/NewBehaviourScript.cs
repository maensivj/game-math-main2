using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject concreate;
    public GameObject trolley;
    public GameObject crane;
    public GameObject cable;
    public GameObject hook;
    public GameObject farLimit;
    public GameObject nearLimit;
    private float rotationSpeed = 1f;
    public int targetFPS = 60;
    private float cableDistanceFromCraneOrigin;
    private float farLimitDistanceFromCraneOrigin;
    private float nearLimitDistanceFromCraneOrigin;
    private float trolleyDistanceFromCraneOrigin;
    private float hookDistanceFromCraneOrigin;
    private float concreateAngle;
    private float angle = 180f;
    private float concreateMagnitude;
    private float trolleyMagnitude;
    private float farLimitMagnitude;
    private float nearLimitMagnitude;
    private float xUnit;
    private float zUnit;
    



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFPS;
        ConcreateAngle();
        calculateUnitVectors();
        cableDistanceFromCraneOrigin = Mathf.Sqrt((cable.transform.position.x * cable.transform.position.x) + (cable.transform.position.z * cable.transform.position.z));
        trolleyDistanceFromCraneOrigin = Mathf.Sqrt((trolley.transform.position.x * trolley.transform.position.x) + (trolley.transform.position.z * trolley.transform.position.z));
        hookDistanceFromCraneOrigin = Mathf.Sqrt((hook.transform.position.x * hook.transform.position.x) + (hook.transform.position.z * hook.transform.position.z));
        farLimitDistanceFromCraneOrigin = Mathf.Sqrt((farLimit.transform.position.x * farLimit.transform.position.x) + (farLimit.transform.position.z * farLimit.transform.position.z));
        nearLimitDistanceFromCraneOrigin = Mathf.Sqrt((nearLimit.transform.position.x * nearLimit.transform.position.x) + (nearLimit.transform.position.z * nearLimit.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (angle != concreateAngle)
        {
            AngleCalculator();
            //Debug.Log(trolley.transform.position);
            //Debug.Log(concreateAngle);
            //Object rotations
            trolley.transform.Rotate(Vector3.up * rotationSpeed);
            crane.transform.Rotate(Vector3.up * rotationSpeed);
            cable.transform.Rotate(Vector3.up * rotationSpeed);
            hook.transform.Rotate(Vector3.up * rotationSpeed);
            //Object position 
            trolley.transform.position = new Vector3(trolleyDistanceFromCraneOrigin * Mathf.Cos(angle * Mathf.Deg2Rad), trolley.transform.position.y, trolleyDistanceFromCraneOrigin * Mathf.Sin(angle * Mathf.Deg2Rad));
            cable.transform.position = new Vector3(cableDistanceFromCraneOrigin * Mathf.Cos(angle * Mathf.Deg2Rad), cable.transform.position.y, cableDistanceFromCraneOrigin * Mathf.Sin(angle * Mathf.Deg2Rad));
            hook.transform.position = new Vector3(hookDistanceFromCraneOrigin * Mathf.Cos(angle * Mathf.Deg2Rad), hook.transform.position.y, hookDistanceFromCraneOrigin * Mathf.Sin(angle * Mathf.Deg2Rad));
            nearLimit.transform.position = new Vector3(nearLimitDistanceFromCraneOrigin * Mathf.Cos(angle * Mathf.Deg2Rad), nearLimit.transform.position.y, nearLimitDistanceFromCraneOrigin * Mathf.Sin(angle * Mathf.Deg2Rad));
            farLimit.transform.position = new Vector3(farLimitDistanceFromCraneOrigin * Mathf.Cos(angle * Mathf.Deg2Rad), farLimit.transform.position.y, farLimitDistanceFromCraneOrigin * Mathf.Sin(angle * Mathf.Deg2Rad));
        }
        Debug.Log("concreate " + concreateMagnitude);
        Debug.Log("trolley " + trolleyMagnitude);
        Debug.Log("angle " + angle);
        Debug.Log("conangle " + concreateAngle);
        if (angle == concreateAngle)
        {
            
            if (Mathf.Round(trolley.transform.position.x) != Mathf.Round(concreate.transform.position.x) && Mathf.Round(trolley.transform.position.z) != Mathf.Round(concreate.transform.position.z))
            {
                
                MoveTrolley();
            }
        }
        






    }
    void ConcreateAngle()
    {
        concreateAngle = Mathf.Round(Mathf.Atan(concreate.transform.position.z / concreate.transform.position.x) * Mathf.Rad2Deg);
        Debug.Log(concreateAngle);
        if (concreate.transform.position.x < 0 && concreate.transform.position.z > 0)
        {
            concreateAngle = 180 + concreateAngle;
        }
        else if (concreateAngle == -90f && concreate.transform.position.z > 0)
        {
            concreateAngle = 90f;
        }
        else if (concreateAngle == 90f && concreate.transform.position.z > 0)
        {
            concreateAngle = 90f;
        }
        else if (concreateAngle == 90f && concreate.transform.position.z < 0)
        {
            concreateAngle = -90f;
        }
        else if (concreateAngle == -90f && concreate.transform.position.z < 0)
        {
            concreateAngle = -90f;
        }
        else if (concreateAngle == 0f && concreate.transform.position.x < 0)
        {
            concreateAngle = 180f;
        }
        else if (concreateAngle == 0f && concreate.transform.position.x > 0)
        {
            concreateAngle = -180f;
        }
        else if (concreate.transform.position.x > 0 && concreate.transform.position.z < 0)
        {
            concreateAngle = (-1) * concreateAngle;
        }
        else if (concreate.transform.position.x < 0 && concreate.transform.position.z < 0)
        {
            concreateAngle = -180 + concreateAngle;
        }
    }

    void AngleCalculator()
    {
        
        if (angle <= -180f)
        {
            
            angle += 360f;
        }
        else
        {
            angle = angle - rotationSpeed;
        }
        
    }

    void calculateUnitVectors()
    {
        concreateMagnitude = Mathf.Sqrt(concreate.transform.position.x * concreate.transform.position.x + concreate.transform.position.z * concreate.transform.position.z);
        trolleyMagnitude = Mathf.Sqrt(trolley.transform.position.x * trolley.transform.position.x + trolley.transform.position.z * trolley.transform.position.z);
        farLimitMagnitude = Mathf.Sqrt(farLimit.transform.position.x * farLimit.transform.position.x + farLimit.transform.position.z * farLimit.transform.position.z);
        nearLimitMagnitude = Mathf.Sqrt(nearLimit.transform.position.x * nearLimit.transform.position.x + nearLimit.transform.position.z * nearLimit.transform.position.z);
        zUnit = Mathf.Round(Mathf.Abs(concreate.transform.position.z / concreateMagnitude)*10) / 10;
        xUnit = Mathf.Round(Mathf.Abs(concreate.transform.position.x / concreateMagnitude)*10) / 10;
    }
    void MoveTrolley()
    {
        Debug.Log("1st  part is working");
        if (trolleyMagnitude < concreateMagnitude)
        {
            if (nearLimitMagnitude<trolleyMagnitude && farLimitMagnitude>trolleyMagnitude)
            {
                
                if (180f >= angle && angle > 90f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x - xUnit, trolley.transform.position.y, trolley.transform.position.z + zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x - xUnit, hook.transform.position.y, hook.transform.position.z + zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x - xUnit, cable.transform.position.y, cable.transform.position.z + zUnit);
                }
                if (90f >= angle && angle > 0f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x + xUnit, trolley.transform.position.y, trolley.transform.position.z + zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x + xUnit, hook.transform.position.y, hook.transform.position.z + zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x + xUnit, cable.transform.position.y, cable.transform.position.z + zUnit);
                }
                if (0f >= angle && angle > -90f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x + xUnit, trolley.transform.position.y, trolley.transform.position.z - zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x + xUnit, hook.transform.position.y, hook.transform.position.z - zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x + xUnit, cable.transform.position.y, cable.transform.position.z - zUnit);
                }
                if (-90f >= angle && angle >= -180f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x - xUnit, trolley.transform.position.y, trolley.transform.position.z - zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x - xUnit, hook.transform.position.y, hook.transform.position.z - zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x - xUnit, cable.transform.position.y, cable.transform.position.z - zUnit);
                }
            }
            
        }
        else if (trolleyMagnitude > concreateMagnitude)
        {
            if (nearLimitMagnitude < trolleyMagnitude && farLimitMagnitude > trolleyMagnitude)
            {
                if (180f >= angle && angle > 90f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x + xUnit, trolley.transform.position.y, trolley.transform.position.z - zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x + xUnit, hook.transform.position.y, hook.transform.position.z - zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x + xUnit, cable.transform.position.y, cable.transform.position.z - zUnit);
                }
                if (90f >= angle && angle > 0f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x - xUnit, trolley.transform.position.y, trolley.transform.position.z - zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x - xUnit, hook.transform.position.y, hook.transform.position.z - zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x - xUnit, cable.transform.position.y, cable.transform.position.z - zUnit);
                }
                if (0f >= angle && angle > -90f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x - xUnit, trolley.transform.position.y, trolley.transform.position.z + zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x - xUnit, hook.transform.position.y, hook.transform.position.z + zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x - xUnit, cable.transform.position.y, cable.transform.position.z + zUnit);
                }
                if (-90f >= angle && angle >= -180f)
                {
                    trolley.transform.position = new Vector3(trolley.transform.position.x + xUnit, trolley.transform.position.y, trolley.transform.position.z + zUnit);
                    hook.transform.position = new Vector3(hook.transform.position.x + xUnit, hook.transform.position.y, hook.transform.position.z + zUnit);
                    cable.transform.position = new Vector3(cable.transform.position.x + xUnit, cable.transform.position.y, cable.transform.position.z + zUnit);
                }
            }
        }



    }
}   
