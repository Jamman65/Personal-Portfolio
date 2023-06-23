#include "Node.h"

Node::Node(string key, Category data) { //Constructor - Creates a new node for the product tree
    this->key = key;
    this->data = data;
    this->Next = nullptr;
    this->First = nullptr;

}

void Node:: Display() { //Displays the key value of the node
    cout << key << endl;

}
