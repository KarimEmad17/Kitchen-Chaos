using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransportaionHelper : MonoBehaviour
{
    [SerializeField] private GameObject Player; // player 
    [SerializeField] private GameObject Villa;  // create gameobjet on evrey place needed
    [SerializeField] private GameObject River;  // create gameobjet on evrey place needed
    [SerializeField] private GameObject Beach;  // create gameobjet on evrey place needed
    [SerializeField] private Button buttonVilla;
    [SerializeField] private Button buttonRiver;
    [SerializeField] private Button buttonBeach;
    // Start is called before the first frame update
    void Start()
    {
        buttonBeach.onClick.AddListener(() =>
        {
            Player.gameObject.transform.position = Beach.transform.position;
        });
        buttonRiver.onClick.AddListener(() =>
        {
            Player.gameObject.transform.position = River.transform.position;
        });
        buttonVilla.onClick.AddListener(() =>
        {
            Player.gameObject.transform.position = Villa.transform.position;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
