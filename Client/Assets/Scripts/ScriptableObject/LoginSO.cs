using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Login Data", menuName = "Scriptable Object/Login Data", order = int.MaxValue)]
public class LoginSO : ScriptableObject
{
    [SerializeField] int playerId;
    public int PlayerId { get { return playerId; } set { playerId = value; } }

    [SerializeField] string username;
    public string UserName { get { return username; } set { username = value; } }
}