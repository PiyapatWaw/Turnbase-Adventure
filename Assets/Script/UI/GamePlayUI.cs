using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI turnText,killText;
        
        public void UpdateUIText()
        {
            turnText.text = "Turn : " + GameManager.Singleton.Turn;
            killText.text = "Kill : " + GameManager.Singleton.Kill;
        }
    }
}