using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerFab;

    // GAME VARIABLES
    private Player player;
    private int player_wallet;
    public int PlayerMoney { get { return player_wallet; } }
    private List<string> owned_suits = new List<string>();
    public bool IsEverthingOwned { get { return owned_suits.Count >= 5; } }

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
        if (instance == null) instance = this;
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

        // gift default at the start of the game
        owned_suits.Add("default");

        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.target = player.transform;
        cam.lerpSpeed = 2.5f;
        cam.InitCamera();

        InteractableObject.onInteract += OnInteract;
        Reward.onPay += PlayerGetPaid;
        ScreenShop.onShopTransaction += OnShopTransaction;

        // lastly, we start the game
        GameState = EnumConfig.GameState.GAME_RUNNING;
    }

    private void OnInteract(bool value)
    {
        if (value) GameState = EnumConfig.GameState.GAME_DIALOGUE;
        else GameState = EnumConfig.GameState.GAME_RUNNING;
    }


    private void PlayerGetPaid(int amount)
    {
        player_wallet += amount;

        UiManager.instance.counter.UpdateCounter(player_wallet);
    }

    private void OnShopTransaction(string id, int price, bool is_sell)
    {
        string new_outfit = "default";

        if (is_sell)
        {
            owned_suits.Remove(id);
            player_wallet += price;
            UiManager.instance.counter.UpdateCounter(player_wallet);
        }
        else
        {
            if (!CheckOutfit(id))
            {
                owned_suits.Add(id);
                player_wallet -= price;
                UiManager.instance.counter.UpdateCounter(player_wallet);
            }
            new_outfit = id;
        }
        player.ChangeOutfit(new_outfit);
    }

    public bool CheckOutfit(string id)
    {
        for (int i = 0; i < owned_suits.Count; i++)
            if (owned_suits[i] == id) return true;
        return false;
    }

    private void EndGame()
    {

    }

}
