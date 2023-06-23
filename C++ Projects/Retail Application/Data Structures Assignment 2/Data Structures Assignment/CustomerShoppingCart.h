#pragma once
#include <iostream>
#include "Product.h"
#include "LinkedList.h"
using namespace std;
class CustomerShoppingCart  //This script utilises the linked list to add and remove products from the customers shopping cart
{
private:
	LinkedList<Product>* ShoppingCart; //Creates a linked list for the customer's shopping cart to allow them to add products to their cart

public:
	CustomerShoppingCart(); //Constructor
	~CustomerShoppingCart(); //Destructor
	void AddProduct(Product* product); //Adds a product to the customers cart using the linked list
	void removeProduct(); //Removes a product from the customers cart using the linked list
	void DisplayCart(); //Displays all of the current products within the cart
	void ClearProducts(); //Clears all of the products from the cart
	double GetTotal(); //Gets the total price of the products within the cart
};

