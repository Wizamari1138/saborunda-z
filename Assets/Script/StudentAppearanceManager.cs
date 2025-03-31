using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生徒の見た目スクリプト
public class StudentAppearanceManager : MonoBehaviour
{
    public SpriteRenderer studentSpriteRenderer; // 生徒の見た目を変更するためのスプライトレンダラー
    public List<Sprite> happinessStages;        // 幸福度に応じた見た目（4段階分のスプライト）
    public Sprite sleepSprite;                 // 居眠り中の見た目
    public Sprite eatSnacksSprite;             // お菓子を食べているときの見た目
    public Sprite playGameSprite;              // ゲームをしているときの見た目

    private GameManager gameManager;           // 幸福度を取得するための参照
    private string currentAction = "";         // 現在のアクションを追跡
    private Sprite defaultSprite;              // デフォルトの見た目を保持

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (studentSpriteRenderer != null)
        {
            defaultSprite = studentSpriteRenderer.sprite; // 初期スプライトを保存
        }
    }

    void Update()
    {
        // アクションが設定されていない場合は幸福度に応じた見た目を変更
        if (string.IsNullOrEmpty(currentAction))
        {
            UpdateAppearanceByHappiness();
        }
    }

    /// <summary>
    /// 幸福度に応じた見た目を変更
    /// </summary>
    private void UpdateAppearanceByHappiness()
    {
        float happiness = gameManager.happiness;

        // 幸福度に応じたスプライトを設定（4段階）
        if (happiness > 75)
        {
            studentSpriteRenderer.sprite = happinessStages[0];
        }
        else if (happiness > 50)
        {
            studentSpriteRenderer.sprite = happinessStages[1];
        }
        else if (happiness > 25)
        {
            studentSpriteRenderer.sprite = happinessStages[2];
        }
        else
        {
            studentSpriteRenderer.sprite = happinessStages[3];
        }
    }

    /// <summary>
    /// 特定のアクションに応じた見た目を設定
    /// </summary>
    /// <param name="action">アクション名</param>
    public void SetActionAppearance(string action)
    {
        currentAction = action;

        switch (action)
        {
            case "Sleep":
                studentSpriteRenderer.sprite = sleepSprite;
                break;
            case "EatSnacks":
                studentSpriteRenderer.sprite = eatSnacksSprite;
                break;
            case "PlayGame":
                studentSpriteRenderer.sprite = playGameSprite;
                break;
            default:
                currentAction = "";
                UpdateAppearanceByHappiness(); // デフォルトの見た目に戻す
                break;
        }
    }

    /// <summary>
    /// アクション終了時に見た目をリセット
    /// </summary>
    public void ResetAppearance()
    {
        currentAction = "";
        UpdateAppearanceByHappiness();
    }
}
