using System.Collections.Generic;

namespace TrieStruct
{
    public class Node<T>
    {
        public T Data { get; set; }
        public bool IsWord { get; set; }
        public Dictionary<char, Node<T>> Children { get; set; }

        //конструктор Node (вершины)
        public Node(T data, bool isWord)
        {
            Data = data;
            IsWord = isWord;
            Children = new Dictionary<char, Node<T>>();
        }

        //Поиск нужного потомка по заданной букве
        public bool FindChild(char key) => (Children.ContainsKey(key) ? true : false);

        //Возвращает искомого потомка, если такой имеется. Иначе возвращает саму вершину
        public Node<T> GetChild(char key) => (Children.ContainsKey(key) ? Children[key] : this);

        //Добавление нового потомка в словарь потомков вершины родителя
        public Node<T> AddChild(char key, bool isWord, T data)
        {
            Node<T> child = new Node<T>(data, isWord);
            Children.Add(key, child);
            return child;
        }
    }
}