using UnityEngine;
using UnityEditor;

public class GameConfigEditorWindow : EditorWindow
{
    public GameConfig GameConfig;

    public bool IsShowMainSettings = false;
    public bool IsShowGameModesSettings = false;
    public bool IsShowSurpriseSettings = false;
    public bool IsShowDiceParameters = false;
    public bool IsShowExtraOptionsDiceParameters = false;
    public bool IsShowGameHelperSettings = false;
    public bool IsShowPromoCodes = false;

    private Vector2 _scrollPosition = Vector2.zero;



    [MenuItem("Tools/Dice Manager Window")]
    private static void Init()
    {
        GetWindow<GameConfigEditorWindow>("Dices Manager Editor");
    }


    private void OnEnable()
    {
        LoadConfig();
    }


    private void OnGUI()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, true, true, GUILayout.Width(position.width), GUILayout.Height(position.height));

        DrawButtons(); 
        GUILayout.Space(10);

        DrawMainSettings(); 
        GUILayout.Space(10);

        DrawGameModeSettings(); 
        GUILayout.Space(10);

        DrawSurpriseAndEnergySettings(); 
        GUILayout.Space(10);

        DrawDiceParameters();
        GUILayout.Space(10);

        DrawGameHelperData();
        GUILayout.Space(10);

        DrawPromoCodes();

        EditorGUILayout.EndScrollView();
    }


    private void DrawButtons()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Save Setting"))
        {
            SaveConfig();
        }
        GUILayout.EndHorizontal();
    }


    private void DrawMainSettings()
    {
        IsShowMainSettings = EditorGUILayout.Foldout(IsShowMainSettings, "Main Settings", true);

        if (IsShowMainSettings)
        {
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("Is Debug");
                    GameConfig.IsDebug = EditorGUILayout.Toggle(GameConfig.IsDebug);
                }
                GUILayout.EndHorizontal();
                
                GUILayout.Space(20);

                GameConfig.MaxAmountEnergy = (int)EditorGUILayout.IntField("Max Amount Energy", GameConfig.MaxAmountEnergy);
                GameConfig.EnergyRecoveryTime = (int)EditorGUILayout.IntField("Energy Recovery Time", GameConfig.EnergyRecoveryTime);

                GUILayout.Space(20);

                EditorGUILayout.HelpBox("GLC - Game Launch Count", MessageType.None);

                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("GLC For Receipt Surprise");
                    GUILayout.Space(20);
                    GameConfig.GameLaunchCountForReceiptSurprise = (int)EditorGUILayout.IntField(GameConfig.GameLaunchCountForReceiptSurprise, GUILayout.Width(150));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("GLC For Open Subscription Popup");
                    GUILayout.Space(20);
                    GameConfig.GameLaunchCountForOpenSubscriptionPopup = (int)EditorGUILayout.IntField(GameConfig.GameLaunchCountForOpenSubscriptionPopup, GUILayout.Width(150));
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(30);

                EditorGUILayout.HelpBox("Links to sites", MessageType.None);

                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("Terms Of Use");
                    GameConfig.TermsOfUseURL = EditorGUILayout.TextField(GameConfig.TermsOfUseURL, GUILayout.Width(250));
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel("Privacy Policy");
                    GameConfig.PrivacyPolicyURL = EditorGUILayout.TextField(GameConfig.PrivacyPolicyURL, GUILayout.Width(250));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
    }


    private void DrawGameModeSettings()
    {
        IsShowGameModesSettings = EditorGUILayout.Foldout(IsShowGameModesSettings, "Game Modes Settings", true);

        if (IsShowGameModesSettings)
        {
            GUILayout.BeginVertical("Box");
            {
                foreach (GameModeSettings gameModeSettings in GameConfig.GameModeSettings)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Mode");
                        gameModeSettings.GameModeType = (GameModeType)EditorGUILayout.EnumPopup(gameModeSettings.GameModeType);
                        GUILayout.Label("Energy Required");
                        gameModeSettings.EnergyRequired = (int)EditorGUILayout.IntField(gameModeSettings.EnergyRequired);
                        GUILayout.Label("Coefficient");
                        gameModeSettings.WinCoefficient = (float)EditorGUILayout.FloatField(gameModeSettings.WinCoefficient);
                        GUILayout.Label("Min Rate");
                        gameModeSettings.MinRate = (int)EditorGUILayout.IntField(gameModeSettings.MinRate);

                        if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            GameConfig.GameModeSettings.Remove(gameModeSettings);
                            break;
                        }
                    }
                    GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add Mode"))
                {
                    GameConfig.GameModeSettings.Add(new GameModeSettings());
                }
            }
            GUILayout.EndVertical();
        }
    }


    private void DrawSurpriseAndEnergySettings()
    {
        IsShowSurpriseSettings = EditorGUILayout.Foldout(IsShowSurpriseSettings, "Surprise Box Settings", true);

        if (IsShowSurpriseSettings)
        {
            GUILayout.BeginVertical("Box");
            {
                GUILayout.BeginVertical("Box", GUILayout.Width(500));
                {
                    GUILayout.Label("Chips");
                    GameConfig.RangeChipValue.Min = (int)EditorGUILayout.IntField("Min Chip Reward", GameConfig.RangeChipValue.Min);
                    GameConfig.RangeChipValue.Max = (int)EditorGUILayout.IntField("Max Chip Reward", GameConfig.RangeChipValue.Max);
                }
                GUILayout.EndVertical();


                GUILayout.BeginVertical("Box", GUILayout.Width(500));
                {
                    GUILayout.Label("Energy");
                    GameConfig.RangeEnergyValue.Min = (int)EditorGUILayout.IntField("Min Energy Reward", GameConfig.RangeEnergyValue.Min);
                    GameConfig.RangeEnergyValue.Max = (int)EditorGUILayout.IntField("Max Energy Reward", GameConfig.RangeEnergyValue.Max);
                }
                GUILayout.EndVertical();

                GUILayout.Space(20);

                GUILayout.Label("Energy Drop Chance");
                GameConfig.EnergyDropChance = (float)EditorGUILayout.Slider(GameConfig.EnergyDropChance, 0f, 1f);
            }
            GUILayout.EndVertical();
        }
    }


    private void DrawDiceParameters()
    {
        IsShowDiceParameters = EditorGUILayout.Foldout(IsShowDiceParameters, "Dice Parameters", true);

        if (IsShowDiceParameters)
        {
            GUILayout.BeginVertical("Box");
            {
                int id = 1;

                foreach (DiceParameter diceParameter in GameConfig.DiceParameters)
                {
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            GameConfig.DiceParameters.Remove(diceParameter);
                            break;
                        }

                        GUILayout.Label($"Dice #{id}");
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Dice ID");
                        diceParameter.DiceID = EditorGUILayout.TextField(diceParameter.DiceID);
                        GUILayout.Label("Dice Prefab");
                        diceParameter.BonePrefab = (Bone)EditorGUILayout.ObjectField(diceParameter.BonePrefab, typeof(Bone), false);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    { 
                        GUILayout.Label("Body Color");
                        diceParameter.BodyColor = EditorGUILayout.ColorField(diceParameter.BodyColor);
                        GUILayout.Label("Points Color");
                        diceParameter.PointsColor = EditorGUILayout.ColorField(diceParameter.PointsColor);
                    }
                    GUILayout.EndHorizontal();
                    
                    GUILayout.Space(10);

                    IsShowExtraOptionsDiceParameters = EditorGUILayout.Foldout(IsShowExtraOptionsDiceParameters, "Extra Options", true);

                    if (IsShowExtraOptionsDiceParameters)
                    {
                        diceParameter.AddingCoefficient = EditorGUILayout.FloatField("Adding Coefficient", diceParameter.AddingCoefficient);
                    }

                    GUILayout.Space(25);

                    id++;
                }

                if (GUILayout.Button("Add Mode"))
                {
                    GameConfig.DiceParameters.Add(new DiceParameter());
                }
            }
            GUILayout.EndVertical();

        }
    }


    private void DrawGameHelperData()
    {
        IsShowGameHelperSettings = EditorGUILayout.Foldout(IsShowGameHelperSettings, "Game Helper Settings", true);

        if (!IsShowGameHelperSettings) return;

        GUILayout.BeginVertical("Box");
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Required Wins Count");
                GameConfig.RequiredWinsCount = EditorGUILayout.IntField(GameConfig.RequiredWinsCount);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Loss Bet");
                GameConfig.LossBet = EditorGUILayout.IntSlider(GameConfig.LossBet, 0, 1000);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }


    private void DrawPromoCodes()
    {
        IsShowPromoCodes = EditorGUILayout.Foldout(IsShowPromoCodes, "Promo Codes", true);
        if (!IsShowPromoCodes) return;

        GameConfig.PromoCodeReward = EditorGUILayout.IntField("Promo Code Reward", GameConfig.PromoCodeReward);

        GUILayout.Space(20);

        for (int i = 0; i < GameConfig.PromoCodes.Count; i++)
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.PrefixLabel($"Promo Code #{i + 1}");
                GameConfig.PromoCodes[i] = EditorGUILayout.TextField(GameConfig.PromoCodes[i], GUILayout.Width(200));

                if(GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    GameConfig.PromoCodes.RemoveAt(i);
                    break;
                }
            }
            GUILayout.EndHorizontal();
        }

        if(GUILayout.Button("Add Promo Code"))
        {
            GameConfig.PromoCodes.Add(string.Empty);
        }
    }


    private void SaveConfig()
    {
        EditorUtility.SetDirty(GameConfig);
        AssetDatabase.SaveAssets();
    }


    private void LoadConfig()
    {
        GameConfig = Resources.Load<GameConfig>("GameConfig");
    }
}
