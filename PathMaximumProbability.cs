namespace PathMaximumProbability
{
	internal class PathMaximumProbability
	{
		private class Node
		{
			public int vertex;
			public double maxProbability;

			public Node(int vertex, double maxProbability)
			{
				this.vertex = vertex;
				this.maxProbability = maxProbability;
			}
		}

		private class MaxHeap
		{
			private readonly List<Node> heap;
			private readonly Dictionary<int, int> map;

			private void Swap(List<Node> arr, int i, int j)
			{
				map[arr[i].vertex] = j;
				map[arr[j].vertex] = i;
				(arr[i], arr[j]) = (arr[j], arr[i]);
			}

			private void SiftDown(List<Node> arr, int curIdx, int endIdx)
			{
				int childOneIdx = curIdx * 2 + 1;
				while (childOneIdx <= endIdx)
				{
					int swapIdx = childOneIdx;
					int childTwoIdx = curIdx * 2 + 2;
					if (childTwoIdx <= endIdx && arr[childTwoIdx].maxProbability > arr[childOneIdx].maxProbability)
					{
						swapIdx = childTwoIdx;
					}
					if (arr[swapIdx].maxProbability > arr[curIdx].maxProbability)
					{
						Swap(arr, curIdx, swapIdx);
						curIdx = swapIdx;
						childOneIdx = curIdx * 2 + 1;
					}
					else
					{
						return;
					}
				}
			}

			private void SiftUp(List<Node> arr, int curIdx)
			{
				int parentIdx = (curIdx - 1) / 2;
				while (parentIdx >= 0 && arr[parentIdx].maxProbability < arr[curIdx].maxProbability)
				{
					Swap(arr, parentIdx, curIdx);
					curIdx = parentIdx;
					parentIdx = (curIdx - 1) / 2;
				}
			}

			private List<Node> BuildHeap(List<Node> arr)
			{
				int parentIdx = (arr.Count - 2) / 2;
				for (int i = parentIdx; i >= 0; --i)
				{
					SiftDown(arr, i, arr.Count - 1);
				}
				return arr;
			}

			public MaxHeap(double[] distances)
			{
				List<Node> arr = new();
				map = new Dictionary<int, int>();
				for (int i = 0; i < distances.Length; ++i)
				{
					arr.Add(new Node(i, distances[i]));
					map.Add(i, i);
				}
				heap = BuildHeap(arr);
			}

			public void Push(int vertex, double maxProbability)
			{
				heap.Add(new Node(vertex, maxProbability));
				SiftUp(heap, heap.Count - 1);
			}

			public Node Pop()
			{
				Swap(heap, 0, heap.Count - 1);
				Node removedNode = heap[^1];
				heap.RemoveAt(heap.Count - 1);
				map.Remove(removedNode.vertex);
				SiftDown(heap, 0, heap.Count - 1);
				return removedNode;
			}

			public void Update(int i, double maxProbability)
			{
				if (map.ContainsKey(i))
				{
					heap[map[i]].vertex = i;
					heap[map[i]].maxProbability = maxProbability;
					SiftUp(heap, map[i]);
				}
			}

			public bool IsEmpty()
			{
				return heap.Count == 0;
			}
		}

		private double[] DijkstraAlgorithm(int n, int start, Dictionary<int, List<Node>> graph)
		{
			double[] probabilities = new double[n];
			Array.Fill(probabilities, double.MinValue);
			probabilities[start] = 0;
			MaxHeap maxHeap = new(probabilities);
			while (!maxHeap.IsEmpty())
			{
				Node maxNode = maxHeap.Pop();
				if (maxNode.maxProbability == double.MinValue)
				{
					break;
				}
				if (graph.ContainsKey(maxNode.vertex))
				{
					foreach (Node node in graph[maxNode.vertex])
					{
						double newMaxProbability = node.maxProbability * (maxNode.maxProbability == 0 ? 1 : maxNode.maxProbability);
						double maxProbability = probabilities[node.vertex];
						if (newMaxProbability > maxProbability)
						{
							probabilities[node.vertex] = newMaxProbability;
							maxHeap.Update(node.vertex, newMaxProbability);
						}
					}
				}
			}
			return probabilities;
		}

		public double MaxProbability(int n, int[][] edges, double[] succProb, int start, int end)
		{
			Dictionary<int, List<Node>> graph = new();
			for (int i = 0; i < edges.Length; ++i)
			{
				if (!graph.ContainsKey(edges[i][0]))
				{
					graph.Add(edges[i][0], new List<Node>());
				}
				graph[edges[i][0]].Add(new Node(edges[i][1], succProb[i]));
				if (!graph.ContainsKey(edges[i][1]))
				{
					graph.Add(edges[i][1], new List<Node>());
				}
				graph[edges[i][1]].Add(new Node(edges[i][0], succProb[i]));
			}
			double[] probabilities = DijkstraAlgorithm(n, start, graph);
			return probabilities[end] == double.MinValue ? 0 : probabilities[end];
		}
	}
}
