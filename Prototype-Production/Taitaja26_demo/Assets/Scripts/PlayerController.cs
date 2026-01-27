using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	public float speed = 5;
	private Vector2 movement;
	

	[SerializeField] private Transform _pipePoint;
	[SerializeField] private float _weaponRange;
   	[SerializeField] private GameObject _bulletTrail;
	[SerializeField] private Animator _muzzleFlashAnimator;
	void Start()
    	{
		rb = GetComponent<Rigidbody2D>();
        
    	}

	void Update() {
		LookAtMouse();
		Move();
		Shoot();
	}

	private void Shoot()
	{

		if (Input.GetMouseButtonDown(0))
		{
			//_muzzleFlashAnimator.SetTrigger("Shoot");

			var hit = Physics2D.Raycast(_pipePoint.position, transform.up, _weaponRange);

			var trail = Instantiate(_bulletTrail, _pipePoint.position, transform.rotation);

			var trailScript = trail.GetComponent<BulletTrail>();

			if (hit.collider != null)
			{
				trailScript.SetTargetPosition(hit.point);
				var hittable = hit.collider.GetComponent<IHittable>();
				if (hittable != null)
                {
					hittable.ReceiveHit(hit);
                }
			}
			else
			{
				var endPosition = _pipePoint.position + transform.up * _weaponRange;
				trailScript.SetTargetPosition(endPosition);
			}
		}
	}

	private void Move()
	{
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		movement = new Vector2(inputX, inputY);

		rb.transform.Translate(movement * speed * Time.deltaTime);
	}

	private void LookAtMouse()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
	}



	/*public void ReceiveHit(RaycastHit2D hit)
	{
		return;
	}*/

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("AAA");


    }
}
