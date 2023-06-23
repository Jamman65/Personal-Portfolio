#pragma once
#include <iostream>
#include "Item.h"
using namespace std;
class HashTable
{
private:
    Item** data; //Creates a new item for the hash table which can be added into the table
    int size;

    int HashFunction(int key); //Organises the products via the key value within the hash table
   
  

public:
    HashTable(int size); //Constructor - Creates a hash table depending on the size entered
    void Display(); //Displays the current hash table
    void Insert(Item* item); //Inserts a new product into the hash table
    Item* Find(int key); //Finds a specific product via the value of the key
    Item* Delete(int key); //Deletes a specific product within the hash table
      
    
};
