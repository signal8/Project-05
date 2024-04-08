//THANK YOU STACK OVERFLOW!!!

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
     
    public class PersistentManagerScript : MonoBehaviour
     
    {
        public static PersistentManagerScript Instance { get; private set; }
     
        public GameObject SaveUIGame;
     
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

