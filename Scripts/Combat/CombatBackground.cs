using System.Collections;
using UnityEngine;

public class CombatBackground : MonoBehaviour {
    //speed(characterNumber) = ((H - ix) a) / H 
    //
    //a - Speed of the foreground(screen) layer
    //characterNumber - The layer we are calculating the speed for
    //width - The distance each layer is into the screen from the last(change this to tune the parallax effect)
    //H - The distance to the Horizon(must be larger then the number of layers times width. Change this to tune the parallax effect)

    [SerializeField]
    private int[] distances;
    [SerializeField]
    private int parallaxEffect;
    [SerializeField] 
    private int distanceToHorizon;
    [SerializeField]
    private SpriteRenderer[] spriteRenderers;
    [SerializeField]
    private float foregroundSpeed;
    private float[] speeds;
    private int ticks = 0;
    private float elapsedSeconds = 0f;
    public bool moving = true;
    [SerializeField]
    private DecalBackgroundLayer decalLayer;
    [SerializeField]
    private int decalLayerDistance = 2;

    private void Awake() {
        speeds = new float[spriteRenderers.Length];
        speeds[0] = foregroundSpeed;
        if (decalLayerDistance < 1) {
            decalLayerDistance = 1;
        }
        for ( int i = 1; i < spriteRenderers.Length; i++ ) {
            
            speeds[i] = ((distanceToHorizon - i * parallaxEffect) * foregroundSpeed) / distanceToHorizon;            
        }
    }

    private void Update() {
        if (moving) {
            ticks++;
            for (int i = 0; i < distances.Length; i++) {
                if (ticks % (parallaxEffect * distances[i]) == 0) {
                    spriteRenderers[i].transform.Translate(0.0625f, 0, 0);
                    spriteRenderers[i + 5].transform.Translate(0.0625f, 0, 0);
                    if (spriteRenderers[i + 5].transform.position.x == -0.0625f) {
                        spriteRenderers[i].transform.Translate(-136, 0, 0);
                    }
                    if (spriteRenderers[i].transform.position.x == -0.0625f) {
                        spriteRenderers[i + 5].transform.Translate(-136, 0, 0);
                    }
                }
            }
            if (ticks % decalLayerDistance == 0) {
                decalLayer.MoveRight(1, ticks);
            }
        }
    }
}
