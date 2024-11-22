namespace AreaCalculationPlugin.View.Extensions;

internal static class TreeViewExtension
{
    public static void MarkAll(this TreeView tree, bool nodeChecked)
    {
        foreach (TreeNode treeNode in tree.Nodes)
        {
            treeNode.Checked = nodeChecked;
            treeNode.CheckAllChildNodes(nodeChecked);
        }
    }
}
