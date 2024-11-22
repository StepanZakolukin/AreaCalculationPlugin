namespace AreaCalculationPlugin.View;

internal static class TreeNodeExtension
{
    public static void CheckAllChildNodes(this TreeNode treeNode, bool nodeChecked)
    {
        if (treeNode.Nodes.Count == 0)
            return;

        foreach (TreeNode node in treeNode.Nodes)
        {
            node.Checked = nodeChecked;
            if (node.Nodes.Count > 0) 
                CheckAllChildNodes(node, nodeChecked);
        }
    }
}
