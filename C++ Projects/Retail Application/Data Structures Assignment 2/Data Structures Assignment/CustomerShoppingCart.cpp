#include "CustomerShoppingCart.h"

CustomerShoppingCart::CustomerShoppingCart() { //Constructor - Creates a new linked list
	ShoppingCart = new LinkedList<Product>();
}
CustomerShoppingCart::~CustomerShoppingCart() { //Destructor
	
}
void CustomerShoppingCart::AddProduct(Product* product) { //Adds a new product to the shopping cart
	ShoppingCart->Insert(product);
	cout << "Product successfully added to your cart" << endl;
}
void CustomerShoppingCart::removeProduct() { //Removes a product from the cart
	ShoppingCart->Delete();
}

void CustomerShoppingCart::ClearProducts() { //Clears the shopping cart of all products
	ShoppingCart->Clear();
}
void CustomerShoppingCart::DisplayCart() { //Displays all the current items within the cart
	if (ShoppingCart->IsEmpty()) {
		cout << "Your Shopping cart is empty" << endl;
		
	}
	else {
		cout << "Your cart contains:" << endl;
		ShoppingCart->Display();
	}
}
double CustomerShoppingCart::GetTotal() { //Returns the total price of the products in the cart
	double totalPrice = 0;
	Product* current = ShoppingCart->first;
	while (current != 0) {
		totalPrice += current->GetPrice();
		
	}
	return totalPrice;
}
