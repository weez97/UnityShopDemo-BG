using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class GameManager : MonoBehaviour
{
    public GameObject playerFab;

    // GAME VARIABLES
    private Player player;
    private EnumConfig.GameState game_state; // serialized for visualization only
    public EnumConfig.GameState GameState
    {
        get { return game_state; }
        set
        {
            game_state = value;
            if (player) player.CanMove = value == EnumConfig.GameState.GAME_RUNNING;
        }
    }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        UiManager.instance.ShowScreen(EnumConfig.ScreenID.MAIN_MENU, "slideIn");
    }

    private void Init()
    {
        ScreenMainMenu.onStartGame += StartGame;
        GameState = EnumConfig.GameState.GAME_INIT;
    }

    private void StartGame()
    {
        ScreenMainMenu.onStartGame -= StartGame;
        if (player == null)
            player = Instantiate(playerFab).GetComponent<Player>();
        
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.target = player.transform;
        cam.lerpSpeed = 2.5f;
        cam.InitCamera();

        InteractableObject.onInteract += OnInteract;

        // lastly, we start the game
        GameState = EnumConfig.GameState.GAME_RUNNING;
    }

    private void OnInteract(bool value)
    {
        if (value) GameState = EnumConfig.GameState.GAME_DIALOGUE;
        else GameState = EnumConfig.GameState.GAME_RUNNING;
    }

    private void EndGame()
    {

    }

}
