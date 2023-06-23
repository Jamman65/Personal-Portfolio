#pragma once
#include <iostream>
#include "Category.h"
using namespace std;
class Node
{
public:
    string key;
    Category data;
    Node* Next; 
    Node* First;  

    Node(string key, Category data); //Constructor - Creates a new node for the product tree
    void Display(); //Displays all the node data
        
    
};