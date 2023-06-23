#pragma once
#include "Tree.h"
#include "Category.h"
class ProductTree :
    public Tree
{
public:
    ProductTree() {} //Constructor
    Tree tree;

    void Insert(string category, string subcategory, Product* product); //Inserts a new product into the tree
    void DisplayInOrder(); //Displays all the products in order 
       
   

   

};

