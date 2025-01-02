namespace AreaCalculationPlugin.View.Extensions;

internal static class TreeViewExtension
{
    public static void AddNode(this TreeView treeView, string text, object tag)
    {
        var roomNode = new TreeNode(text)
        {
            Tag = tag,
        };
        treeView.Nodes.Add(roomNode);
    }

    public static void MarkAll(this TreeView tree, bool nodeChecked)
    {
        foreach (TreeNode treeNode in tree.Nodes)
        {
            treeNode.Checked = nodeChecked;
            treeNode.CheckAllChildNodes(nodeChecked);
        }
    }
}
