using UnityEngine;
using System.Collections;

public class destroyByBoundary : MonoBehaviour {
    void OnTriggerExit(Collider other) {
        Destroy(other.gameObject);
    }
}
