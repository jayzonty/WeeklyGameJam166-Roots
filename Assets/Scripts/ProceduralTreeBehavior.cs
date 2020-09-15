using System.Collections.Generic;

using UnityEngine;

namespace WGJRoots
{
    public class ProceduralTreeBehavior : MonoBehaviour
    {
        public GameState gameState;
        public LevelBehavior levelBehavior;

        public GameObject branchLineRendererPrefab;
        public GameObject leavesPrefab;

        public Transform startingPoint;

        private class Node
        {
            public Node parent = null;
            public int depth = 0;
            public List<Node> children = new List<Node>();
            public List<LineRenderer> childrenBranchLineRenderers = new List<LineRenderer>();
            public bool willGrowFurther = true;

            public Vector3 position;
        }

        private Node rootNode = new Node();

        private void OnEnable()
        {
            gameState.onGrowthLevelIncreased.AddListener(HandleGrowthLevelIncreased);
        }

        private void OnDisable()
        {
            gameState.onGrowthLevelIncreased.RemoveListener(HandleGrowthLevelIncreased);
        }

        private void Start()
        {
            rootNode.position = startingPoint.localPosition;

            transform.position = levelBehavior.TreeStartingPoint;
        }

        private void HandleGrowthLevelIncreased(int newGrowthLevel)
        {
            int branchFromDepth = newGrowthLevel - 1;
            Stack<Node> frontier = new Stack<Node>();
            frontier.Push(rootNode);

            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Pop();
                if (currentNode.depth == branchFromDepth)
                {
                    // Max of 4 branches, minimum of 2
                    int numBranches = Random.Range(2, 5);

                    int numBranchesThatGrowsFurther = Random.Range(1, numBranches);

                    List<int> branchIndicesThatGrowsFurther = new List<int>();
                    for (int i = 0; i < numBranches; ++i)
                    {
                        branchIndicesThatGrowsFurther.Add(i);
                    }
                    Shuffle(branchIndicesThatGrowsFurther);

                    branchIndicesThatGrowsFurther = branchIndicesThatGrowsFurther.GetRange(0, numBranchesThatGrowsFurther);

                    float branchAngle = 30.0f;
                    float branchAngleIncrement = 120.0f / (numBranches - 1);
                    for (int i = 0; i < numBranches; ++i)
                    {
                        Node child = new Node();
                        child.parent = currentNode;
                        child.depth = currentNode.depth + 1;
                        child.willGrowFurther = branchIndicesThatGrowsFurther.Contains(i);

                        float branchLength = Random.Range(1.0f, 1.5f);
                        
                        Vector3 branchDirection = Quaternion.Euler(0.0f, 0.0f, branchAngle) * Vector3.right;
                        child.position = currentNode.position + branchDirection * branchLength;

                        branchAngle += branchAngleIncrement;

                        currentNode.children.Add(child);

                        GameObject lineRendererObj = GameObject.Instantiate(branchLineRendererPrefab, transform);
                        LineRenderer lineRenderer = lineRendererObj.GetComponent<LineRenderer>();
                        lineRenderer.useWorldSpace = false;
                        lineRenderer.SetPosition(0, currentNode.position);
                        lineRenderer.SetPosition(1, child.position);

                        currentNode.childrenBranchLineRenderers.Add(lineRenderer);

                        int numLeaves = Random.Range(3, 5);
                        for (int j = 0; j < numLeaves; ++j)
                        {
                            GameObject leavesObj = GameObject.Instantiate(leavesPrefab, transform);
                            leavesObj.transform.localPosition = child.position + Random.insideUnitSphere * Random.Range(0.2f, 0.3f);
                        }
                    }
                }
                else if (currentNode.willGrowFurther)
                {
                    for (int i = 0; i < currentNode.children.Count; ++i)
                    {
                        frontier.Push(currentNode.children[i]);
                    }
                }
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int targetIndex = Random.Range(0, n);
                --n;

                // Swap
                T temp = list[n];
                list[n] = list[targetIndex];
                list[targetIndex] = temp;
            }
        }
    }
}
