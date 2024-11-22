namespace AreaCalculationPlugin.View.Extensions;

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
                node.CheckAllChildNodes(nodeChecked);
        }
    }
}
