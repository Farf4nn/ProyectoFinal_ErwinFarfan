using UnityEngine;


public enum AnimalType { Ardilla, Conejo, Castor};

[CreateAssetMenu(fileName = "CreatureDataSO", menuName = "Scriptable Objects/CreatureDataSO")]
public class CreatureDataSO : ScriptableObject
{
    [Header("Identidad")]
    public AnimalType type;
    public float creatureSpeed;

    [Header("Media")]
    public Sprite icon;
    public AudioClip sound;
}   
