#pragma once
#include <iostream>
using namespace std;
class Item
{
public:
	int key;
	string name;
	string rating;
	string description;
	int price;

	Item(int key, string name, string description, string rating, int price); //Constructor - Creates a new item for the hash table
	
};

