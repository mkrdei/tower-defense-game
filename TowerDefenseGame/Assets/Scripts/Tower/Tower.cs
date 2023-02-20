using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Profiles;
public class Tower : MonoBehaviour
{
    public TowerProfile towerProfile;
    [SerializeField]
    private GameObject projectilePrefab;
    private float cooldownTimestamp;
    [field: SerializeField]
    public float damage;
    [field: SerializeField]
    public float cooldown;
    [field: SerializeField]
    public float range;
    [field: SerializeField]
    public float projectileSpeed;
    private TargetDetector targetDetector;
    private Transform target;
    private Transform launcher;
    private SphereCollider rangeCollider;

    // Start is called before the first frame update
    void Awake()
    {
        targetDetector = GetComponentInChildren<TargetDetector>();
        launcher = transform.Find("Launcher");
        rangeCollider = transform.Find("Range").GetComponent<SphereCollider>();
        rangeCollider.radius = range;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        target = targetDetector.target;
        if (target != null)
        {
            if (Time.time >= cooldownTimestamp)
            {
                cooldownTimestamp = Time.time + cooldown;
                Debug.Log(transform.name + "fired to " + target.name);
                GameObject projectile = Instantiate(projectilePrefab, launcher.position, Quaternion.identity, transform);
                projectile.transform.rotation = Quaternion.LookRotation((target.transform.position - projectile.transform.position));
                projectile.GetComponent<Rigidbody>().AddForce((target.position - projectile.transform.position).normalized * projectileSpeed,ForceMode.VelocityChange);
                Destroy(projectile, 2);
            }
        }
    }
    
}
