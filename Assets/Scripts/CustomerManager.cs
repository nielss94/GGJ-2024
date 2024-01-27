using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public event Action<Customer> OnCustomerSpawned = delegate {  };
    
    public List<Transform> travelPoints = new List<Transform>();
    public Transform customerSpawnPoint;
    public List<Customer> customers = new List<Customer>();
    public Customer customerPrefab;
    public AudioClip[] customerSpawnSound;
    
    private float minSpawnInterval = 5f;
    private float maxSpawnInterval = 10f;
    private float lastSpawnTime;
    private LevelProgressionManager levelProgressionManager;
    private AudioSource audioSource;
    
    private void Start()
    {
        lastSpawnTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(float minSpawnInterval, float maxSpawnInterval, LevelProgressionManager levelProgressionManager)
    {
        this.minSpawnInterval = minSpawnInterval;
        this.maxSpawnInterval = maxSpawnInterval;
        this.levelProgressionManager = levelProgressionManager;
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
        customer.gameObject.GetComponent<CustomerOrder>().OnOrderIncorrect += levelProgressionManager.OnCustomerMiss;
        customer.gameObject.GetComponent<CustomerOrder>().OnOrderCorrect += levelProgressionManager.OnCustomerHit;
        customers.Add(customer);
        
        OnCustomerSpawned(customer);
        audioSource.clip = customerSpawnSound[Random.Range(0, customerSpawnSound.Length)];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
    
}
