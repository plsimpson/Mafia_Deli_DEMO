using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletTrails : MonoBehaviour
    {
        [SerializeField] private LineRenderer lr;
        public Transform bulletTrailOrigin;

        private List<LineRenderer> lineRenderers = new List<LineRenderer>();

        private void Update()
        {
            for (int i = lineRenderers.Count - 1; i >= 0; i--)
            {
                LineRenderer lr = lineRenderers[i];

                if (lr == null)
                {
                    lineRenderers.RemoveAt(i);
                    continue;
                }

                Gradient gradient = lr.colorGradient;
                GradientAlphaKey[] alphaKeys = gradient.alphaKeys;

                // Reduce all alpha keys over time
                for (int j = 0; j < alphaKeys.Length; j++)
                {
                    alphaKeys[j].alpha -= Time.deltaTime * 5f;
                }

                gradient.alphaKeys = alphaKeys;
                lr.colorGradient = gradient;

                // If fully invisible, destroy
                if (alphaKeys[0].alpha <= 0f && alphaKeys[alphaKeys.Length - 1].alpha <= 0f)
                {
                    Destroy(lr.gameObject);
                    lineRenderers.RemoveAt(i);
                }
            }
        }

        public void CreateTrail(Vector3 start, Vector3 end)
        {
            LineRenderer newLR = Instantiate(lr, transform.position, Quaternion.identity);

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
            new GradientColorKey(Color.red, 0f),
            new GradientColorKey(Color.orange, 1f)
                },
                new GradientAlphaKey[]
                {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(0f, 1f)
                }
            );

            newLR.colorGradient = gradient;

            newLR.SetPositions(new Vector3[]
            {
        start,
        end
            });

            lineRenderers.Add(newLR);
        }
    }
}