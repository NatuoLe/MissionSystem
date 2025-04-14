using System.Collections.Generic;
using NodeCanvas.Framework;

public static class GraphExtensions
{
    /// <summary>
    /// 获取某个节点的所有后续节点（不包括子节点，即直接连接的节点）。
    /// </summary>
    /// <param name="graph">目标图</param>
    /// <param name="startNode">起始节点</param>
    /// <returns>后续节点的列表</returns>
    public static List<Node> GetSubsequentNodes(this Graph graph, Node startNode)
    {
        var visited = new HashSet<Node>(); // 用于记录已访问的节点，避免重复访问
        var result = new List<Node>(); // 存储后续节点

        // 使用队列进行广度优先搜索
        var queue = new Queue<Node>();
        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            // 遍历当前节点的所有输出连接
            foreach (var connection in current.outConnections)
            {
                var targetNode = connection.targetNode;

                // 如果目标节点未被访问过
                if (!visited.Contains(targetNode))
                {
                    visited.Add(targetNode);
                    result.Add(targetNode);

                    // 将目标节点加入队列，继续遍历其后续节点
                    queue.Enqueue(targetNode);
                }
            }
        }

        return result;
    }
    public static List<Node> GetOuterNodes(this Graph graph)
    {
        var outerNodes = new List<Node>();
        // 2. 找到图的出口点（叶子节点）的子节点
        var leafNodes = graph.GetLeafNodes();
        foreach (var leafNode in leafNodes)
        {
            foreach (var connection in leafNode.outConnections)
            {
                outerNodes.Add(connection.targetNode);
            }
        }

        return outerNodes;
    }
    /// <summary>
    /// 获取与当前图直接连接的父节点和下一个节点。
    /// </summary>
    /// <param name="graph">目标图</param>
    /// <returns>一个包含父节点和下一个节点的元组 (父节点列表, 下一个节点列表)</returns>
    public static List<Node> GetConnectedNodes(this Graph graph)
    {
        var nextNodes = new List<Node>();
        
        // 2. 找到连接到当前图出口点的下一个节点
        var leafNodes = graph.GetLeafNodes();
        foreach (var leafNode in leafNodes)
        {
            foreach (var connection in leafNode.outConnections)
            {
                nextNodes.Add(connection.targetNode);
            }
        }

        return nextNodes;
    }
}