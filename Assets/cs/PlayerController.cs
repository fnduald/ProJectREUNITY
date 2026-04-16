using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 씬 재시작(41p)을 위해 반드시 필요합니다.

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("바닥 체크 설정")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    // 슬라이드 10p: 게임 시작 시 Rigidbody2D 컴포넌트 연결
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 좌우 이동 처리 (기존 코드 유지)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 바닥 체크 (OverlapCircle을 사용하여 특정 레이어와 닿아있는지 확인)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Input System: 이동 입력 처리
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    // Input System: 점프 입력 처리
    public void OnJump(InputValue value)
    {
        // 바닥에 있을 때만 점프 가능
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // 슬라이드 41p: 함정(DeadZone) 충돌 감지 로직 추가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 물체의 태그가 "Dead"라면 죽음 처리
        // (유니티 인스펙터 창에서 DeadZone의 Tag를 "Dead"로 설정해야 합니다!)
        if (collision.CompareTag("Dead"))
        {
            RestartLevel();
        }
    }

    // 사망 시 씬을 다시 로드하는 함수
    void RestartLevel()
    {
        Debug.Log("사망! 스테이지를 재시작합니다.");

        // 현재 활성화된 씬의 이름을 가져와서 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}