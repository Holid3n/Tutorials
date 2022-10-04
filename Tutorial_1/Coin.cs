using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private MeshRenderer mr;
    private Collider cl;
    private void Awake()
    {
        cl = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
    }
    public void DestroyObject()
    {
        cl.enabled = false;
        mr.enabled = false;
        particle.Play();
        Destroy(gameObject, 1);
    }
}
