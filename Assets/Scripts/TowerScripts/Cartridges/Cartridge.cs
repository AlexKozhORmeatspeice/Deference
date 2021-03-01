using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

public class Cartridge : MonoBehaviour
{
    private Vector3[] beziePoints;

    //private float speed = 3.0f;

    private float _startTime;

    public GameObject target;
    private float _speedOfAtack;

    public float SpeedOfAtack
    {
        protected get => _speedOfAtack;
        set => _speedOfAtack = value;
    }

    private float _damage; 
    public float Damage
    {
        protected get => _damage;
        set => _damage = value;
    }

    private float radiusOfBOMB = 5.0f;

    public float RadiusOfBomb
    {
        get => radiusOfBOMB;
        set
        {
            radiusOfBOMB = value;
        }
    }

    private void Start()
    {
        beziePoints = new Vector3[3]{transform.position, 
                                    (target.transform.position/2 + transform.position/2 ) + (Vector3.up) * 10.0f , 
                                    target.transform.position };
        _startTime = Time.time;
    }

    private void Update()
    {
        Attack();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            Collider[] colliders =  Physics.OverlapSphere(this.transform.position, radiusOfBOMB);
            
            foreach (Collider collider in colliders)
            {
                Creep un = collider.GetComponent<Creep>();
                if (un)
                {
                    un.Hit(_damage);
                }
            }
            Destroy(this.gameObject);
        }
    }

    protected virtual void Attack()
    {
        float u = (Time.time - _startTime);

        Vector3 p01, p12;
        
        p01 = (1 - u) * beziePoints[0] + u * beziePoints[1];
        p12 = (1 - u) * beziePoints[1] + u * beziePoints[2];


        transform.position =  (1 - u) * p01 + u * p12;
    }
}
