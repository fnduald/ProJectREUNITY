using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("적 설정")]
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator eAni; // 71p: 적 애니메이터 변수 추가
    private bool isMovingRight = true;
    private bool isDead = false; // 죽음 상태 체크

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        eAni = GetComponent<Animator>(); // 애니메이터 연결
    }

    private void Update()
    {
        if (isDead) return; // 죽었으면 이동 금지

        // 좌우 이동 로직
        rb.linearVelocity = new Vector2(isMovingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);
    }

    // 외부(플레이어)에서 이 함수를 호출해 죽음 처리
    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero; // 이동 정지
        eAni.SetTrigger("Die"); // 71p: 사망 애니메이션 재생

        // 애니메이션 재생 후 오브젝트 삭제 (0.5초 뒤 삭제)
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        // 경계선(Boundary)에 닿으면 방향 반전
        if (collision.CompareTag("Boundary"))
        {
            isMovingRight = !isMovingRight;
            transform.localScale = new Vector3(isMovingRight ? 1f : -1f, 1f, 1f);
        }
    }
}