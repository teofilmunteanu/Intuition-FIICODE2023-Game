using Assets.Scripts.DTOs;
using Assets.Scripts.Managers;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    #region SingletonInit
    public static ProgressManager Instance { get; private set; }
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
        }
    }
    #endregion

    private const int nrOfRooms = 3;

    public bool[] CompletedRooms { get; set; }
    public bool[] PaperCollectedInRoom { get; set; }


    public void ResetProgress()
    {
        CompletedRooms = new bool[nrOfRooms];
        PaperCollectedInRoom = new bool[nrOfRooms];
        SceneChangeManager.Instance.ResetPosition();
    }

    public void LoadProgress()
    {
        ProgressDTO progresDTO = SavesManager.LoadProgress();

        CompletedRooms = progresDTO.CompletedRooms;
        PaperCollectedInRoom = progresDTO.PaperCollectedInRoom;

        Vector3 lastPosition = new Vector3(progresDTO.Position[0], progresDTO.Position[1], progresDTO.Position[2]);
        SceneChangeManager.Instance.UpdateLastPlayerPosition(lastPosition);
    }

    public void CompleteRoom(int index)
    {
        if (index >= 1 && index <= nrOfRooms)
        {
            CompletedRooms[index - 1] = true;

            SavesManager.SaveProgress();
        }
    }

    public void AddFoundPaper(int roomNr)
    {
        PaperCollectedInRoom[roomNr] = true;
    }
}
