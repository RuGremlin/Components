using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class FlyingEnemyAI : MonoBehaviour {

	public Transform target;
	public float updateRate = 2f;
	public Path path;
	public float speed = 300f;
	public ForceMode2D forceMode;
	public float nextWaypointDistance = 3;

	[HideInInspector]
	public bool pathIsEnded = false;

	private Seeker seeker;
	private Rigidbody2D rb;
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			Debug.LogError("No target is found.");
			return;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);
		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath() {
		if (target == null) {
			return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds(1f/updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		} else {
			Debug.Log ("Path error: " + p.error);
		}
	}

	void FixedUpdate () {
		if (target == null) {
			return;			
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}

			Debug.Log("End of path reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		Vector3 direction = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		direction *= speed * Time.fixedDeltaTime;

		rb.AddForce (direction, forceMode);

		float distance = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
		if (distance < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}	
}
