#pragma once
#include<iostream>
#include <string>
#include <iomanip>
using namespace std;
class TicketManager //Abstraction Technique
{
public:
	TicketManager() {

	}
	~TicketManager() {

	}
	//Defines the display function as abstract since the other classes will overide the function
	virtual void Display() = 0;
	string GenreChoice;
	

	
};

