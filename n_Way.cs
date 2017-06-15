using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_Way : MonoBehaviour {

	public GameObject EnemyMissilePrefab;
	private GameObject parents;

	//連射の弾数、同時弾の最初〜最後
	[SerializeField]
	public int ChainNum=1,FirstBulletNum=-1,LastBulletNum=1;
	//同時弾の角度
	[Range(0,90)]
	public int Degree=0;
	//弾のスピード
	[Range(0,10)]
	public float Speed=5.0f;
	//連射の復帰時間(s)、連射間隔(s)、連射に入るまでの待ち時間(s)
	[SerializeField]
	public float ChainRestoreTime=0.1f,RestoreTime=0.5f,InitializationTime=1.0f;		

	void Start () {
		StartCoroutine ("Shot");
	}
	
	//Sceneに発射角度を表示
   void OnDrawGizmos()
    {
		parents=transform.parent.gameObject;
        Gizmos.color = Color.red;
		for(int i=FirstBulletNum;i<=LastBulletNum;i++){
			//enemyとの差をとる
			Vector3 sub=(transform.position-parents.transform.position);
			//ベクトルに回転をかけて向きを変える
			sub = Quaternion.Euler (0.0f, 0.0f,Degree*i)*sub;
			Vector3 p=parents.transform.position;
			//enemyから発射ベクトルに線を引く
	        Gizmos.DrawLine(p+sub, parents.transform.position);
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
					//enemyとの差をとり、単位ベクトル化する					
					Vector2 direction=(transform.position-parents.transform.position).normalized;
					//ベクトルに回転をかけて向きを変える
					direction = Quaternion.Euler (0.0f, 0.0f,Degree*j)*direction;
					em.SetMissile(parents.transform.position,direction,Speed);
				}
					yield return new WaitForSeconds (ChainRestoreTime);				
			}
			yield return new WaitForSeconds (RestoreTime);
		}
	}
}
