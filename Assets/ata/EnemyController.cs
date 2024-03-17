using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public int damage = 10;
    public float attackCooldown = 4f;
    public LayerMask playerLayer;
    public int maxHealth = 100;
    public float patrolDistance = 5f; // Patrol alanýnýn geniþliði

    private Animator animator;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private int currentHealth;
    private bool movingRight = true; // Baþlangýçta saða doðru hareket edilsin

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        InvokeRepeating("PlayerAttack", 0f, attackCooldown);
        StartCoroutine("Patrol"); // Patrul Coroutine'unu baþlat
    }

    void Update()
    {
        // Düþman ölmediyse ve yürümeye baþlamadýysa ve patrolling modunda ise
        if (!isDead && !isAttacking && isWalking)
        {
            animator.SetBool("iswalk", true);
            // Hareket yönüne göre hareket ettirme
            if (movingRight)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            else
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator Patrol()
    {
        // Düþman yaþamýyor olduðu sürece patrul et
        while (!isDead)
        {
            isWalking = true; // Patrolling moduna gir
            // Rastgele bir süre bekle
            yield return new WaitForSeconds(Random.Range(1f, 4f));

            // Rastgele bir yöne doðru hareket et
            // Eðer düþman saða doðru hareket ediyorsa, sol tarafa gitmesi gerekiyorsa
            if (movingRight)
                movingRight = false;
            else // Eðer düþman sol tarafa hareket ediyorsa, saða gitmesi gerekiyorsa
                movingRight = true;
            Flip(); // Yönü çevir
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void PlayerAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWalking = false; // Hareket etmeyi durdur
            isAttacking = true; // Saldýrý moduna geç
            animator.SetBool("iswalk", false);
            animator.SetBool("isattack", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false; // Saldýrý modunu sonlandýr
            animator.SetBool("isattack", false);
            animator.SetBool("iswalk", true);
            isWalking = true; // Patrolling moduna geri dön
        }
    }
}
