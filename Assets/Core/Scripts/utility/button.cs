using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "button", menuName = "Scriptable Objects/button")]
public class button : ScriptableObject
{
    public GameObject battleState = GameObject.FindGameObjectWithTag("battleState");
    
    
}
