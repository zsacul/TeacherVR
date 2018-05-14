using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private GameController _gameController;
    
    public void LoadData()
    {
        Debug.Log("LoadData");
        _gameController = GameController.Instance;
        _gameController.Tooltips = PlayerPrefs.GetInt("Tooltips", 1) == 1;
        _gameController.Messages = PlayerPrefs.GetInt("Messages", 1) == 1;
        _gameController.ChangeLocomotion(PlayerPrefs.GetInt("ArmSwinger", 1) == 1);
        _gameController.SoundManager.SetGlobalVolume(PlayerPrefs.GetFloat("Volume", 1));
        int[] _TopScore =
        {
            PlayerPrefs.GetInt("Top1", 0), PlayerPrefs.GetInt("Top2", 0), PlayerPrefs.GetInt("Top3", 0),
            PlayerPrefs.GetInt("Top4", 0), PlayerPrefs.GetInt("Top5", 0)
        };
        _gameController.ScoreBoard.SetTopScore(_TopScore);
    }

    public void SaveData()
    {
        Debug.Log("SaveData");
        _gameController = GameController.Instance;
        PlayerPrefs.SetInt("Tooltips", _gameController.Tooltips ? 1 : 0);
        PlayerPrefs.SetInt("Messages", _gameController.Messages ? 1 : 0);
        PlayerPrefs.SetInt("ArmSwinger", _gameController.IsLocomotionArmSwinger() ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", _gameController.SoundManager.GetGlobalVolume());
        PlayerPrefs.SetInt("Top1", _gameController.ScoreBoard.GetTopScore()[0]);
        PlayerPrefs.SetInt("Top2", _gameController.ScoreBoard.GetTopScore()[1]);
        PlayerPrefs.SetInt("Top3", _gameController.ScoreBoard.GetTopScore()[2]);
        PlayerPrefs.SetInt("Top4", _gameController.ScoreBoard.GetTopScore()[3]);
        PlayerPrefs.SetInt("Top5", _gameController.ScoreBoard.GetTopScore()[4]);
    }

    public void ResetAllData()
    {
        Debug.Log("ResetData");
        PlayerPrefs.DeleteAll();
        LoadData();
    }
}