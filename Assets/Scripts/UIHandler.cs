using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Tacometro")]
    [SerializeField] Image tacoTrail;
    [SerializeField] RectTransform tacoNeedle;

    [Header("Velocimetro")]
    [SerializeField] Image veloTrail;
    [SerializeField] RectTransform veloNeedle;
    [SerializeField] TMP_Text currentVeloText;

    private AnimationCurve curve;
    private float expectedAngle;
    private float tLerp;

    void Start()
    {
        SetCurve();
    }

    private void SetCurve()
    {
        curve = new AnimationCurve();

        //Keys -> velocity, expected angle
        curve.AddKey(0, 0);
        curve.AddKey(60, -90);
        curve.AddKey(180, -197);
        curve.AddKey(300, -270);
    }

    private void OnEnable()
    {
        VelocityManager.OnVelocityChange += UpdateUI;
    }

    private void OnDisable()
    {
        VelocityManager.OnVelocityChange -= UpdateUI;
    }

    private void UpdateUI(float velocity)
    {
        currentVeloText.text = Mathf.Round(velocity).ToString();

        expectedAngle = curve.Evaluate(velocity); ;
        tLerp = expectedAngle / AppConstants.MAX_ANGLE;

        veloTrail.fillAmount = Mathf.Lerp(0, 0.75f, tLerp);
        tacoTrail.fillAmount = Mathf.Lerp(0.11f, 0.88f, tLerp);

        veloNeedle.localEulerAngles = new Vector3(0, 0, expectedAngle);
        tacoNeedle.localEulerAngles = new Vector3(0, 0,
            Mathf.Lerp(AppConstants.MIN_TACO_ANGLE, AppConstants.MAX_ANGLE, tLerp));
    }
}
