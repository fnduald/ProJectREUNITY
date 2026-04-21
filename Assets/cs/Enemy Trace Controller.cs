using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .5f;
    public float raycastDistance = .2f;
    public float traceDistance = 2f;

    private Transform player;

    private void Start()
    {
        // "Player" 태그를 가진 오브젝트를 찾아 추적 대상으로 설정
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // 플레이어와 적 사이의 방향 계산
        Vector2 direction = player.position - transform.position;

        // 추적 거리를 벗어나면 아무것도 하지 않음
        if (direction.magnitude > traceDistance)
            return;

        // 방향 벡터 정규화
        Vector2 directionNormalized = direction.normalized;

        // 레이캐스트를 쏴서 앞쪽에 장애물이 있는지 확인
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        bool obstacleFound = false;
        foreach (RaycastHit2D rHit in hits)
        {
            // 장애물을 발견하면
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                obstacleFound = true;
                // 90도 회전하여 장애물을 우회하는 방향 계산
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
        }

        // 장애물이 없으면 플레이어 방향으로 바로 이동
        if (!obstacleFound)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}