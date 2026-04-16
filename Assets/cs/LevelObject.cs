using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 꼭 필요함

public class LevelObject : MonoBehaviour
{
    // 다음으로 이동할 씬의 이름을 인스펙터 창에서 입력받습니다.
    public string nextLevel;

    // 플레이어가 깃발(Goal)에 닿으면 실행됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 물체의 태그가 "Player"인지 확인
        if (collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        // 씬 이름이 비어있지 않은지 확인 후 로드
        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("인스펙터 창에서 Next Level 이름을 설정해주세요!");
        }
    }
}