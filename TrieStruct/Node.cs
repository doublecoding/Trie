using System.Collections.Generic;

namespace TrieStruct
{
    public class Node<T>
    {
        public T Key { get; set; }
        public string Word { get; set; }
        public bool IsWord { get; set; }
        public Dictionary<char, Node<T>> Children { get; set; }
        public int CountChildren { get { return Children.Count; } }

        public Node(T key, bool isWord)
        {
            Key = key;
            Word = "";
            IsWord = isWord;
            Children = new Dictionary<char, Node<T>>();
        }

        public bool FindChild(char key) => (Children.ContainsKey(key) ? true : false);

        public Node<T> GetChild(char key) => (Children.ContainsKey(key) ? Children[key] : this);

        public Node<T> AddChild(char key, bool isWord, T data)
        {
            Node<T> child = new Node<T>(data, isWord);
            Children.Add(key, child);
            return child;
        }
    }
}