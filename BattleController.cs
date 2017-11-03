using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

	public enum BattleStates {
	START,
	PLAYERCHOICE_ACT,
	PLAYERCHOICE_TARGET,
	PLAYERCHOICE_ATTACK,
	PLAYERACTION_EXECUTION,
	ENEMY,
	POWER,
	END
	};

	public KeyCode next;
	public Text displayText;
	public BattleStates currentState;

	public GameObject player;

	public GameObject PlayerBattleOptions;
	private int menuIndex;
	public GameObject fight;
	private int fightIndex;
	public GameObject defend;
	public GameObject item;

	public GameObject attackOptions;
	public List<GameObject> attacks;
	public GameObject attack1;
	public GameObject attack2;
	public GameObject attackReticle;
	private int attackIndex;

	public List<GameObject> enemies;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject targetReticle;
	private int enemyIndex;

	private AudioSource source;
	public AudioClip menuScroll;
	public AudioClip menuSelect;
	public AudioClip menuCancel;

	// Use this for initialization
	void Start () {
		source = Camera.main.GetComponent<AudioSource>();
		currentState = BattleStates.START;
		menuIndex = 0;
		enemies.Add(enemy1);
		enemies.Add(enemy2);
		attackIndex = 0;
		attacks.Add(attack1);
		attacks.Add(attack2);
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState){ // State machine
		case BattleStates.START:
			if(Input.GetKey(next)){
				currentState = BattleStates.PLAYERCHOICE_ACT;
				source.PlayOneShot(menuSelect);
			}
			break;
		case BattleStates.PLAYERCHOICE_ACT:
			menuIndex = chooseAction();
			if (menuIndex == 0 && Input.GetKeyDown(KeyCode.RightArrow)){
				currentState = BattleStates.PLAYERCHOICE_ATTACK;
				source.PlayOneShot(menuSelect);
			}
			break;
		case BattleStates.PLAYERCHOICE_ATTACK:
			chooseAttack();
			break;
		case BattleStates.PLAYERCHOICE_TARGET:
			chooseTarget();
			break;
		case BattleStates.PLAYERACTION_EXECUTION:
			execution();
			break;
		case BattleStates.ENEMY:
			break;
		case BattleStates.POWER:
			break;
		case BattleStates.END:
			break;
		}

		switch(currentState){ // UI Control
		case BattleStates.START:
			displayText.text = "START";
			targetReticle.SetActive(false);
			PlayerBattleOptions.SetActive(false);
			attackOptions.SetActive(false);
			break;
		case BattleStates.PLAYERCHOICE_ACT:
			PlayerBattleOptions.SetActive(true);
			attackOptions.SetActive(false);
			displayText.text = "PLAYERCHOICE_ACT";
			break;
		case BattleStates.PLAYERCHOICE_ATTACK:
			PlayerBattleOptions.SetActive(false);
			attackOptions.SetActive(true);
			targetReticle.SetActive(false);
			displayText.text = "PLAYERCHOICE_ATTACK";
			break;
		case BattleStates.PLAYERCHOICE_TARGET:
			attackOptions.SetActive(false);
			targetReticle.SetActive(true);
			displayText.text = "PLAYERCHOICE_TARGET";
			break;
		case BattleStates.PLAYERACTION_EXECUTION:
			targetReticle.SetActive(false);
			displayText.text = "PLAYERACTION_EXECUTION";
			break;
		case BattleStates.ENEMY:
			Debug.Log(currentState);
			break;
		case BattleStates.POWER:
			Debug.Log(currentState);
			break;
		case BattleStates.END:
			Debug.Log(currentState);
			break;
		}
	}

	int chooseAction(){
		Vector3 fightScale;
		Vector3 defendScale;
		Vector3 itemScale;

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			menuIndex += 2;
			source.PlayOneShot(menuScroll);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			menuIndex++;
			source.PlayOneShot(menuScroll);
		}

		switch(menuIndex % 3){
			case 0: //fight
				fightScale = fight.transform.localScale;
				fightScale.x = .05f;
				fight.transform.localScale = fightScale;
				defendScale = defend.transform.localScale;
				defendScale.x = .025f;
				defend.transform.localScale = defendScale;
				itemScale = item.transform.localScale;
				itemScale.x = .025f;
				item.transform.localScale = itemScale;
				break;
			case 1: //defend
				defendScale = defend.transform.localScale;
				defendScale.x = .05f;
				defend.transform.localScale = defendScale;
				fightScale = fight.transform.localScale;
				fightScale.x = .025f;
				fight.transform.localScale = fightScale;
				itemScale = item.transform.localScale;
				itemScale.x = .025f;
				item.transform.localScale = itemScale;
				break;
			case 2: //item
				itemScale = item.transform.localScale;
				itemScale.x = .05f;
				item.transform.localScale = itemScale;
				defendScale = defend.transform.localScale;
				defendScale.x = .025f;
				defend.transform.localScale = defendScale;
				fightScale = fight.transform.localScale;
				fightScale.x = .025f;
				fight.transform.localScale = fightScale;
				break;
			default:
				break;
		}

		return menuIndex % 3;
	}

	void chooseAttack(){
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			currentState = BattleStates.PLAYERCHOICE_ACT;
			source.PlayOneShot(menuCancel);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			currentState = BattleStates.PLAYERCHOICE_TARGET;
			source.PlayOneShot(menuSelect);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			attackIndex++;
			source.PlayOneShot(menuScroll);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			attackIndex += attacks.Count+1;
			source.PlayOneShot(menuScroll);
		}
		attackReticle.transform.position = 
			attacks[attackIndex % 2].transform.position + 
			attackReticle.transform.TransformDirection(new Vector3(-2.25f, 1.25f, 0));
	}

	void chooseTarget(){
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			currentState = BattleStates.PLAYERCHOICE_ATTACK;
			source.PlayOneShot(menuCancel);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			source.PlayOneShot(menuScroll);
			enemyIndex++;
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			currentState = BattleStates.PLAYERACTION_EXECUTION;
			source.PlayOneShot(menuSelect);
		}
		targetReticle.transform.position = 
			enemies[enemyIndex % 2].transform.position + 
			targetReticle.transform.TransformDirection(new Vector3(0, -2, 1));
	}

	void execution(){
		if (attackIndex % 2 == 0) //jump
			//if(enemies[enemyIndex % 2].transform.position.x - player.transform.position.x > 5)
			player.transform.Translate(Vector3.right * Time.deltaTime * 5);

		if (attackIndex % 2 == 1) //charge
			currentState = BattleStates.PLAYERCHOICE_ACT;
	}

}
