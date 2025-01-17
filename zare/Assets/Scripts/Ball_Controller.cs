using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public float moveSpeed = 10f; // سرعت حرکت توپ
    public float throwForce = 15f; // نیروی پرتاب توپ
    public int scorePerPin = 10; // امتیاز هر پین
    private Rigidbody rb; // Rigidbody توپ
    private bool isThrown = false; // بررسی پرتاب شدن توپ

    void Start()
    {
        // گرفتن Rigidbody توپ
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // حرکت توپ تا زمانی که پرتاب نشده است
        if (!isThrown)
        {
            HandleMovement();
        }

        // پرتاب توپ با کلید Space
        if (Input.GetKeyDown(KeyCode.Space) && !isThrown)
        {
            ThrowBall();
        }
    }

    private void HandleMovement()
    {
        // دریافت ورودی‌های کیبورد
        float horizontal = Input.GetAxis("Horizontal"); // چپ و راست
        float vertical = Input.GetAxis("Vertical"); // جلو و عقب

        // محاسبه جهت حرکت
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // حرکت دادن توپ
        transform.Translate(moveDirection);
    }

    private void ThrowBall()
    {
        // اعمال نیروی پرتاب به توپ
        rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
        isThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // برخورد توپ با پین‌ها
        if (collision.gameObject.CompareTag("Pin"))
        {
            // امتیاز اضافه کن
            GameManager.Instance.AddScore(scorePerPin);

            // حذف پین از بازی
            Destroy(collision.gameObject);

            Debug.Log("Hit a pin! Score: " + scorePerPin);
        }
    }
}
