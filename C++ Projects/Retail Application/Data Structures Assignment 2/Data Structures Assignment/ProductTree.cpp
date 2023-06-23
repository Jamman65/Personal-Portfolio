#include "ProductTree.h"



void ProductTree::Insert(string category, string subcategory, Product* product) { //Inserts a new category into the product tree
    
    Node* categoryNode = tree.Find(category, subcategory); //This finds the specific node that is made for each category
    if (categoryNode == 0) {
        Category newCategory;
        newCategory.name = category;
        categoryNode = tree.Insert(category, newCategory); //Adds the new category that has been created to the tree
    }

  
    Node* subcategoryNode = categoryNode = tree.Find(category, subcategory); //This finds the specific node for the subcategories
    if (subcategoryNode == 0) {
        Category newSubcategory;
        newSubcategory.name = subcategory;
        subcategoryNode = categoryNode = tree.Insert(category, newSubcategory); //Adds the new subcategory to the previous category
    }


    subcategoryNode->data.products.Insert(product); //Adds the new product that has been created to the tree
}


void ProductTree::DisplayInOrder() { //Displays all the categories in order 
    tree.DisplayInOrder(tree.root);
}