using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class CharacterSO : ScriptableObject
{
    public string nameCharacter = "Character";
    //Health
    public int hpMax = 2;
    public bool isImmortal;
    //Move
    public float speed = 5f;
    public float jumpPower = 10f;
    public float rollPower = 15f;
    //Dame
    public float dame = 1;
}
