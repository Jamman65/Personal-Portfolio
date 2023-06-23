#include "Tree.h"


Tree::Tree() { //Constructor
    root = 0;
}

Node* Tree::Find(string category, string subcategory) { //This Find function searches through the tree and finds the current node based on the category and subcategory
    Node* current = root;
    while (current != nullptr) {
        if (current->key == category) {
            Node* subcategoryNode = current->First;
            while (subcategoryNode != nullptr) {
                if (subcategoryNode->key == subcategory) {
                    return subcategoryNode;
                }
                subcategoryNode = subcategoryNode->Next;
            }
            return nullptr;
        }
        current = current->Next;
    }
    return nullptr;
}



void Tree::DisplayInOrder(Node* localRoot) { //This function displays all of the products in order with all of their assigned categories
    if (localRoot != nullptr) {
        DisplayInOrder(localRoot->First); 
        localRoot->data.Display();
        cout << " ";
        DisplayInOrder(localRoot->Next);
        
       
    }
}
Node* Tree::Insert(string key, Category data) { //Inserts a new product with their assigned key value and their desired category
    Node* newNode = new Node(key, data);
    if (root == 0) {
        root = newNode;
    }
    else {
        Node* current;
        current = root;
        Node* parent;
        while (true) {
            parent = current;
            if (key < current->key) {
                current = current->First;
                if (current == 0) {
                    parent->First = newNode;
                    return newNode;
                }
            }
            else {
                current = current->Next;
                if (current == 0) {
                    parent->Next = newNode;
                    return newNode;
                }
            }
        }
    }
}