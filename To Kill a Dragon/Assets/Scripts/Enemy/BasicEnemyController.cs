using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	private EnemyController enemyControl;
	private WanderController wanderControl;

	private GameController gameControl;

	#region Stats

	public string Name;
	public float HP;

	public float Atk;
	public float Def;

	public float EXPValue;

	#endregion

	#region Movement Modifiers

	public float damageKnockback;

	#endregion

	#region Projectiles

	public Transform projectile;

	private bool canShoot;

	#endregion

	#region Item Drops

	public Transform[] itemDrops;

	public float[] itemNumbers;

	#endregion

	// Use this for initialization
	void Start () {
				wanderControl = GetComponentInChildren<WanderController> ();
				enemyControl = GetComponentInChildren<EnemyController> ();

				damageKnockback = Mathf.Max (1, damageKnockback);

				if (projectile != null) {
						enemyControl.CanShoot (true);
				} else {
						enemyControl.CanShoot (false);
				}

				/*
				if (itemDrops.Length != itemPercentages.Length) {
						Debug.Log (gameObject.name + "'s items are not set properly.");
				} else if (itemDrops.Length > 0) {
						float totalPercent = 0;

						foreach (float percent in itemPercentages) {
								totalPercent += percent;
						}

						if (!Mathf.Approximately (totalPercent, 100)) {
								Debug.Log (gameObject.name + "'s items are not set properly.");
						}
				}*/

				if (itemDrops.Length != itemNumbers.Length) {
						Debug.LogError ("Something is wrong with the item drops of " + gameObject.name);
				}
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
				if (HP <= 0) {
						gameControl.DestroyMonster (gameObject);
			NotifyDeath();
				}
		}

	/**
	 * Gets a new point whenever the moving part of the enemy reaches its position
	 * **/
	public void getNewPoint(){
				enemyControl.setPathPoint (wanderControl.getRandomPoint ());
		}

	/**
	 * Sets the new direction for the animator
	 * **/
	public void setDirection(Animator _animator, float direction){
				//_animator.SetFloat ("Direction", direction);
		}

	/**
	 * Sets the speed for the animator
	 * **/
	public void setSpeed(Animator _animator, float speed){
				//_animator.SetFloat ("Speed", speed);
		}

	/**
	 * Deals damage to player
	 * **/
	public void DealDamage(Vector3 playerDirection, Vector3 myDirection){
				gameControl.DealPlayerDamage (gameObject, playerDirection, myDirection);
		}

	/**
	 * Take damage from spell
	 * **/
	public void TakeDamage(Vector3 flinch){
				Spell spellCast = gameControl.getSpell ();
				HP -= Mathf.Max (0, spellCast.getSpellDamage () - Def);
				KnockBack (flinch, spellCast.getSpellKnockback ());
		}

	/**
	 * Take damage from magnet block
	 * **/
	public void MagnetDamage(Vector3 flinch){
				HP -= 1;
				MagnetKnockBack (flinch, 1);
		}

	/**
	 * How much EXP is this monster worth?
	 * **/
	public float valueEXP(){
				return EXPValue;
		}

	/**
	 * Tells the model to flinch back
	 * **/
	public void FlinchBack(Vector3 flinch){
				enemyControl.FlinchBack (flinch);
		}

	/**
	 * Tells the model to be knocked back
	 * **/
	public void KnockBack(Vector3 direction, float knockback){
				enemyControl.KnockBack (direction, knockback);
		}

	/**
	 * Tells the model to be knocked back by a magnet block
	 * **/
	public void MagnetKnockBack (Vector3 direction, float knockback){
				enemyControl.MagnetKnockBack (direction, knockback);
		}

	/**
	 * Gets the knockback of the monster
	 * **/
	public float getKnockback(){
				return damageKnockback;
		}

	/**
	 * Fires the projectile
	 * **/
	public void FireProjectile(Vector3 targetPosition){
				targetPosition.y = transform.position.y;

				Instantiate (projectile,
	                         enemyControl.gameObject.transform.position,
	                         Quaternion.LookRotation (Vector3.RotateTowards (transform.forward,
	                		 												 targetPosition,
	                														 2 * Mathf.PI,
	                														 0)));
		}

	/**
	 * Drops all the items the enemy is holding.
	 * **/
	public void DropItems () {
				for (int i = 0; i<itemDrops.Length; i++) {
						for (int j = 0; j<itemNumbers[i]; j++) {
								DropItem (itemDrops [i]);
						}
				}
		}

	/**
	 * Drops an item.
	 * **/
	private void DropItem (Transform item){
				Instantiate (item,
		             enemyControl.transform.position,
		             Quaternion.identity);
		}

	/**
	 * Instantly kills the enemy.
	 * **/
	public void Die(){
				HP = 0;
		}

	/**
	 * Notifies the parent (if one exists) that it died.
	 * **/
	private void NotifyDeath () {
				if (transform.parent != null) {
						GetComponentInParent<DeathTrigger> ().NotifyDeath ();
				}
		}
}
