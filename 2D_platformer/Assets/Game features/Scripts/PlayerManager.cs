using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{ 
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private CinemachineVirtualCamera vCam;
    int characterIndex;

    // Start is called before the first frame update
    void Start()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Vector3 startpos = GameObject.FindGameObjectWithTag("StartPos").transform.position;
        GameObject player = Instantiate(playerPrefabs[characterIndex], startpos, Quaternion.identity);
        vCam.m_Follow = player.transform;
        vCam.m_LookAt = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
