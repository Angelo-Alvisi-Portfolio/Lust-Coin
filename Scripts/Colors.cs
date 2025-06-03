using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Colors", menuName = "Scriptable Objects/Colors")]
public class Colors : ScriptableObject {
    [SerializeField]
    private Color basicDamageColor;
    [SerializeField]
    private Color critColor, dawnLightColor, dayLightColor, twilightLightColor, nightLightColor, fontColor1, fontColor2;
    public Color CritColor { get { return critColor; } }
    public Color DawnLightColor { get { return dawnLightColor; } }
    public Color TwilightColor { get { return twilightLightColor; } }
    public Color NightLightColor { get { return nightLightColor; } }
    public Color DayLightColor { get { return dayLightColor; } }
    public Color FontColor1 => fontColor1;
    public Color FontColor2 => fontColor2;
}
