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
    public AudioClip[] customerLaughSounds;
    
    private float minSpawnInterval = 5f;
    private float maxSpawnInterval = 10f;
    private float lastSpawnTime;
    private LevelProgressionManager levelProgressionManager;
    
    
    private void Start()
    {
        lastSpawnTime = Time.time;
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
        AudioManager.Instance.PlayClip(customerSpawnSound[Random.Range(0, customerSpawnSound.Length)], AudioType.SFX, customerSpawnPoint.position, 1f, true, 0.2f);
    }

    public void Haha()
    {
        StartCoroutine(PlayLaughs());
    }

    private IEnumerator PlayLaughs()
    {
        int randomCustomer = Random.Range(0, Mathf.Min(6, customers.Count));
        for (int i = 0; i < randomCustomer; i++)
        {
            yield return new WaitForSeconds(0.05f);
            if (customerLaughSounds.Length > 0) AudioManager.Instance.PlayClip(customerLaughSounds[Random.Range(0, customerLaughSounds.Length)], AudioType.SFX, customers[i].transform.position, 1f, true, 0.2f);
        }
    }

    public void CustomerExit(Customer customer)
    {
        customers.Remove(customer);
        Destroy(customer.gameObject);
    }
}
