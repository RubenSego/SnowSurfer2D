using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptableObject", menuName = "PowerUps/PowerUp Data", order = 1)]
public class PowerUpScriptableObject : ScriptableObject
{
    [SerializeField] private string powerUpType; // Speed, health, damage, etc.
    [SerializeField] private float powerUpValue; // Increase amount.
    [SerializeField] private float time; // How long the power up lasts.

    public string PowerUpType { get => powerUpType; set => powerUpType = value;}
    public float PowerUpValue { get => powerUpValue; set => powerUpValue = value;}
    public float TimeLimit { get => time; set => time = value;}
}
