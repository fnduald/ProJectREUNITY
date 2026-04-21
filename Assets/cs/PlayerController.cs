using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("기본 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    private void Update()
    {
        // 1. 바닥 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 2. 좌우 반전
        FlipCharacter();

        // 3. 애니메이션 상태 업데이트
        pAni.SetBool("isGrounded", isGrounded);
        // 이동 중인지 확인하여 달리기 애니메이션 제어 (필요시 추가)
        pAni.SetFloat("MoveSpeed", Mathf.Abs(moveInput));
    }

    private void FixedUpdate()
    {
        // 4. 물리 이동 (FixedUpdate에서 처리하여 끊김 방지)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void FlipCharacter()
    {
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    // Input System과 연동되는 메서드
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (transform.position.y > collision.transform.position.y + 0.3f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.5f);
                Destroy(collision.gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (collision.CompareTag("Dead"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}