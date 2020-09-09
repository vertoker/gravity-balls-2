using UnityEngine;

/// Правильное расположение UI при разных экранах (игра)
public class AspectRatio : MonoBehaviour
{
    public Camera cam;
    public RectTransform pause;
    public RectTransform pauseWindow;
    public RectTransform healthbar;
    public RectTransform tipsGamePlay;
    public RectTransform joystick;
    [Header("LoseWindow")]
    public RectTransform homeBut;
    public RectTransform repeatBut;
    [Header("Tips")]
    public RectTransform textTips;

    public void Awake()
    {
        /// Расчёт сдвига UI на экране
        float ratio = cam.aspect; float ratioBase = 16f / 9f;
        Vector2 vx = new Vector2(ratioBase - ratio, ratio - ratioBase) * 540f;
        /// Сдвиг: vx.x - влево, vx.y - вправо

        /// Исполнение смещения
        pause.localPosition += new Vector3(vx.x, 0, 0);
        healthbar.localPosition += new Vector3(vx.y, 0, 0);
        pauseWindow.localPosition += new Vector3(vx.x, 0, 0);
        tipsGamePlay.localPosition += new Vector3(vx.x - 5f, 0, 0);
        homeBut.localPosition += new Vector3(vx.x, 0, 0);
        joystick.localPosition += new Vector3(vx.y, 0, 0);
        repeatBut.localPosition += new Vector3(vx.y, 0, 0);
        textTips.sizeDelta += new Vector2(10f * vx.y, 0);
    }
}
