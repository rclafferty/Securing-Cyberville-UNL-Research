using UnityEngine;

namespace Assets.Scripts.Testing
{
    public class Car
    {
        Rigidbody rb;

        Light left_tail;
        Light right_tail;

        public float power;
        public float power_cap;
        float turn_cap;
        float vel_limit;
        
        float base_power;

        Vector3 stationary;
        [SerializeField]
        bool isActive;
        

        public Car(GameObject car, float init, float cap, float velocity_limit)
        {
            Initialize(car, init, cap, velocity_limit);
            stationary = GameObject.Find("SpawnPoint1").transform.position;
        }

        public Car(GameObject car, float init, float cap, float velocity_limit, Vector3 s)
        {
            Initialize(car, init, cap, velocity_limit);
            stationary = s;
        }

        void Initialize(GameObject car, float init, float cap, float velocity_limit)
        {
            rb = car.GetComponent<Rigidbody>();
            base_power = init;
            power = init;
            power_cap = cap;
            vel_limit = velocity_limit;
            left_tail = GameObject.Find("Left Tail Light").GetComponent<Light>();
            if (left_tail == null) Debug.Log("LIGHTS DONT WORK");
            right_tail = GameObject.Find("Right Tail Light").GetComponent<Light>();
            Inactive();
        }

        public void tailLightIntensity(float f)
        {
            left_tail.intensity = f;
            right_tail.intensity = f;
        }

        public void Accelerate(Vector3 f)
        {
            if (!isActive) return;

            if (rb.velocity.z < vel_limit)
            {
                power = power * 1.15f;
                if (power > power_cap)
                {
                    power = power_cap;
                }
                
                rb.AddRelativeForce(f * power);
            }

            left_tail.intensity = 0;
            right_tail.intensity = 0;
        }

        public void Turn(Vector3 f)
        {
            if (!isActive) return;

            if (rb.angularVelocity.y < 3f)
                rb.AddRelativeTorque(f * power);

            tailLightIntensity(0f);
        }

        public void Decelerate()
        {
            if (!isActive) return;

            if (power > base_power)
                power = power * 0.7f;
            else
                power = base_power;

            tailLightIntensity(1f);
        }

        public void SetPosition(Vector3 p)
        {
            rb.MovePosition(p);
        }

        public void Reset()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            SetRotation(Vector3.zero);
        }

        public void StopMotion()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public Rigidbody GetRigidBody()
        {
            return rb;
        }

        public Vector3 GetPosition()
        {
            return rb.position;
        }

        public void Inactive()
        {
            Pause(true);
            //SetPosition(stationary);
        }

        public void Pause(bool tf)
        {
            isActive = !tf;
            rb.useGravity = !tf;
        }

        public void Active()
        {
            Pause(false);
        }

        public void SetStationary (Vector3 s)
        {
            stationary = s;
        }

        public void SetRotation(Vector3 r)
        {
            rb.rotation = Quaternion.Euler(r);
        }

        public void ApplyForce(Vector3 f)
        {
            rb.AddRelativeForce(f);
        }

        public void ApplyAbsForce(Vector3 f)
        {
            rb.AddForce(f);
        }
    }
}
