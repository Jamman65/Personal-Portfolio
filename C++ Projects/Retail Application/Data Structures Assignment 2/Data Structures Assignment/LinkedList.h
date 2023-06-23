#pragma once
#include "iostream"

using namespace std;
//Modified version of the linked list used within the units 
template <class T>
class LinkedList {
private:
    int items;

public:
    T* first;
    LinkedList();
    ~LinkedList();
    bool IsEmpty() const;
    void Insert(T* newLink);
    void Display() const;
    T* Find(T* key) const;
    T* Delete();
    T* Delete(T* key);
    int Count() const;
    T* operator[](const int& i);
    bool operator!=(const LinkedList<T>& other) const;
    void Clear(); 
};

template <class T>
LinkedList<T>::LinkedList() { //Constructor
    first = nullptr;
    items = 0;
}

template <class T>
LinkedList<T>::~LinkedList() { //Destructor
    Clear();
}

template <class T>
bool LinkedList<T>::IsEmpty() const { //Checks if the linked list is empty or not
    return (first == nullptr);
}

template <class T>
void LinkedList<T>::Insert(T* newLink) { //Inserts a new object into the linked list
    newLink->next = first;
    first = newLink;
    items++;
}

template <class T>
void LinkedList<T>::Display() const {  //Displays all the current objects stored
    T* current = first;
    while (current != nullptr) {
        current->Display();
        current = current->next;
    }
}

template <class T>
T* LinkedList<T>::Find(T* key) const { //Finds a specific object within the linked list
    T* current = first;
    while (current != nullptr) {
        if (*current == *key)
            return current;
        current = current->next;
    }
    return nullptr;
}

template <class T>
T* LinkedList<T>::Delete() { //Deletes an object within the linked list
    T* temp = first;
    first = first->next;
    items--;
    return temp;
}

template <class T>
T* LinkedList<T>::Delete(T* search) { //Deletes a specific object 
    T* current = first;
    T* previous = first;
    while (*current != *search) {
        if (current->next == nullptr)
            return nullptr;
        else {
            previous = current;
            current = current->next;
        }
    }
    if (current == first)
        first = first->next;
    else
        previous->next = current->next;
    items--;
    return current;
}

template <class T>
T* LinkedList<T>::operator[](const int& i) {
    int count = 0;
    T* current = first;
    while (current != nullptr) {
        if (count == i) return current;
        count++;
        current = current->next;
    }
    return nullptr;
}

template <class T>
int LinkedList<T>::Count() const { //returns the count of the items stored
    return items;
}

template <class T>
bool LinkedList<T>::operator!=(const LinkedList<T>& other) const {
    if (this == &other) return false;
    if (Count() != other.Count()) return true;
    T* current = first;
    T* other_current = other.first;
    while (current != nullptr) {
        if (*current != *other_current) return true;
        current = current->next;
        other_current = other_current->next;
    }
    return false;
}

template <class T>
void LinkedList<T>::Clear() { //Clears all the current items in the linked list
    while (first != nullptr) {
        T* temp = first;
        first = first->next;
        delete temp;
    }
    items = 0;
}
