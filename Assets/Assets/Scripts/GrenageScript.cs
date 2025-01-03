using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float throwForce = 10f;
    public float explosionRadius = 5f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;
    public float angle = 45f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 throwDirection = Quaternion.Euler(angle, 0, 0) * transform.forward;
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        GameObject explosionInstance = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        bool CheckDamage = false;
    foreach (Collider hit in hitColliders)
    {
        Health targetHealth = hit.GetComponent<Health>();
        if (targetHealth != null && !CheckDamage)
        {
            targetHealth.TakeDamage(explosionDamage);
            CheckDamage = true;
        }
    }
        Destroy(explosionInstance, 2f);
        Destroy(gameObject);
    }
}
