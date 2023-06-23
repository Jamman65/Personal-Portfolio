#include "Item.h"
Item::Item(int key, string name, string description, string rating, int price) { //Constructor - Creates a new item with all the relevant data

    this->key = key;

    this->name = name;
    this->description = description;
    this->rating = rating;
    this->price = price;
}