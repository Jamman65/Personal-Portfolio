#pragma once
//This class assigns the category of specific products
//Functions with the product tree
#include <iostream>
#include "Product.h"
#include "LinkedList.h"
using namespace std;
class Category
{
public:
	string name;
	LinkedList<Product> products; //Initalises a linked list for all of the products

	void Display();
	
};

