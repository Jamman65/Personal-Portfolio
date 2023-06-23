#include "HashTable.h"

int HashTable::HashFunction(int key) { //Organises the hash table by the key value
    return key % size;
}


HashTable::HashTable(int size) { //Constructor - creates a new hash table depending on the size
    this->size = size;
    data = new Item * [size];
    for (int i = 0; i < size; i++) {
        data[i] = NULL;
    }
}
void HashTable::Display() { //Displays the current hash table with all the products
    for (int i = 0; i < size; i++) {
        if (data[i] != NULL) {
            cout << "Index: " << i << " || " << "Key: " << data[i]->key << " || "
                << "Name: " << data[i]->name << " || " << "Description: " << data[i]->description << " || "
                << "Reviews: " << data[i]->rating  << " || " << "Price:" << data[i]->price << " || " << endl;
        }
    }
}

void HashTable::Insert(Item* item) { //Inserts a new product into the table
    int key = item->key;
    int hash = HashFunction(key);
    while (data[hash] != NULL) {
        ++hash;
        hash %= size;
    }
    data[hash] = item;
}

Item* HashTable::Find(int key) { //Finds a specific product via the key value
    int hash = HashFunction(key);
    while (data[hash] != NULL) {
        if (data[hash]->key == key) {
            return data[hash];
        }
        hash++;
        hash = hash % size;
    }
    return NULL;
}

Item* HashTable::Delete(int key) { //Deletes a specifc product via the key value
    int hash = HashFunction(key);
    while (data[hash] != NULL) {
        if (data[hash]->key = key) {
            Item* temp = data[hash];
            data[hash] = NULL;
            return temp;
        }
        hash++;
        hash = size;
    }
    return NULL;
}