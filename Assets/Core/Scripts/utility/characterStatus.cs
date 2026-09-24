using UnityEngine;

[CreateAssetMenu(fileName = "characterStatus", menuName = "Scriptable Objects/characterStatus")]
public class playerStatus : ScriptableObject
{
    public GameObject characterGameObject;
    public int level = 1;
    public float maxHealth = 100;
    public float maxEnergy = 10;
    public float health = 100;
    public float energy = 10;
}
