using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{

    [SerializeField]
    private GameObject ExploadObj;
    private float speed = 10.0f;
    private bool Bounce = false;    //これを追加
    private Rigidbody rBall;

    void Start()
    {
        //以下を追加
        rBall = this.GetComponent<Rigidbody>();
        rBall.AddForce((transform.forward + transform.right) * speed,
            ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        Vector3 v = rBall.velocity;
        float rot = Mathf.Atan2(v.x, v.z) * 180 / Mathf.PI;

		//角度が垂直すぎ・水平だった時の調整
        if (rot >= 0){
            if (rot < 15.0f || rot > 165.0f){
                v.x *= 3.0f;
            }
        }else if(rot < 0){
            if(rot > -15.0f || rot < -165.0f){
                v.x *= 3.0f;
            }
        }
        if(85 < Mathf.Abs(rot) && Mathf.Abs(rot) <= 90){
            v.z = 2.0f;
        }else if(90 < Mathf.Abs(rot) && Mathf.Abs(rot) < 95){
            v.z = -2.0f;
        }

        // もし速度が遅すぎる場合
        if (rBall.velocity.magnitude < 2*speed) {
            Debug.Log(rBall.velocity.magnitude);
            //速度を初期値に戻す
            rBall.velocity = rBall.velocity.normalized * 2*speed;
        }
        else if(rBall.velocity.magnitude > 2.5f *speed){
            rBall.velocity = rBall.velocity.normalized * 2.5f *speed;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Rock_4(Clone)")
        {
            for (int i = 0; i < 3; i++)
            {
                if (other.transform.position.x == GimmickController.Instance.savePos[i].fx)
                {
                    if (other.transform.position.z == GimmickController.Instance.savePos[i].fz)
                    {
                        //rBall.AddForce(0 * this.transform.position, ForceMode.VelocityChange);
                        Destroy(GimmickController.Instance.savePos[i].CinderObj);
                        GameObject exp = Instantiate(ExploadObj, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity);
                        Destroy(exp, 3f);
                        GimmickController.Instance.savePos[i].fx = 0;
                        GimmickController.Instance.savePos[i].fy = 0;
                        GimmickController.Instance.savePos[i].fz = 0;
                        GimmickController.Instance.savePos[i].bUse = false;
                        GimmickController.Instance.savePos[i].CinderObj = null;
                        break;
                    }
                }
            }
        }
    }

    void OnColliderExit(Collider other) {
			if(other.gameObject.name == "Racket1" || other.gameObject.name == "Racket2")
			{
				Vector3 v = GetComponent<Rigidbody>().velocity;
			}
    }
}
