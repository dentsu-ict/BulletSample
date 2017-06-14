using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Way : MonoBehaviour {

	public GameObject EnemyMissilePrefab;
	private GameObject parents;

	[SerializeField]
	public int ChainNum=1,FirstBulletNum=-1,LastBulletNum=1;
	[Range(0,90)]
	public int Degree=0;
	[Range(0,10)]
	public float Speed=5.0f;	
	[SerializeField]
	public float ChainRestoreTime=0.1f,RestoreTime=0.5f,InitializationTime=1.0f;		


	// Use this for initialization
	void Start () {
		StartCoroutine ("Shot");
	}
	
   void OnDrawGizmos()
    {
		parents=transform.parent.gameObject;
        Gizmos.color = Color.red;
		for(int i=FirstBulletNum;i<=LastBulletNum;i++){
			Vector2 sub2=(transform.position-parents.transform.position);
			Vector2 sub;
			sub.x=sub2.x*Mathf.Cos(Mathf.Deg2Rad*Degree*i)-sub2.y*Mathf.Sin(Mathf.Deg2Rad*Degree*i);
			sub.y=sub2.x*Mathf.Sin(Mathf.Deg2Rad*Degree*i)+sub2.y*Mathf.Cos(Mathf.Deg2Rad*Degree*i);
			Vector3 p=parents.transform.position;
	        Gizmos.DrawLine(new Vector3(p.x+sub.x,p.y+sub.y,p.z), parents.transform.position);
		}
			Gizmos.DrawSphere(transform.position, 0.1f);
    }

	private IEnumerator Shot() {
		yield return new WaitForSeconds (InitializationTime);
        while (true){
			for(int i=0;i<ChainNum;i++){
				for(int j=FirstBulletNum;j<=LastBulletNum;j++){
        	        GameObject g=Instantiate (EnemyMissilePrefab, transform.position, Quaternion.identity) as GameObject;
					EnemyMissile em=g.GetComponent<EnemyMissile>();
					parents=transform.parent.gameObject;
					Vector2 direction2=(transform.position-parents.transform.position).normalized;
					Vector2 direction;
					direction.x=direction2.x*Mathf.Cos(Mathf.Deg2Rad*Degree*j)-direction2.y*Mathf.Sin(Mathf.Deg2Rad*Degree*j);
					direction.y=direction2.x*Mathf.Sin(Mathf.Deg2Rad*Degree*j)+direction2.y*Mathf.Cos(Mathf.Deg2Rad*Degree*j);
					em.SetMissile(parents.transform.position,direction,Speed);
				}
					yield return new WaitForSeconds (ChainRestoreTime);				
			}
			yield return new WaitForSeconds (RestoreTime);
		}
	}
}
