using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float currentHorizontalPosition;
    [SerializeField] private float currentVerticalPosition;

    [SerializeField] private float delay = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        CalculatetHorizontalPosition();
        CalculateVerticalposition();
        SetCameraPosition();
    }

    private void CalculatetHorizontalPosition()
    {
       currentHorizontalPosition = player.transform.position.x;
    }

    private void CalculateVerticalposition()
    {
        //currentVerticalPosition = Mathf.Lerp(currentVerticalPosition, player.transform.position.y, delay * Time.deltaTime);
        currentVerticalPosition = player.transform.position.y;
    }

    private void SetCameraPosition()
    {
        transform.position = new Vector3(currentHorizontalPosition, currentVerticalPosition, transform.position.z);
    }
}
