﻿using System;
using UnityEngine;

public class GameEdge : MonoBehaviour {
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("KOLIZJA");
        if (other.gameObject.CompareTag("Player"))
        {
            throw new NotImplementedException();
        }
        else
        {
            Debug.Log("destroying");
            Destroy(other.gameObject);
        }
        
    }
}
