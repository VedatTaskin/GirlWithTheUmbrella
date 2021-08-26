using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed, rotationSpeedX, rotationSpeedZ;
    float startSpeed;

    [SerializeField]
    Transform transformX, transformZ;

    [SerializeField]
    float xBelowLimit, xUpLimit, zBelowLimit, zUpLimit, umbrellaFixSpeed;

    Vector3 clickedPosition;
    float clickedRotationX, clickedRotationZ;

    Animator anim;

    [SerializeField]
    ParticleSystem particle;

    [SerializeField]
    float speedUpEffectMultiplier, speedUpTime, speedUpBonus;

    [SerializeField]
    bool revertX, revertY;

    int revX, revY;

    [SerializeField]
    GameObject comboTimeobject;

    [SerializeField]
    int comboRequirement;

    int comboCount;

    bool canMove, isSpeedUp;

    void Start()
    {
        canMove = true;
        isSpeedUp = false;
        anim = GetComponent<Animator>();
        startSpeed = speed;
        if (revertX)
            revX = -1;
        else
            revX = 1;
        if (revertY)
            revY = -1;
        else
            revY = 1;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
                Clicked();
            else if (Input.GetMouseButton(0))
                Move();
            else if (Input.GetMouseButtonUp(0))
                UnClicked();
        }
        else
            FixUmbrellaPosition();
        SetSimulationSpeedOfSpeedUpEffect();
    }

    private void SetSimulationSpeedOfSpeedUpEffect()
    {
        if(particle.gameObject.activeInHierarchy)
        {
            var main = particle.main;
            float holder = speed - startSpeed;
            if (holder > 0.1f)
                main.simulationSpeed = (holder / 2 + 1) * speedUpEffectMultiplier;
            else
            {
                particle.gameObject.SetActive(false);
                comboTimeobject.SetActive(false);
            }
        }
        anim.SetFloat("speedMultiplier", speed / startSpeed);
    }

    private void Move()
    {
        anim.SetBool("IsWalk", true);
        transform.position += Vector3.forward * speed * Time.deltaTime;
        UmbrellaMovement();
    }

    private void Clicked()
    {
        clickedPosition = Input.mousePosition;
    }

    private void UnClicked()
    {
        anim.SetBool("IsWalk", false);
        clickedRotationX = transformX.rotation.eulerAngles.x;
        clickedRotationZ = transformZ.rotation.eulerAngles.z;
        if (clickedRotationX > 180)
            clickedRotationX -= 360;
        if (clickedRotationZ > 180)
            clickedRotationZ -= 360;
    }

    private void UmbrellaMovement()
    {
        Vector3 pos = Input.mousePosition - clickedPosition;
        float x, z;
        x = revY * pos.y * rotationSpeedX + clickedRotationX;
        z = revX * (-pos.x) * rotationSpeedZ + clickedRotationZ;
        if (x > xUpLimit)
            x = xUpLimit;
        else if (x < xBelowLimit)
            x = xBelowLimit;
        if (z > zUpLimit)
            z = zUpLimit;
        else if (z < zBelowLimit)
            z = zBelowLimit;
        transformX.rotation = Quaternion.Euler(new Vector3(x, 0, z));
    }

    private void FixUmbrellaPosition()
    {
        float x = transformX.rotation.eulerAngles.x;
        float z = transformZ.rotation.eulerAngles.z;
        if (x > 180)
            x -= 360;
        if (z > 180)
            z -= 360;
        if (x < -2)
            x += Time.deltaTime * umbrellaFixSpeed;
        else if (x > 2)
            x -= Time.deltaTime * umbrellaFixSpeed;
        if (z < -2)
            z += Time.deltaTime * umbrellaFixSpeed;
        else if (z > 2)
            z -= Time.deltaTime * umbrellaFixSpeed;
        transformX.rotation = Quaternion.Euler(new Vector3(x, 0, z));
    }

    internal void SpeedUp()
    {
        comboCount++;
        if (comboCount >= comboRequirement && !isSpeedUp)
        {
            isSpeedUp = true;
            comboCount = 0;
            StartCoroutine(SpeedUpAsync(speedUpBonus));
        }
    }

    IEnumerator SpeedUpAsync(float extraSpeed)
    {
        particle.gameObject.SetActive(true);
        comboTimeobject.SetActive(true);
        for (float i = 0; i <= extraSpeed; i += 1f)
        {
            speed += 1f;
            yield return new WaitForSeconds(speedUpTime * 0.5f / extraSpeed);
        }
        for (float i = 0; i <= extraSpeed; i += 1f)
        {
            speed -= 1f;
            yield return new WaitForSeconds(speedUpTime * 0.5f / extraSpeed);
        }
    }

    internal void SetCanMove(bool state)
    {
        canMove = state;
        StartCoroutine(StopWalkingAsync(state));
    }

    IEnumerator StopWalkingAsync(bool state)
    {
        yield return new WaitForSeconds(0.1f);
        if(!state)
            anim.SetBool("IsWalk", state);
    }
}
