namespace StorageIndexer.Index.Btree;

public class BTreeNode<T> where T : IComparable<T> {
    public List<T> Keys { get; set; }
    public List<BTreeNode<T>> Children { get; set; }
    public bool IsLeaf { get; set; }
    public BTreeNode(bool isLeaf) {
        Keys = new List<T>();
        Children = new List<BTreeNode<T>>();
        IsLeaf = isLeaf;
    }
}

public class BTree<T> where T : IComparable<T> {
    private int _degree;
    private BTreeNode<T> _root;

    public BTreeNode<T> Root {
        get => _root;
        set => _root = value;
    }

    public BTree(int degree) {
        _degree = degree;
        _root = new BTreeNode<T>(true);
    }

    public bool Search(T key) => InternalSearch(_root, key);
    public void Insert(T key) {
        if (_root.Keys.Count == (2 * _degree) - 1) {
            var node = new BTreeNode<T>(false);
            node.Children.Add(_root);
            SplitChild(node, 0);
            _root = node;
        }

        InsertNonFull(_root, key);
        Console.WriteLine(_root);
    }

    private void SplitChild(BTreeNode<T> parent, int index) {
        var child = parent.Children[index];
        var newNode = new BTreeNode<T>(child.IsLeaf);

        parent.Keys.Insert(index, child.Keys[_degree - 1]);
        parent.Children.Insert(index + 1, newNode);

        newNode.Keys.AddRange(child.Keys.GetRange(_degree, _degree - 1));
        if (!child.IsLeaf) {
            newNode.Children.AddRange(child.Children.GetRange(_degree, _degree));
            child.Children.RemoveRange(_degree, _degree);
        }

        child.Keys.RemoveRange(_degree - 1, _degree);
    }

    private void InsertNonFull(BTreeNode<T> node, T key) {
        int i = node.Keys.Count - 1;
        if (node.IsLeaf) {
            while (i >= 0 && key.CompareTo(node.Keys[i]) < 0) {
                i--;
            }
            node.Keys.Insert(i + 1, key);
        }
        else {
            while (i >= 0 && key.CompareTo(node.Keys[i]) < 0) {
                i--;
            }
            i++;
            if (node.Children[i].Keys.Count == (2 * _degree) - 1) {
                SplitChild(node, i);

                if (key.CompareTo(node.Keys[i]) > 0) {
                    i++;
                }
            }

            InsertNonFull(node.Children[i], key);
        }
    }

    private bool InternalSearch(BTreeNode<T> node, T key) {
        int i = 0;
        while (i < node.Keys.Count && key.CompareTo(node.Keys[i]) > 0) {
            i++;
        }

        if (i < node.Keys.Count && key.CompareTo(node.Keys[i]) == 0) {
            return true;
        }

        if (node.IsLeaf) {
            return false;
        }

        return InternalSearch(node.Children[i], key);
    }



        public BTreeNode<T> FindNode(T key) {
        int i = 0;
        
        var node = _root;
        LoopCycle:
        while (i < node.Keys.Count && key.CompareTo(node.Keys[i]) > 0) {
            i++;
        }

        if (i < node.Keys.Count && key.CompareTo(node.Keys[i]) == 0) {
            return node;
        }

        if (node.IsLeaf) {
            return null;
        }

        node = node.Children[i];
        i = 0; 
        goto LoopCycle;
    }

        public T Find(T element) {
            var node = FindNode(element);
            return node.Keys[0]; 
        }
}