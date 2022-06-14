using UnityEngine;

public class DestroyedScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => other.gameObject.SetActive(false);
}
