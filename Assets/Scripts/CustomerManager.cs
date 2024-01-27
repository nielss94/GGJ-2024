using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Transform> travelPoints = new List<Transform>();
    
    public Transform customerSpawnPoint;
    
    public List<Customer> customers = new List<Customer>();
    
    public Customer customerPrefab;
    
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;
    private float lastSpawnTime;
    
    private void Start()
    {
        lastSpawnTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time - lastSpawnTime > Random.Range(minSpawnInterval, maxSpawnInterval))
        {
            SpawnCustomer();
            lastSpawnTime = Time.time;
        }
    }
    
    public void SpawnCustomer()
    {
        Customer customer = Instantiate(customerPrefab, customerSpawnPoint.position, customerSpawnPoint.rotation);
        customer.Init(travelPoints);
        customers.Add(customer);
    }
    
}
