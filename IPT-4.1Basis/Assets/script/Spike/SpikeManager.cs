using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    public Vector3[] Spikes;
    public GameObject Spike_gameobject;

    private void Awake()
    {
        for(int i = 0; i < Spikes.Length; i++)
        {
            Instantiate(Spike_gameobject, Spikes[i], Quaternion.identity);
        }
    }
}
