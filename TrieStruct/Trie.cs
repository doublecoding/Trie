using System.Collections.Generic;

namespace TrieStruct
{
    public class Trie<T>
    {
        private Node<T> root = new Node<T>(default(T), false);

        public void AddWord(string word, T data)
        {
            var currentNode = root;
            for (var i = 0; i < word.Length; i++)
            {
                if (!currentNode.FindChild(word[i]))
                {
                    if (i != word.Length - 1)
                        currentNode.AddChild(word[i], false, default(T));
                    else
                    {
                        currentNode.AddChild(word[i], true, data);
                        currentNode = currentNode.GetChild(word[i]);
                        currentNode.Word = word;
                    }
                }
                else if (i == word.Length - 1)
                {
                    currentNode = currentNode.GetChild(word[i]);
                    currentNode.Key = data;
                    currentNode.IsWord = true;
                    currentNode.Word = word;
                    break;
                }
                currentNode = currentNode.GetChild(word[i]);
            }
        }
        public bool DeleteWord(string word)
        {
            var currentNode = root;
            foreach (char symbol in word)
            {
                if (!currentNode.FindChild(symbol))
                    return false;
                currentNode = currentNode.GetChild(symbol);
            }
            currentNode.IsWord = false;
            currentNode.Key = default(T);
            currentNode.Word = "";
            return true;
        }

        public T GetDataOfWord(string word)
        {
            var currentNode = root;
            foreach (char symbol in word)
            {
                if (!currentNode.FindChild(symbol))
                    return default(T);
                currentNode = currentNode.GetChild(symbol);
            }
            if (currentNode.IsWord == true)
                return currentNode.Key;
            return default(T);
        }

        public List<string> GetWordsForPrefix(string prefix)
        {
            var wordsForPrefix = new List<string>();
            var currentNode = root;
            foreach (var symbol in prefix)
            {
                if (currentNode.FindChild(symbol))
                    currentNode = currentNode.GetChild(symbol);
                else
                    return wordsForPrefix;
            }
            FindAllChildrenWords(currentNode, wordsForPrefix);
            return wordsForPrefix;
        }

        private void FindAllChildrenWords(Node<T> currentNode, List<string> wordsForPrefix)
        {
            if (currentNode.IsWord)
                wordsForPrefix.Add(currentNode.Word);
            foreach (var node in currentNode.Children.Values)
                FindAllChildrenWords(node, wordsForPrefix);
        }
    }
}