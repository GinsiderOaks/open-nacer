using UnityEngine;

public class Cannon : MonoBehaviour {

    public Shot shotPrefab;
	
	public void Shoot (SpaceCraft craft) {
        Shot shotInstance = Instantiate<Shot> (shotPrefab, transform.position, transform.rotation);
        shotInstance.owner = craft;
    }
}
