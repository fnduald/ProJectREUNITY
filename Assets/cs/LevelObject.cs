using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class LevelObject : MonoBehaviour
{
    // 48~49p: 인스펙터 창에서 다음 스테이지 이름을 적을 수 있게 노출
    public string nextLevel;

    // 트리거 충돌 감지 (Is Trigger가 켜져 있어야 작동)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 물체의 태그가 "Player"인지 확인 (49p 핵심 로직)
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어 도달! 다음 레벨로 이동: " + nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }
}