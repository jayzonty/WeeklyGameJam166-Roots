using UnityEngine;
using UnityEngine.UI;

namespace WGJRoots
{
    public class InfoPanelBehavior : MonoBehaviour
    {
        public Text growthLevelText;

        public Text nutrientPointsText;

        public Text branchPointsText;

        public GameState gameState;

        private void Update()
        {
            if (growthLevelText != null)
            {
                growthLevelText.text = "Growth Level: " + gameState.growthLevel;
            }

            if (nutrientPointsText != null)
            {
                nutrientPointsText.text = "Nutrient Points: " + gameState.NutrientPoints + "/" + gameState.NutrientPointsToNextGrowthLevel;
            }

            if (branchPointsText != null)
            {
                branchPointsText.text = "Branch Points: " + gameState.BranchPoints;
            }
        }
    }
}
