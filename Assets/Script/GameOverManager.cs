using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//ゲームオーバー画面スクリプト
public class GameOverManager : MonoBehaviour
{
    public GameObject characterObject; //キャラクターオブジェクト参照
    public TextMeshProUGUI gameOverText;　//ゲームオーバーテキスト参照

    public Sprite sermonSprite;　//説教画像
    public Sprite escapeSprite;　//脱走画像
    public SpriteRenderer spriteRenderer; // SpriteRendererコンポーネントの参照

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isGameOverBranch) Escape();
        else Sermon();
    }

    private void Sermon()
    {
        gameOverText.text = "ばれちゃった・・・";
        spriteRenderer.sprite = sermonSprite;
    }

    private void Escape()
    {
        gameOverText.text = "脱走してしまった・・・";
        spriteRenderer.sprite = escapeSprite;
    }
}
