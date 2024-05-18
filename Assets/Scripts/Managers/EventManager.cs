using UnityEngine;
using UnityEngine.Events;

// A singleton that manages all the events that used to trigger things across the game
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public class ApplyCharm : UnityEvent<CharmInfo> { }
    public ApplyCharm OnApplyCharm = new ApplyCharm();
    public class CharmSelection : UnityEvent<CharmInfo[], UnityAction<CharmInfo>> { }
    public CharmSelection OnCharmSelection = new CharmSelection();

    public class LevelUp : UnityEvent<int> { }
    public LevelUp OnLevelUp = new LevelUp();
    public class IncreasedSnakeCount : UnityEvent<int, int> { }
    public IncreasedSnakeCount OnIncreasedSnakeCount = new IncreasedSnakeCount();
    public class SetScore : UnityEvent<int> { }
    public SetScore OnSetScore = new SetScore();

    // Used when snakes are left on the pot
    public class PotFill : UnityEvent { }
    public PotFill OnPotFill = new PotFill();

    // Used when a snake is charmed
    public class SnakeCharmed : UnityEvent { }
    public SnakeCharmed OnSnakeCharmed = new SnakeCharmed();

    // Used when a snake spawns
    public class SnakeSpawn : UnityEvent<SnakeController> { }
    public SnakeSpawn OnSnakeSpawn = new SnakeSpawn();

    // Used when the player is attacked
    public class PlayerHit : UnityEvent { }
    public PlayerHit OnPlayerHit = new PlayerHit();

    // Used when the game is restarted
    public class RestartGame : UnityEvent { }
    public RestartGame OnRestartGame = new RestartGame();

    // Used when the game is paused
    public class PauseGame : UnityEvent { }
    public PauseGame OnPauseGame = new PauseGame();

    public class StartCharm : UnityEvent<float, int> { }
    public StartCharm OnStartCharm = new StartCharm();

    public class StopCharm : UnityEvent { }
    public StopCharm OnStopCharm = new StopCharm();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
}