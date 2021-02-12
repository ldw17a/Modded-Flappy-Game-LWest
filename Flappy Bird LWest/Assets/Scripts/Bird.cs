using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
    public float upForce;
	public int oofs = 5;
    private bool isDead = false;
    private Animator anim;
    private Rigidbody2D rb2d;                
	
    void Start()
    {
        anim = GetComponent<Animator> ();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead == false) 
        {
            if (Input.GetButtonDown("FLAP")) 
            {
                anim.SetTrigger("Flap");
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.tag == "Column")
		{
			oofs--;
			anim.SetTrigger ("Oof");
			if (oofs <= 0)
			{
				rb2d.velocity = Vector2.zero;
				isDead = true;
				anim.SetTrigger ("Die");
				GameControl.instance.BirdDied ();
			}
			else
			{
				GetComponent<PolygonCollider2D> ().enabled = false;
				StartCoroutine(EnableBox(1.0F));
			}
		}
		else
		{
			rb2d.velocity = Vector2.zero;
			isDead = true;
			anim.SetTrigger ("Die");
			GameControl.instance.BirdDied ();
		}
    }
	
	IEnumerator EnableBox(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		GetComponent<PolygonCollider2D> ().enabled = true;
	}
}
