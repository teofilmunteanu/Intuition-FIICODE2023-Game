using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    #region SingletonInit
    public static SceneChangeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SetSceneIndex();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    #endregion

    #region Fields
    private readonly string mainMenuScene = "MainMenu";
    private readonly string[] gameScenes = new string[]
    {
        "Hallway",
        "Room1",
        "Room2",
        "Room3",
        "Room4",
        "Room5"
    };

    public readonly Vector3 InitialHallwayPos = new Vector3(545, 1, 518.5f);

    public Vector3 LastPlayerPosition { get; set; }
    public int CurrentSceneIndex { get; private set; }
    #endregion fields

    public void SetSceneIndex()
    {
        string name = SceneManager.GetActiveScene().name;

        switch (name)
        {
            case "MainMenu":
                CurrentSceneIndex = -1;
                break;
            case "Hallway":
                CurrentSceneIndex = 0;
                break;
            case "Room1":
                CurrentSceneIndex = 1;
                break;
            case "Room2":
                CurrentSceneIndex = 2;
                break;
            case "Room3":
                CurrentSceneIndex = 3;
                break;
            case "Room4":
                CurrentSceneIndex = 4;
                break;
            default:
                return;
        }
    }

    public void LoadRoom(int targetRoomNr, Vector3 lastPlayerPosition)
    {
        try
        {
            UpdateLastPlayerPosition(lastPlayerPosition);

            if (targetRoomNr == 1 || IsRoomCompleted(targetRoomNr - 1))
            {
                SceneManager.LoadScene(gameScenes[targetRoomNr]);
            }
            else
            {
                Debug.Log("Room inaccessible, complete previous rooms.");
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.LogException(ex);
        }
        catch (Exception ex)
        {
            Debug.Log("Invalid room. Error:" + ex.Message);
        }
    }

    bool IsRoomCompleted(int roomNr)
    {
        return ProgressManager.Instance.CompletedRooms[roomNr - 1];
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(gameScenes[0]);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetSceneIndex();

        if (IsMainScene())
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = LastPlayerPosition;
        }
    }

    public bool IsMainScene()
    {
        return CurrentSceneIndex == 0;
    }

    public void UpdateLastPlayerPosition(Vector3 position)
    {
        LastPlayerPosition = position;
    }

    public void ResetPosition()
    {
        LastPlayerPosition = InitialHallwayPos;
    }
}
