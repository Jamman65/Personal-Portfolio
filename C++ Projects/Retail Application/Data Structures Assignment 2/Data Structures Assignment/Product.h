#pragma once
#include <iostream>

using namespace std;
class Product {
private:
    int id;
    string name;
    double price;
    string category;


public:
    Product* next;
    Product(int id, string name, double price, string category); //Creates a new product with all the relevant data
    int GetId(); //returns the ID of the product
    string GetName(); //returns the name of the product
    double GetPrice(); //returns the price of the product
    void Display(); //Displays all the product data
 
};


