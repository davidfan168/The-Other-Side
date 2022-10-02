using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum SceneState
{
    LEFT,
    RIGHT
};

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject shooterPanel;
    [SerializeField] private GameObject castlePanel;
    [NonSerialized] public SceneState state = SceneState.LEFT;
    [NonSerialized] public bool inTransition;
    [NonSerialized] public UnityAction PostActivateLeft;
    [NonSerialized] public UnityAction PostActivateRight;
    public int gold = 0;
    [SerializeField] private Text goldText;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject castle;
    public List<Turret> turrets;
    public List<Enemy> enemies;
    
    private float timePassed;
    private const string TIMER_STR = "Time: ";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        timePassed += Time.deltaTime;
        if (inTransition)
        {
            if (timePassed > 0.8)
            {
                inTransition = false;
                timePassed = 0;
                if (state == SceneState.LEFT)
                {
                    state = SceneState.RIGHT;
                    ActivateRight();
                }
                else
                {
                    Debug.Assert(state == SceneState.RIGHT);
                    state = SceneState.LEFT;
                    ActivateLeft();
                }
            }
        }
        
        if (state == SceneState.LEFT)
        {
            if (timePassed < 10)
            {
                timeText.text = TIMER_STR + (10 - timePassed).ToString("0.000");
            }
            else
            {
                timeText.text = TIMER_STR + "10.000";
                cameraAnimator.SetTrigger("to_right");
                inTransition = true;
                timePassed = 0;
            }
        }

        if (state == SceneState.RIGHT)
        {
            if (timePassed < 10)
            {
                timeText.text = TIMER_STR + (10 - timePassed).ToString("0.000");
            }
            else
            {
                timeText.text = TIMER_STR + "10.000";
                cameraAnimator.SetTrigger("to_left");
                inTransition = true;
                timePassed = 0;
            }
        }
    }

    void ActivateLeft()
    {
        castlePanel.SetActive(false);
        shooterPanel.SetActive(true);
        PostActivateLeft.Invoke();
    }

    void ActivateRight()
    {
        shooterPanel.SetActive(false);
        castlePanel.SetActive(true);
        PostActivateRight.Invoke();
    }

    public void ChangeGold(int i)
    {
        gold += i;
        goldText.text = "Gold: " + gold;
    }

    public Transform GetTarget()
    {
        if (state == SceneState.RIGHT)
        {
            for (int i = 0; i < turrets.Count; i++)
            {
                if (turrets[i].isAlive())
                {
                    return turrets[i].transform;
                }
            }
            return castle.transform;
        }
        else
        {
            Debug.Assert(state == SceneState.LEFT);
            return player.transform;
        }
    }

    public Transform GetTargetForTurret()
    {
        if (state == SceneState.RIGHT)
        {
            if (enemies.Count > 0)
            {
                return enemies[0].transform;
            }

            return player.transform;
        }
        else
        {
            Debug.Assert(state == SceneState.LEFT);
            return player.transform;
        }
    }
}
