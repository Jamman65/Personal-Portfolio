#pragma once
#include "Node.h"
#include "Category.h"
class Tree
{
public:
    Node* root; //Creates a root node for the tree to start from
    Tree(); //Constructor
    Node* Find(string category, string subcategory); //Finds a specific node within the tree
    void DisplayInOrder(Node* localRoot); //Displays all the nodes in ordeer
    Node* Insert(string key, Category data); //Inserts a new category into the tree
    

};