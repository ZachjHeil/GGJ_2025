using System.Collections;
using UnityEngine;

public class Puzzle_SecondPuzzle : MonoBehaviour
{
    InputSystem_Actions controls;

    bool curMovingPick;
    bool curAttemptingUnlock;

    public Transform lockPick;
    public Transform knife;
    public AudioSource audioSource;
    public AudioClip unlockSound;
    public AudioClip failedSound;
    InputManager input;

    public MapPickup mapHandler;

    public float desiredRot;
    int[] possibleAngles = { -45, -30, -15, 0, 15, 30, 45, 60, 75, 90, 105, 120, 135 };

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void Start()
    {
        input = InputManager.Instance;
    }

    private void OnEnable()
    {
        controls.Enable();
        curMovingPick = false;
        desiredRot = possibleAngles[Random.Range(0, possibleAngles.Length)];

    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (curAttemptingUnlock)
        {
            return;
        }

            if (input.PlayerCancel() || input.PlayerDashed())
            {
                this.transform.parent.gameObject.SetActive(false);
                input.isInPuzzle = false;
            }

        else
        {
            if (GetUnlock())
            {
                curAttemptingUnlock = true;
                StartCoroutine(MoveKnife());
            }
        }

        if (curMovingPick == false)
        {
            Vector2 movement = GetMovement();
            if (movement.x < -0.5)
            {
                curMovingPick = true;
                StartCoroutine(MovePick(false));
            }
            else if (movement.x > 0.5)
            {
                curMovingPick = true;
                StartCoroutine(MovePick(true));
            }
        }
    }

    IEnumerator MovePick(bool leftRight)
    {
        Vector3 rot = lockPick.rotation.eulerAngles + new Vector3(0, 0, leftRight ? -15 : +15); //use local if your char is not always oriented Vector3.up
        rot.z = ClampAngle(rot.z, -45f, 135f);

        lockPick.eulerAngles = rot;

        yield return new WaitForSeconds(0.1f);
        curMovingPick = false;
    }

    IEnumerator MoveKnife()
    {
        Quaternion startRot = knife.rotation;
        Quaternion DesiredRotation = Quaternion.identity;
        int goalRot = Mathf.RoundToInt(desiredRot);
        int lockRot = Mathf.RoundToInt(lockPick.rotation.eulerAngles.z);
        if (goalRot <= 0) { goalRot += 360; }
        if (lockRot <= 0) { lockRot += 360; }
        float movementPercentage = (float)goalRot / (float)lockRot;
        movementPercentage = Mathf.Clamp(movementPercentage, 0.25f, 1.25f);
        DesiredRotation.eulerAngles = new Vector3(0,0,-90 * movementPercentage);

        bool showingMovement = true;
        float curTime = 0;

        while (showingMovement)
        {
            knife.rotation = Quaternion.Lerp(startRot, DesiredRotation, curTime / 1.5f);

            if (knife.eulerAngles.z == DesiredRotation.eulerAngles.z) { showingMovement = false; }

            curTime += Time.deltaTime;
            yield return null;
        }

        if(goalRot == lockRot)
        {
            audioSource.clip = unlockSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = failedSound;
            audioSource.Play();
        }


        showingMovement = true;
        startRot = knife.rotation;
        curTime = 0;
        while (showingMovement)
        {
            knife.rotation = Quaternion.Lerp(startRot, Quaternion.identity, curTime / 1.5f);
            
            if (knife.rotation.z == 0) { showingMovement = false; }

            curTime += Time.deltaTime;
            yield return null;
        }


        if (goalRot == lockRot)
        {
            TriggerPuzzleComplete();
        }
        curAttemptingUnlock = false;
    }

    public void TriggerPuzzleComplete()
    {
        //ADD PUZZLE COMPLETE CODE HERE

        mapHandler.curStep++;
        mapHandler.TriggerItemInteract();
        input.isInPuzzle = false;
        this.transform.parent.gameObject.SetActive(false);
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    public Vector2 GetMovement()
    {
        return controls.UI.Navigate.ReadValue<Vector2>();
    }

    public bool GetUnlock()
    {
        return (controls.UI.Submit.triggered || controls.UI.Click.triggered);
    }
}