using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

/// Общий менеджмент большинства скриптов
public class Management : GlobalFunctions
{
    public Data data;
    public GameUI gameUI;
    public Power ppl;
    public GameObject player;
    public GameObject mathPlayer;
    public GameObject mathPlayer1;
    public GameObject mathPlayer2;

    public bool isShotMode = false;
    public JoyStick joyStick;
    public GameObject joystick;
    public float maxSpeed = 5f;

    public bool isReduced = false;
    public bool isLeft = false;
    public bool isRight = false;
    public Vector3 offset = new Vector3(0f, 0f, -0.001f);
    public float speed = 5f;
    public float gravity = 5f;
    public float timeslow = 0.2f;
    private bool isDead = false;
    public GameObject[] leftovers;
    public Vector2 pkb;
    private float massEnd = 2f;
    private float speedAnim = 0.05f;
    private bool activeAnimation = false;
    private bool animationActive = false;
    private Transform cam, pl, mt, mt1, mt2;
    private Rigidbody2D plrb;
    public HealthBar healthBar;
    
    public PostProcessingProfile special;
    public PostProcessingBehaviour ppb;
    public bool isMakeGraphics = true;


    private void Awake()
    {
        GraphicsSettings.renderPipelineAsset = null;
        massEnd = gravity;
        float c = PlayerPrefs.GetFloat("senrot") / 2f + 0.65f;
        speed *= c;
        animationActive = PlayerPrefs.GetString("graphicsquality") == "high";
        cam = gameObject.transform;
        pl = player.transform;
        plrb = player.GetComponent<Rigidbody2D>();
        mt = mathPlayer.transform;
        mt1 = mathPlayer1.transform;
        mt2 = mathPlayer2.transform;
        isMakeGraphics = PlayerPrefs.GetString("graphicsquality") != "low";
        IsShotMode(PlayerPrefs.GetString("isshotmode") == "True");
        StartGraphics();
    }

    public void Set()
    {
        mt1.localEulerAngles = new Vector3(0f, 0f, PlayerPrefs.GetFloat("rotatenextlevel"));
    }

    public void Set(float f)
    {
        mt1.localEulerAngles = new Vector3(0f, 0f, f);
        mt.localEulerAngles = mt1.localEulerAngles;
        cam.localEulerAngles = mt.localEulerAngles;
    }

    private void Update()
    {
        if (activeAnimation == true)
        {
            if (gravity != massEnd)
            {
                gravity = MoveToward(gravity, massEnd, speed, new Vector2(-100f, 100f));
            }
            else
            {
                activeAnimation = false;
            }
        }

        if (isDead == false)
        {
            if (isShotMode == false)
            {
                bool isLeftGamepad = false;
                bool isRightGamepad = false;

                if (isLeft == false)
                {
                    if (Input.GetAxis("JoyXx") < 0f)
                    {
                        isLeft = isLeftGamepad = true;
                    }
                    else if (Input.GetAxis("JoyYy") < 0f)
                    {
                        isLeft = isLeftGamepad = true;
                    }
                }

                if (isRight == false)
                {
                    if (Input.GetAxis("JoyXx") > 0f)
                    {
                        isRight = isRightGamepad = true;
                    }
                    else if (Input.GetAxis("JoyYy") > 0f)
                    {
                        isRight = isRightGamepad = true;
                    }
                }

                if (isLeft == false && isRight == true)
                {
                    Vector3 p = mt1.localEulerAngles;
                    mt1.localEulerAngles = new Vector3(p.x, p.y, p.z + speed * (Time.deltaTime / 0.03f));
                }
                else if (isLeft == true && isRight == false)
                {
                    Vector3 p = mt1.localEulerAngles;
                    mt1.localEulerAngles = new Vector3(p.x, p.y, p.z - speed * (Time.deltaTime / 0.03f));
                }

                if (isLeftGamepad == true)
                {
                    isLeftGamepad = isLeft = false;
                }
                if (isRightGamepad == true)
                {
                    isRightGamepad = isRight = false;
                }

                mt.localEulerAngles = mt1.localEulerAngles;
                mt.position = pl.position;
                Physics2D.gravity = (mt2.position - pl.position) * gravity;
                float edit = Mathf.Abs(Stable(transform.localEulerAngles.z, -180f, 180f) / 180f);
                pkb = new Vector2(edit, -0.5f + edit / 2f);
                if (isReduced == false)
                {
                    cam.position = mt.position;
                }
                else
                {
                    float pow = Vector2.Distance(cam.position, mt.position) / 2f;
                    cam.position = Vector2.MoveTowards(cam.position, mt.position, pow);
                }
                cam.localEulerAngles = mt.localEulerAngles;
            }
            else
            {
                if (isReduced == false)
                {
                    cam.position = pl.position;
                }
                else
                {
                    float pow = Vector2.Distance(cam.position, mt.position) / 2f;
                    cam.position = Vector2.MoveTowards(cam.position, mt.position, pow);
                }
                Physics2D.gravity = RotateVector(joyStick.Joy() * maxSpeed, mt.localEulerAngles.z);
            }
        }
    }

    public void SetGravity(bool isSet, float setGravity, float plusGravity, float speed)
    {
        if (animationActive)
        {
            speedAnim = speed;
            if (isSet == true)
            {
                massEnd = setGravity;
            }
            else
            {
                massEnd = Stable2(massEnd + plusGravity, -100f, 100f);
            }

            if (massEnd == 0)
            {
                plrb.angularDrag = 0f;
                plrb.drag = 0f;
                joystick.SetActive(false);
                joyStick.ResetJoystick();
            }
            else
            {
                if (isShotMode)
                {
                    plrb.angularDrag = 1f;
                    plrb.drag = 1f;
                    joystick.SetActive(true);
                }
                else
                {
                    plrb.angularDrag = 0f;
                    plrb.drag = 0.05f;
                }
            }
            activeAnimation = true;
        }
        else
        {
            if (isSet == true)
            {
                gravity = setGravity;
            }
            else
            {
                gravity = Stable2(gravity + plusGravity, -100f, 100f);
            }

            if (gravity == 0)
            {
                plrb.angularDrag = 0f;
                plrb.drag = 0f;
                joystick.SetActive(false);
                joyStick.ResetJoystick();
            }
            else
            {
                if (isShotMode)
                {
                    plrb.angularDrag = 1f;
                    plrb.drag = 1f;
                    joystick.SetActive(true);
                }
                else
                {
                    plrb.angularDrag = 0f;
                    plrb.drag = 0.05f;
                }
            }
        }
    }

    public void SetGravity(float inp)
    { gravity += inp; }

    public void Dead(string typeDead)
    {
        if (isDead == false)
        {
            isDead = true;
            StartGraphics();
            if (isShotMode) { Physics2D.gravity = Vector2.zero; }
            joystick.SetActive(false);
            PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths") + 1);
            leftovers = player.GetComponent<Player>().Dead();
            gameUI.Dead(data.GetDeadPhrase(typeDead), data.GetDeadPhrase2());
        }
    }

    public void DownLeft()
    {
        isLeft = true;
    }

    public void UpLeft()
    {
        isLeft = false;
    }

    public void DownRight()
    {
        isRight = true;
    }

    public void UpRight()
    {
        isRight = false;
    }

    public void StartGraphics()
    {
        if (isMakeGraphics)
        {
            ppb.profile = null;
            ppb.enabled = false;
        }
        return;
    }

    public void SpecialGraphics()
    {
        if (isMakeGraphics)
        {
            ppb.profile = special;
            ppb.enabled = true;
        }
        return;
    }

    public void IsShotMode(bool isShotModeLocal)
    {
        PlayerPrefs.SetString("isshotmode", isShotModeLocal.ToString());
        if (isShotModeLocal)
        {
            plrb.angularDrag = 1f;
            plrb.drag = 1f;
            Physics2D.gravity = Vector2.zero;
        }
        else
        {
            plrb.angularDrag = 0f;
            plrb.drag = 0.05f;
            joyStick.ResetJoystick();
            mt1.localEulerAngles = new Vector3(0f, 0f, cam.localEulerAngles.z);
        }
        joystick.SetActive(isShotModeLocal);
        isShotMode = isShotModeLocal;
        return;
    }

    public Vector2 RotateVector(Vector2 a, float offsetAngle)
    {
        float power = Mathf.Sqrt(a.x * a.x + a.y * a.y);
        float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90f + offsetAngle;
        return Quaternion.Euler(0, 0, angle) * Vector2.up * power;
    }
}