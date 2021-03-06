﻿using System.Collections.Generic;
using System.Text;

namespace TrieStruct
{
    public class Trie<T>
    {
        //корень дерева (основная вершина: в качестве Data не хранит ничего, концом слова не является)
        private Node<T> root = new Node<T>(default(T), false);

        //Добавления нового слова в префиксное дерево
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
                    }
                }
                else if (i == word.Length - 1)
                {
                    currentNode = currentNode.GetChild(word[i]);
                    currentNode.Data = data;
                    currentNode.IsWord = true;
                    break;
                }
                currentNode = currentNode.GetChild(word[i]);
            }
        }

        //Удаление слова из префиксного дерева
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
            currentNode.Data = default(T);
            return true;
        }

        //Получение значения слова
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
                return currentNode.Data;
            return default(T);
        }

        //Получение списка всех слов, начинающихся с заданного префикса
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
            FindAllChildrenWords(prefix, currentNode, wordsForPrefix, new StringBuilder());
            return wordsForPrefix;
        }

        //Получение списка слов из ветвлений идущих после префикса
        private void FindAllChildrenWords(string prefix,
                                          Node<T> currentNode,
                                          List<string> wordsForPrefix,
                                          StringBuilder currentWord)
        {
            foreach (var node in currentNode.Children)
            {
                var currentPrefix = new StringBuilder(currentWord.ToString());
                if (currentPrefix.Length == 0)
                    currentPrefix.Append(prefix);
                currentPrefix.Append(node.Key);
                if (node.Value.IsWord)
                    wordsForPrefix.Add(currentPrefix.ToString());
                FindAllChildrenWords(prefix, node.Value, wordsForPrefix, currentPrefix);
            }
        }
    }
}