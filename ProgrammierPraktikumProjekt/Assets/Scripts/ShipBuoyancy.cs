using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

/// <summary>
/// Sorgt dafür, dass das Schiff sich an die Oberfläche einer HDRP-Wasserfläche anpasst.
/// Nutzt die WaterSurface API von Unity HDRP für realistische Auf- und Abbewegung.
/// </summary>
/// <remarks> der Code wurde aus der Vorlesung uebernommen </remarks>

public class ShipBuoyancy : MonoBehaviour
{
    public WaterSurface targetSurface = null;

    // Internal search params
    WaterSearchParameters searchParameters = new WaterSearchParameters();
    WaterSearchResult searchResult = new WaterSearchResult();

    private void Update()
    {
        if (targetSurface != null)
        {
            // Build the search parameters
            searchParameters.startPositionWS = searchResult.candidateLocationWS;
            searchParameters.targetPositionWS = gameObject.transform.position;
            searchParameters.error = 0.01f;
            searchParameters.maxIterations = 8;

            // Do the search
            if (targetSurface.ProjectPointOnWaterSurface(searchParameters, out searchResult))
            {
                gameObject.transform.position = searchResult.projectedPositionWS;
            }
        }
    }
}

