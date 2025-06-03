using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DecalBackgroundLayer : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[] decalPrefabs;
    [SerializeField]
    private int minDecalsOnScreen = 1, maxDecalsOnScreen = 2, checkForSpawnTicks = 100;
    private int targetDecals;
    private int decalsOnScreen = 0;
    private readonly List<SpriteRenderer> decals = new List<SpriteRenderer>();
    private SpriteRenderer cullingDecal;
    private bool spawning = false;

    private void Awake() {
        if (maxDecalsOnScreen > 10) {
            maxDecalsOnScreen = 10;
        }
        if (minDecalsOnScreen < 0) {
            minDecalsOnScreen = 0;
        }
        targetDecals = Random.Range(minDecalsOnScreen, maxDecalsOnScreen+1);
        while (targetDecals != 1 || targetDecals == 3 || targetDecals == 6 || targetDecals == 9) {
            targetDecals = Random.Range(minDecalsOnScreen, maxDecalsOnScreen + 1);
        }
        float slice = 20f / targetDecals;
        float[] startingPoints = new float[targetDecals];
        float startingPoint = 10f;
        for (int i = 0; i < startingPoints.Length; i++) {
            startingPoints[i] = startingPoint - slice;
            startingPoint -= slice;
        }
        if (targetDecals > decalsOnScreen) {
            for (int i = 0; i < targetDecals; i++) {
                int r = Random.Range((int)startingPoints[i]*16, (int) ((slice/2 + startingPoints[i])*16) +1);
                //float pos = Random.Range(startingPoints[characterNumber], slice/2 + startingPoints[characterNumber]);
                float pos = r * 0.0625f;
                int dN = Random.Range(0, decalPrefabs.Length);
                SpriteRenderer decal = Instantiate(decalPrefabs[dN], this.transform);
                decal.transform.position = new Vector3(pos, decal.transform.position.y, 0);
                decals.Add(decal);
            }
        }
    }

    public void MoveRight(int speed, int tick) {
        if (!spawning) {
            if (tick % checkForSpawnTicks == 0) {
                targetDecals = Random.Range(minDecalsOnScreen, maxDecalsOnScreen + 1);
                if (targetDecals > decals.Count) {
                    int dN = Random.Range(0, decalPrefabs.Length);
                    SpriteRenderer decal = Instantiate(decalPrefabs[dN], transform);
                    Vector3 maxBounds = decal.localBounds.max;
                    decal.transform.position = new Vector3(-10f - maxBounds.x, decal.transform.position.y, 0);
                    decals.Add(decal);
                    spawning = true;
                }
            }
        }
        for (int i = 0; i < decals.Count; i++) {
            decals[i].transform.Translate(0.0625f * speed, 0, 0);                                    
            if (decals[i].transform.position.x > 10) {
                cullingDecal = decals[i];
                decals.Remove(decals[i]);                
            }
            if (decals.Count != 0 && decals[decals.Count-1].transform.position.x > -10) {
                spawning = false;
            }
        }
        if (cullingDecal != null) {
            cullingDecal.transform.Translate(0.0625f * speed, 0, 0);
            Vector3 minBounds = cullingDecal.bounds.min;
            if (minBounds.x > 10) {
                Destroy(cullingDecal.gameObject);
                cullingDecal = null;
            }
        }
    }

}
