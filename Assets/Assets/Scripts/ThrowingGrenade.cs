using UnityEngine;
public class PlayerThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public int grenadeCount = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && grenadeCount > 0)
        {
            ThrowGrenade();
            grenadeCount -= 1;
        }
    }

    private void ThrowGrenade()
    {
        Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
    }
}
