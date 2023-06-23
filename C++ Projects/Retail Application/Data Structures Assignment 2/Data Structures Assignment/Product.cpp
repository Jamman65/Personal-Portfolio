#include "Product.h"

Product::Product(int id, string name, double price, string category) { //Constructor - Creates a new product for the application
    this->id = id;
    this->name = name;
    this->price = price;
    this->category = category;
}

int Product::GetId() { //Returns the ID value of the product 
    return id;
}

string Product::GetName() { //Returns the name of the product
    return name;
}

double Product::GetPrice() { //Returns the price of the product
    return price;
}

void Product::Display() { //Displays all of the relevant details of the product
    cout << "Below Is all the details of the chosen Product:" << endl;
    cout << "Product ID: " << id << endl;
    cout << "Product Name: " << name << endl;
    cout << "Product Price: " << price << endl;
    cout << "Product Category: " << category << endl;
}
