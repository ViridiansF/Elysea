using UnityEngine;

public class Vent : MonoBehaviour
{

    public Vector2 windDirection = Vector2.right;
    public float windStrength = 2f;
    public Vector2 WindForce => windDirection.normalized * windStrength;

}
