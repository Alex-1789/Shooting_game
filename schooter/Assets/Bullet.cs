using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(this.gameObject, audioSource.clip.length);
        }
        else {
            this.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this.gameObject);
        }
    }
}
